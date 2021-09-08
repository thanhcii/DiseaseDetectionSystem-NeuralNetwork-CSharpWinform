using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DDSExplorer
{
    class NNAttributeSelection
    {
        private DataTable dt_DataTable;
        private int i_Total = 0;
        private int i_TotalPositives = 0;
        private double d_EntropySet = 0.0;

        public void SelectAttributes(int attributeCount, bool bIsMultipleInstance)
        {
            dt_DataTable = LoadUtil.GetDataTable();
            i_Total = dt_DataTable.Rows.Count;

            List<NNAttribute> attributeList = GetAttributesFromTable(dt_DataTable, bIsMultipleInstance);

            if (attributeList.Count <= attributeCount)
                return;
            else
            {
                i_TotalPositives = countTotalPositives(dt_DataTable);
                d_EntropySet = calcEntropy(i_TotalPositives, i_Total - i_TotalPositives);

                //sort the attribute list
                SortBestAttributes(dt_DataTable, attributeList);

                DataTable dtNNDataTable;
                dtNNDataTable = dt_DataTable.Copy();
                for (int i = attributeCount; i < attributeList.Count; i++)
                {
                    dtNNDataTable.Columns.Remove(attributeList[i].AttributeName);
                }

                NNClassifierUtil.SetNNDataTable(dtNNDataTable);
            }
        }


        private List<NNAttribute> GetAttributesFromTable(DataTable _dtDataTable, bool bIsMultipleInstance)
        {
            List<NNAttribute> attributeList = new List<NNAttribute>();
            NNAttribute attribute;

            foreach (DataColumn dcColumn in _dtDataTable.Columns)
            {
                //omit CaseID and Result columns
                if (bIsMultipleInstance == true && (dcColumn.ColumnName == "CaseID" || dcColumn.ColumnName == "Result"))
                    continue;
                else if (dcColumn.ColumnName == "Result")
                    continue;

                ArrayList alValues = GetDistinctValues(_dtDataTable, dcColumn.Caption.ToString());
                attribute = new NNAttribute(dcColumn.Caption.ToString(), alValues);
                attributeList.Add(attribute);
            }

            return attributeList;
        }


        public ArrayList GetDistinctValues(DataTable samples, string targetAttribute)
        {
            ArrayList distinctValues = new ArrayList(samples.Rows.Count);

            foreach (DataRow row in samples.Rows)
            {
                if (distinctValues.IndexOf(row[targetAttribute]) == -1)
                    distinctValues.Add(row[targetAttribute]);
            }

            return distinctValues;
        }

        private void SortBestAttributes(DataTable samples, List<NNAttribute> attributes)
        {
            DataTable dataTable = new DataTable();

            for (int i = 0; i < attributes.Count; i++)
            {
                double aux = gain(samples, attributes[i]);
                attributes[i].gain = aux;
            }

            attributes.Sort();
        }


        private double gain(DataTable samples, NNAttribute attribute)
        {
            double[] values = attribute.values;
            double sum = 0.0;

            for (int i = 0; i < values.Length; i++)
            {
                int positives, negatives;

                positives = negatives = 0;

                getValuesToAttribute(samples, attribute, values[i], out positives, out negatives);

                double entropy = calcEntropy(positives, negatives);
                sum += -(double)(positives + negatives) / i_Total * entropy;
            }
            
            return d_EntropySet + sum;
        }

        private double calcEntropy(int positives, int negatives)
        {
            int total = positives + negatives;
            double ratioPositive = (double)positives / total;
            double ratioNegative = (double)negatives / total;

            if (ratioPositive != 0)
                ratioPositive = -(ratioPositive) * System.Math.Log(ratioPositive, 2);
            if (ratioNegative != 0)
                ratioNegative = -(ratioNegative) * System.Math.Log(ratioNegative, 2);

            double result = ratioPositive + ratioNegative;

            return result;
        }

        private void getValuesToAttribute(DataTable samples, NNAttribute attribute, double value, out int positives, out int negatives)
        {
            positives = 0;
            negatives = 0;


            foreach (DataRow aRow in samples.Rows)
            {
                if (((double)aRow[attribute.AttributeName] == value))
                    if ((double)aRow["Result"] > 0.0)
                        positives++;
                    else
                        negatives++;
            }
        }

        private int countTotalPositives(DataTable samples)
        {
            int result = 0;

            foreach (DataRow aRow in samples.Rows)
            {
                if ((double)aRow["Result"] > 0.0)
                    result++;
            }

            return result;
        }

        
    }
}
