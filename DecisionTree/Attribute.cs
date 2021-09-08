using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DecisionTrees
{
    /// <summary>
    /// Class which represents an attribute used in the class of decision
    /// </summary>
    public class Attribute
    {
        ArrayList mValues;
        string mName;
        object mLabel;

        /// <summary>
        /// Boots a new instance of a class Atribute
        /// </summary>
        /// <param name="name">It indicates the attribute name</param>
        /// <param name="values">It indicates the possible values for the attribute</param>
        public Attribute(string name, string[] values)
        {
            mName = name;
            mValues = new ArrayList(values);
            mValues.Sort();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="values"></param>
        public Attribute(string name, ArrayList values)
        {
            mName = name;
            mValues = values;
            mValues.Sort();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Label"></param>
        public Attribute(object Label)
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

        public object MLabel
        {
            get
            {
                return mLabel;
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
            {
                return mValues.BinarySearch(value);
            }
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
    }

}
