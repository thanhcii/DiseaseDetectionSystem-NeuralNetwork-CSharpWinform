using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

using DecisionTrees;

namespace DDSExplorer
{
    class DTClassifierUtil
    {
        private DecisionTree DTree = new DecisionTree();

        private static DataTable dt_DTDataTable;

        // Decision Tree, for one case strat
        // for multiple instance, bootstrap, based on case ID
        // for single instance, cross fold, based on randomised grouping
        private static DataTable dt_TrainTable;
        private static DataTable dt_TestTable;

        public static void SetDTDataTable(DataTable _dtDataTable)
        {
            dt_DTDataTable = _dtDataTable;
        }

        public double BootStrapEvaluate(out double dNPValue)
        {
            // for multiple instance data set
            LoadUtil.CountCases();

            SetDTDataTable(LoadUtil.GetDataTable());

            double dAccuracyTot = 0.0;
            double dAccuracy = 0.0;
            double dNPVTot = 0.0;
            double dNPV = 0.0;

            // for each case
            foreach (double dCaseID in LoadUtil.al_Caseids)
            {
                // bootstrap
                DTStratify(dCaseID);
                dAccuracyTot += TrainandTest(out dNPV);
                dNPVTot += dNPV;
            }

            dAccuracy = (dAccuracyTot / LoadUtil.al_Caseids.Count);
            dNPValue = (dNPVTot / 10);
            return dAccuracy;

        }

        public double CrossFoldEvaluate(out double dNPValue)
        {
            LoadUtil.CreateCrossFolds();

            SetDTDataTable(LoadUtil.GetDataTable());

            double dAccuracyTot = 0.0;
            double dAccuracy = 0.0;
            double dNPVTot = 0.0;
            double dNPV = 0.0;

            for (int i = 0; i < 10; i++)
            {
                // crossfold
                DTStratify(i);
                dAccuracyTot += TrainandTest(out dNPV);
                dNPVTot += dNPV;
            }

            dAccuracy = (dAccuracyTot / 10);
            dNPValue = (dNPVTot / 10);
            return dAccuracy;

        }

        // for Decision Tree classifier, multiple instance dataset
        private void DTStratify(double _dCaseID)
        {
            dt_TrainTable = dt_DTDataTable.Clone();
            dt_TestTable = dt_DTDataTable.Clone();
            dt_TrainTable.Rows.Clear();
            dt_TestTable.Rows.Clear();

            foreach (DataRow drRow in dt_DTDataTable.Rows)
            {
                if (((double)drRow["CaseID"]) == _dCaseID)
                    dt_TestTable.Rows.Add(drRow.ItemArray);
                else
                    dt_TrainTable.Rows.Add(drRow.ItemArray);
            }

            // remove the Case ID column
            dt_TestTable.Columns.Remove("CaseID");
            dt_TrainTable.Columns.Remove("CaseID");
        }

        // for Decision Tree classifier, single instance dataset
        public static void DTStratify(int iIndex)
        {
            dt_TrainTable = dt_DTDataTable.Clone();
            dt_TrainTable.Rows.Clear();

            dt_TestTable = LoadUtil.GetCrossFoldTable(iIndex);

            for (int i = 0; i < 10; i++)
            {
                if (i != iIndex)
                    dt_TrainTable.Merge(LoadUtil.GetCrossFoldTable(i));
            }
        }

        public double TrainandTest(out double dNPV)
        {
            double dAccuracyInd = 0.0;

            // construct the tree
            // column count is the actual count minus one, as one column is the Result column
            DecisionTrees.Attribute[] oaAttributes = new DecisionTrees.Attribute[dt_TrainTable.Columns.Count - 1];

            int x = 0;
            foreach (DataColumn dcColumn in dt_TrainTable.Columns)
            {
                // omit the Result column
                if (x != 0)
                {
                    ArrayList alValues = DTree.getDistinctValues(dt_TrainTable, dcColumn.Caption.ToString());
                    oaAttributes[x - 1] = new DecisionTrees.Attribute(dcColumn.Caption.ToString(), alValues);
                }
                x++;
            }

            TreeNode Root = DTree.mountTree(dt_TrainTable, "Result", oaAttributes);

            // test the tree
            int iRecordCount = dt_TestTable.Rows.Count;
            int iCorrectResultCount = 0;
            int iTrueNegative = 0;
            int iFalseNegative = 0;
            
            Double dAttributeValue;
            TreeNode ChildNode;

            foreach (DataRow row in dt_TestTable.Rows)
            {
                dAttributeValue = (double)(row[Root.attribute.AttributeName]);
                ChildNode = Root.getChildByBranchName(dAttributeValue);

                if ((bool)ChildNode.attribute.MLabel == true)
                {
                    if ((double)row["Result"] > 0.0)
                        iCorrectResultCount++;
                }
                else
                {
                    if ((double)row["Result"] == 0.0)
                    {
                        iCorrectResultCount++;
                        iTrueNegative++;
                    }
                    else
                        iFalseNegative++;
                }
            }

            if (iRecordCount != 0)
            {
                dAccuracyInd = ((double)iCorrectResultCount / (double)iRecordCount);
                dNPV = (double)iTrueNegative / ((double)iTrueNegative + (double)iFalseNegative);
            }

            else
            {
                dAccuracyInd = 1.0;
                dNPV = 1.0;
            }

            return dAccuracyInd;
        }

    }
}
