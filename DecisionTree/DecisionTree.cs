using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DecisionTrees
{
    /// <summary>
    /// Class that implements a decision tree of the algorithm using ID3
    /// </summary>
    public class DecisionTree
    {
        private DataTable mSamples;
        private int mTotalPositives = 0;
        private int mTotal = 0;
        private string mTargetAttribute;
        private double mEntropySet = 0.0;

        /// <summary>
        /// Returns the total number of positive samples in a table of samples
        /// </summary>
        /// <param name="samples">DataTable with the samples</param>
        /// <returns>The total number of positive samples</returns>
        private int countTotalPositives(DataTable samples)
        {
            int result = 0;

            foreach (DataRow aRow in samples.Rows)
            {
                if ((double)aRow[mTargetAttribute] > 0.0)
                    result++;
            }

            return result;
        }

        /// <summary>
        /// Calculate the entropy given the following formula
        /// -p+log2p+ - p-log2p-
        /// 
        /// where: p + e is the proportion of positive values
        ///		   p - e is the proportion of negative values
        /// </summary>
        /// <param name="positives">Number of positive values</param>
        /// <param name="negatives">Number of negative values</param>
        /// <returns>Retorna o valor da Entropia</returns>
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

        /// <summary>
        /// Varre table of samples and checking an attribute if the result is positive or negative
        /// </summary>
        /// <param name="samples">DataTable with the samples</param>
        /// <param name="attribute">Attribute to search</param>
        /// <param name="value">value allowed for the attribute</param>
        /// <param name="positives">No to contain all the attributes with the value determined by positive result</param>
        /// <param name="negatives">No to contain all the attributes with the value determined with negative</param>
        private void getValuesToAttribute(DataTable samples, Attribute attribute, double value, out int positives, out int negatives)
        {
            positives = 0;
            negatives = 0;

            foreach (DataRow aRow in samples.Rows)
            {
                if (((double)aRow[attribute.AttributeName] == value))
                    if ((double)aRow[mTargetAttribute] > 0.0)
                        positives++;
                    else
                        negatives++;
            }

            //TODO
            if (positives == 0 && negatives == 0)
                negatives++;
        }

        /// <summary>
        /// Calculate the gain of an attribute
        /// </summary>
        /// <param name="attribute">Attribute to be calculated</param>
        /// <param name="samples">DataTable</param>
        /// <returns>The gain of attribute</returns>
        private double gain(DataTable samples, Attribute attribute)
        {
            double[] values = attribute.values;
            double sum = 0.0;

            for (int i = 0; i < values.Length; i++)
            {
                int positives, negatives;

                positives = negatives = 0;

                getValuesToAttribute(samples, attribute, values[i], out positives, out negatives);

                double entropy = calcEntropy(positives, negatives);
                sum += -(double)(positives + negatives) / mTotal * entropy;
            }
            return mEntropySet + sum;
        }

        /// <summary>
        /// Returns the best attribute
        /// </summary>
        /// <param name="attributes">A vector with the attributes</param>
        /// <returns>Returns which have greater gain</returns>
        private Attribute getBestAttribute(DataTable samples, Attribute[] attributes)
        {
            double maxGain = 0.0;
            Attribute result = null;

            foreach (Attribute attribute in attributes)
            {
                double aux = gain(samples, attribute);
                if (aux > maxGain)
                {
                    maxGain = aux;
                    result = attribute;
                }
            }
            return result;
        }

        /// <summary>
        /// Returns true if all are positive examples of sampling
        /// </summary>
        /// <param name="samples">DataTable with the samples</param>
        /// <param name="targetAttribute">Attribute (column) of the table which will be verified</param>
        /// <returns>True if all are positive examples of sampling</returns>
        private bool allSamplesPositives(DataTable samples, string targetAttribute)
        {
            foreach (DataRow row in samples.Rows)
            {
                if((double)row[targetAttribute] == 0.0)
                   return false;
            }

            return true;
        }

        /// <summary>
        /// Returns true if all examples of sampling are negative
        /// </summary>
        /// <param name="samples">DataTable with the samples</param>
        /// <param name="targetAttribute">Attribute (column) of the table which will be verified</param>
        /// <returns>True if all examples of sampling are negative</returns>
        private bool allSamplesNegatives(DataTable samples, string targetAttribute)
        {
            foreach (DataRow row in samples.Rows)
            {
                if ((double)row[targetAttribute] > 0.0)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Returns a list of all values of a separate table sampling
        /// </summary>
        /// <param name="samples">DataTable with the samples</param>
        /// <param name="targetAttribute">Attribute (column) of the table which will be verified</param>
        /// <returns>An ArrayList with different values</returns>
        public ArrayList getDistinctValues(DataTable samples, string targetAttribute)
        {
            ArrayList distinctValues = new ArrayList(samples.Rows.Count);

            foreach (DataRow row in samples.Rows)
            {
                if (distinctValues.IndexOf(row[targetAttribute]) == -1)
                    distinctValues.Add(row[targetAttribute]);
            }

            return distinctValues;
        }

        /// <summary>
        /// Returns the most common value within a sampling
        /// </summary>
        /// <param name="samples">DataTable with the samples</param>
        /// <param name="targetAttribute">Attribute (column) of the table which will be verified</param>
        /// <returns>Returns the object with a higher incidence in the table of samples</returns>
        private object getMostCommonValue(DataTable samples, string targetAttribute)
        {
            ArrayList distinctValues = getDistinctValues(samples, targetAttribute);
            int[] count = new int[distinctValues.Count];

            foreach (DataRow row in samples.Rows)
            {
                int index = distinctValues.IndexOf(row[targetAttribute]);
                count[index]++;
            }

            int MaxIndex = 0;
            int MaxCount = 0;

            for (int i = 0; i < count.Length; i++)
            {
                if (count[i] > MaxCount)
                {
                    MaxCount = count[i];
                    MaxIndex = i;
                }
            }

            return distinctValues[MaxIndex];
        }

        /// <summary>
        /// Mount a tree for a decision based on samples submitted
        /// </summary>
        /// <param name="samples">Table with the samples to be submitted to the assembly of the tree</param>
        /// <param name="targetAttribute">Name of the column of the table that has the value true or false to 
        /// validate or not a sampling</param>
        /// <returns>The root of the tree, mounted decision</returns></returns?>
        private TreeNode internalMountTree(DataTable samples, string targetAttribute, Attribute[] attributes)
        {
            if (allSamplesPositives(samples, targetAttribute) == true)
                return new TreeNode(new Attribute(true));

            if (allSamplesNegatives(samples, targetAttribute) == true)
                return new TreeNode(new Attribute(false));
            
            if (attributes.Length == 0)
                return new TreeNode(new Attribute(getMostCommonValue(samples, targetAttribute)));

            mTotal = samples.Rows.Count;
            mTargetAttribute = targetAttribute;
            mTotalPositives = countTotalPositives(samples);

            mEntropySet = calcEntropy(mTotalPositives, mTotal - mTotalPositives);

            Attribute bestAttribute = getBestAttribute(samples, attributes);

            TreeNode root = new TreeNode(bestAttribute);

            DataTable aSample = samples.Clone();

            foreach (double value in bestAttribute.values)
            {

                // Select all the elements with the value of this attribute	
                aSample.Rows.Clear();

                DataRow[] rows = samples.Select(bestAttribute.AttributeName + " = " + "'" + value + "'");

                foreach (DataRow row in rows)
                {
                    aSample.Rows.Add(row.ItemArray);
                }

                // Select all the elements with the value of this attribute	

                // Create a new list of attributes less attribute the current that is the best attribute
                ArrayList aAttributes = new ArrayList(attributes.Length - 1);
                for (int i = 0; i < attributes.Length; i++)
                {
                    if (attributes[i].AttributeName != bestAttribute.AttributeName)
                        aAttributes.Add(attributes[i]);
                }

                // Create a new list of attributes less attribute the current that is the best attribute
                if (aSample.Rows.Count == 0)
                {
                    //TODO
                    return new TreeNode(new Attribute(false));
                    //return new TreeNode(new Attribute(getMostCommonValue(aSample, targetAttribute)));
                }
                else
                {
                    DecisionTree dc = new DecisionTree();
                    TreeNode ChildNode = dc.mountTree(aSample, targetAttribute, (Attribute[])aAttributes.ToArray(typeof(Attribute)));
                    root.AddTreeNode(ChildNode, value);
                }
            }

            return root;
        }

        /// <summary>
        /// Mount a tree for a decision based on samples submitted
        /// </summary>
        /// <param name="samples">Table with the samples to be submitted to the assembly of the tree</param>
        /// <param name="targetAttribute">Name of the column of the table that has the value true or false to 
        /// validate or not a sampling</param>
        /// <returns>The root of the tree, mounted decision</returns></returns?>
        public TreeNode mountTree(DataTable samples, string targetAttribute, Attribute[] attributes)
        {
            mSamples = samples;
            return internalMountTree(mSamples, targetAttribute, attributes);
        }
    }

}
