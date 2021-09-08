using System;
using System.Text;
using System.Data;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace DDSExplorer
{
    public class LoadUtil
    {
        private static String s_DataFile = "";
        private static DataTable dt_DataTable = null;

        // multiple instance
        public static int i_NumCases = 0;
        public static ArrayList al_Caseids = null;

        // single instance, cross fold tables
        private static DataTable[] DataGroups = new DataTable[10];


        public static String GetTableName()
        {
            return s_DataFile;
        }

        public static void SetTableName(String _sDataFile)
        {
            s_DataFile = _sDataFile;
        }
        
        public static DataTable GetDataTable()
        {
            return dt_DataTable;
        }

        public static void SetDataTable(DataTable _dtDataTable)
        {
            dt_DataTable = _dtDataTable;
        }

        public static DataTable GetCrossFoldTable(int iIndex)
        {
            return DataGroups[iIndex];
        }
        
        public static void LoadData(bool _bIsMultipleInstance)
        {
            StreamReader reader = null;
            DataRow drRow;
            DataColumn dcColumn;

            dt_DataTable = new DataTable("DataTable");

            int iSamples = 0;
            int iColumns = 0;
            int iColumnIndex = 0;
                
            try
            {
                string str = null;

                // open selected file
                reader = File.OpenText(s_DataFile);

                // read the data
                while ((str = reader.ReadLine()) != null)
                {
                    // split the string
                    // need to replace the tab with space if there are any
                    //string[] strs = str.Split(' ');
                    string[] strs = str.Replace("\t", " ").Split(' ');

                    if (iSamples == 0)
                    {
                        for (int i = 0; i < strs.Length; i++)
                        {
                            if (strs[i] != "")
                            {
                                // add columns in Datatable
                                // requirement
                                // first column is CaseID, second column is Result in multiple instance data set
                                // first column is Result in single instance data set
                                // next column starts in 1
                                if (_bIsMultipleInstance && iColumns == 0)
                                    dcColumn = dt_DataTable.Columns.Add("CaseID");
                                else if (!_bIsMultipleInstance && iColumns == 0)
                                    dcColumn = dt_DataTable.Columns.Add("Result");
                                else if (_bIsMultipleInstance && iColumns == 1)
                                    dcColumn = dt_DataTable.Columns.Add("Result");
                                else
                                {
                                    if (_bIsMultipleInstance)
                                        iColumnIndex = iColumns - 1;
                                    else
                                        iColumnIndex = iColumns;
                                    dcColumn = dt_DataTable.Columns.Add("Attribute_" + (iColumnIndex).ToString());
                                }

                                dcColumn.DataType = typeof(double);
                                iColumns++;
                            }
                        }
                    }

                    // create a new row
                    drRow = dt_DataTable.NewRow();
                    iColumns = 0;

                    // assign data to each column of the row
                    for (int i = 0; i < strs.Length; i++)
                    {
                        if (strs[i] != "")
                        {
                            drRow[iColumns] = double.Parse(strs[i]);
                            iColumns++;
                        }
                    }

                    // add the row in Datatable
                    dt_DataTable.Rows.Add(drRow);
                    iSamples++;
                }

                
                // normalizing the attribute values
                // deduct 2 for multiple instance data set
                // deduct 1 for single instance data set
                int iAttColCount;
                if(_bIsMultipleInstance)
                    iAttColCount = dt_DataTable.Columns.Count - 2;
                else
                    iAttColCount = dt_DataTable.Columns.Count - 1;

                for (int k = 1; k <= iAttColCount; k++)
                {
                    string sMaxSelectString = "Max(Attribute_" + k.ToString() + " )";
                    string sMinSelectString = "Min(Attribute_" + k.ToString() + " )";
                    double dMax = (double)dt_DataTable.Compute(sMaxSelectString, string.Empty);
                    double dMin = (double)dt_DataTable.Compute(sMinSelectString, string.Empty);

                    foreach (DataRow oRow in dt_DataTable.Rows)
                    {
                        double value = (double)oRow["Attribute_" + k.ToString()];
                        double normalizedvalue = 0.0;
                        //TODO : What should be the exact normalized value in else clause, 0 or 1 or anything else?
                        //TODO : What should be the rounding value, now it is 12
                        if (dMax != dMin)
                        {
                            normalizedvalue = (value - dMin) / (dMax - dMin);
                            // round it to 12 decimal points
                            normalizedvalue = Math.Round(normalizedvalue, 12);
                        }
                        else
                            normalizedvalue = 1.0;
                        //normalize(x) = (x - min(x)) / (max(x) - min(x));
                        //Math.Round(3.44, 1); //Returns 3.4
                        oRow.BeginEdit();
                        oRow["Attribute_" + k.ToString()] = normalizedvalue;
                        oRow.EndEdit();
                    }
                }

                dt_DataTable.AcceptChanges();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // close file
                if (reader != null)
                    reader.Close();
            }

        }

        // count cases for multiple instance Data set 
        public static void CountCases()
        {
            double dCaseid = -1;
            al_Caseids = new ArrayList();

            foreach (DataRow drRow in dt_DataTable.Rows)
            {
                if (((double)drRow["CaseID"]) != dCaseid)
                {
                    i_NumCases++;
                    al_Caseids.Add((double)drRow["CaseID"]);
                    dCaseid = (double)drRow["CaseID"];
                }
            }
        }

        // split the datatable into 10 cross fold tables
        public static void CreateCrossFolds()
        {
            Random random = new Random();

            for (int k = 0; k < 10; k++)
            {
                DataGroups[k] = new DataTable();
                DataGroups[k] = dt_DataTable.Clone();
            }

            foreach (DataRow oRow in dt_DataTable.Rows)
            {
                //generate random numbers between 10 numbers
                int randomIndex = random.Next(0, 10);
                DataGroups[randomIndex].Rows.Add(oRow.ItemArray);
            }

        }
        
    }
}
