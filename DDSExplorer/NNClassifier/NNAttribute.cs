using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DDSExplorer
{
    /// <summary>
    /// Class which represents an attribute
    /// </summary>
    public class NNAttribute : IComparable<NNAttribute>
    {
        ArrayList mValues;
        string mName;
        object mLabel;
        public double gain;

        /// <summary>
        /// Boots a new instance of a class Atribute
        /// </summary>
        /// <param name="name">It indicates the attribute name</param>
        /// <param name="values">It indicates the possible values for the attribute</param>
        public NNAttribute(string name, string[] values)
        {
            mName = name;
            mValues = new ArrayList(values);
            mValues.Sort();
        }

        public NNAttribute(string name, ArrayList values)
        {
            mName = name;
            mValues = values;
            mValues.Sort();
        }

        public NNAttribute(object Label)
        {
            mLabel = Label;
            mName = string.Empty;
            mValues = null;
        }

        /// <summary>
        /// It indicates the attribute name
        /// </summary>
        public string AttributeName
        {
            get
            {
                return mName;
            }
        }

        /// <summary>
        /// Returns an array with the values of the attribute
        /// </summary>
        public double[] values
        {
            get
            {
                if (mValues != null)
                    return (double[])mValues.ToArray(typeof(double));
                else
                    return null;
            }
        }

        /// <summary>
        /// It indicates whether a value is allowed to attribute this
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool isValidValue(double value)
        {
            return indexValue(value) >= 0;
        }

        /// <summary>
        /// Returns the index of the value
        /// </summary>
        /// <param name="value">Value to be returned</param>
        /// <returns>The value of the index in which the position of the value is</returns>
        public int indexValue(double value)
        {
            if (mValues != null)
                return mValues.BinarySearch(value);
            else
                return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (mName != string.Empty)
            {
                return mName;
            }
            else
            {
                return mLabel.ToString();
            }
        }

        /// <summary>
        /// method implemented to compare and sort the elements in the list
        /// </summary>
        /// <param name="otherNNAttribute"></param>
        /// <returns></returns>
        public int CompareTo(NNAttribute otherNNAttribute)
        {
            return otherNNAttribute.gain == this.gain  ? 0 : (otherNNAttribute.gain > this.gain) ? 1 : -1;
        }
    }

}
