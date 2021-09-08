using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using NeuralNetworks;

namespace DDSExplorer
{
    class NNClassifierUtil
    {
        private NeuralNetwork Network = new NeuralNetwork();

        // Datatable with selected attribute for NN classifier
        private static DataTable dt_NNDataTable;

        // Neural Network, for one case strat
        // for multiple instance, bootstrap, based on case ID
        // for single instance, cross fold, based on randomised grouping
        private static double[,] Testinput;
        private static double[,] Traininput;
        private static double[,] Testoutput;
        private static double[,] Trainoutput;

        public static DataTable GetNNDataTable()
        {
            return dt_NNDataTable;
        }

        public static void SetNNDataTable(DataTable _dtDataTable)
        {
            dt_NNDataTable = _dtDataTable;
        }

        public double BootStrapEvaluate()
        {
            // for multiple instance data set
            LoadUtil.CountCases();

            double dAccuracyTot = 0.0;
            double dAccuracyInd = 0.0;
            double dAccuracy = 0.0;

            // for each case
            foreach (double dCaseID in LoadUtil.al_Caseids)
            {
                // bootstrap
                NNStratify(dCaseID);
                dAccuracyInd = TrainandTest();
                dAccuracyTot += dAccuracyInd;
            }

            dAccuracy = (dAccuracyTot / LoadUtil.al_Caseids.Count);
            return dAccuracy;

        }

        public double CrossFoldEvaluate()
        {
            LoadUtil.CreateCrossFolds();

            double dAccuracyTot = 0.0;
            double dAccuracyInd = 0.0;
            double dAccuracy = 0.0;

            for (int i = 0; i < 10; i++)
            {
                // crossfold
                NNStratify(i);
                dAccuracyInd = TrainandTest();
                dAccuracyTot += dAccuracyInd;
            }

            dAccuracy = (dAccuracyTot / 10);
            return dAccuracy;
            
        }

        // for Neural Network classifier, multiple instance dataset
        public static void NNStratify(double _dCaseID)
        {
            // deduct 2 since it is multiple instance
            int iVariables = dt_NNDataTable.Columns.Count - 2;
            int iSamples = dt_NNDataTable.Rows.Count;

            int iCount = dt_NNDataTable.Select("CaseID=" + _dCaseID).Length;

            Testinput = new Double[iCount, iVariables];
            Traininput = new Double[iSamples - iCount, iVariables];
            Testoutput = new Double[iCount, 1];
            Trainoutput = new Double[iSamples - iCount, 1];

            int iTestIndex = 0;
            int iTrainIndex = 0;
            int iInnerIndex = 0;

            foreach (DataRow drRow in dt_NNDataTable.Rows)
            {
                //construct test data
                if ((double)drRow["CaseID"] == _dCaseID)
                {
                    iInnerIndex = 0;

                    Testoutput[iTestIndex, 0] = Convert.ToDouble(drRow[1]);

                    // start with 2 since it is multiple instance
                    for (int i = 2; i <= (iVariables + 1); i++)
                    {
                        Testinput[iTestIndex, iInnerIndex] = Convert.ToDouble(drRow[i]);
                        iInnerIndex++;
                    }
                    iTestIndex++;
                }

                //construct training data
                else
                {
                    iInnerIndex = 0;

                    Trainoutput[iTrainIndex, 0] = Convert.ToDouble(drRow[1]);

                    // start with 2 since it is multiple instance
                    for (int i = 2; i <= (iVariables + 1); i++)
                    {
                        Traininput[iTrainIndex, iInnerIndex] = Convert.ToDouble(drRow[i]);
                        iInnerIndex++;
                    }
                    iTrainIndex++;
                }
            }

        }

        // for Neural Network classifier, single instance dataset
        public static void NNStratify(int iIndex)
        {
            DataTable dt_TempCrossFoldTable = LoadUtil.GetCrossFoldTable(iIndex);

            // deduct 1 since it is multiple instance
            int iVariables = dt_NNDataTable.Columns.Count - 1;
            int iSamples = dt_NNDataTable.Rows.Count;

            int iCount = dt_TempCrossFoldTable.Rows.Count;

            Testinput = new Double[iCount, iVariables];
            Traininput = new Double[iSamples - iCount, iVariables];
            Testoutput = new Double[iCount, 1];
            Trainoutput = new Double[iSamples - iCount, 1];

            int iTestIndex = 0;
            int iTrainIndex = 0;
            int iInnerIndex = 0;


            //construct test data
            foreach (DataRow drRow in dt_TempCrossFoldTable.Rows)
            {
                // Result column is the first column in DataTable
                Testoutput[iTestIndex, 0] = Convert.ToDouble(drRow[0]);

                iInnerIndex = 0;
                for (int i = 1; i <= iVariables; i++)
                {
                    Testinput[iTestIndex, iInnerIndex] = Convert.ToDouble(drRow[i]);
                    iInnerIndex++;
                }

                iTestIndex++;
            }

            //construct training data

            for (int i = 0; i < 10; i++)
            {
                if (i != iIndex)
                {
                    //cross fold tables except the table of iIndex
                    dt_TempCrossFoldTable = LoadUtil.GetCrossFoldTable(i);

                    foreach (DataRow drRow in dt_TempCrossFoldTable.Rows)
                    {
                        Trainoutput[iTrainIndex, 0] = Convert.ToDouble(drRow[0]);

                        iInnerIndex = 0;
                        for (int j = 1; j <= iVariables; j++)
                        {
                            Traininput[iTrainIndex, iInnerIndex] = Convert.ToDouble(drRow[j]);
                            iInnerIndex++;
                        }
                        iTrainIndex++;
                    }
                }
            }

        }
  
        public double TrainandTest()
        {
            double dAccuracyInd = 0.0;

            // train the network
            int recordCount = Traininput.GetUpperBound(0) + 1;

            if (recordCount != (Trainoutput.GetUpperBound(0) + 1))
            {
                throw new System.ArgumentOutOfRangeException("number of samples in input data is not equal to number of samples in output data");
            }

            //TODO : Change the hidden layer neuron count and test
            Network.Initialize(Traininput, Trainoutput, 4);
            Network.b_IsTraining = true;

            for (int count = 0; count < NeuralNetworks.NeuralNetwork.iterations; count++)
            {
                for (int sample = 0; sample < recordCount; sample++)
                {

                    Network.FeedForward(sample);

                    Network.BackPropagate();

                }

                //adjust the learning rate after every training sample iteration
                //net.learning_rate = (double) (1 / ((double)count + 1));      
            }

            //test the network
            recordCount = Testinput.GetUpperBound(0) + 1;

            Network.b_IsTraining = false;

            Network.input_data = Testinput;
            int iCorrectResultCount = 0;

            for (int sample = 0; sample < recordCount; sample++)
            {
                Network.FeedForward(sample);
                if (Testoutput[sample, 0] == 0.0)
                {
                    if (Network.output[0].Output == 0.0)
                        iCorrectResultCount++;
                }
                else
                {
                    if (Network.output[0].Output > 0.0)
                        iCorrectResultCount++;
                }

            }

            if (recordCount != 0)
                dAccuracyInd = ((double)iCorrectResultCount / (double)recordCount);
            else
                dAccuracyInd = 0.0;

            return dAccuracyInd;
        }


    }
}
