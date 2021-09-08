using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DecisionTrees
{
    /// <summary>
    /// Class will represent the tree of decision mounted
    /// </summary>
    public class TreeNode
    {
        private ArrayList mChilds = null;
        private Attribute mAttribute;

        /// <summary>
        /// Boots a new instance of TreeNode
        /// </summary>
        /// <param name="attribute">Attribute to which the node is connected</param>
        public TreeNode(Attribute attribute)
        {
            if (attribute.values != null)
            {
                mChilds = new ArrayList(attribute.values.Length);
                for (int i = 0; i < attribute.values.Length; i++)
                    mChilds.Add(null);
            }
            else
            {
                mChilds = new ArrayList(1);
                mChilds.Add(null);
            }
            mAttribute = attribute;
        }

        /// <summary>
        /// Adds a TreeNode child in this branch of treenode in the name indicated by ValueName
        /// </summary>
        /// <param name="treeNode">TreeNode child to be added</param>
        /// <param name="ValueName">Name of where the branch is created treeNode</param>
        public void AddTreeNode(TreeNode treeNode, double ValueName)
        {
            int index = mAttribute.indexValue(ValueName);
            mChilds[index] = treeNode;
        }

        /// <summary>
        /// Returns nro the total of children of the node
        /// </summary>
        public int totalChilds
        {
            get
            {
                return mChilds.Count;
            }
        }

        /// <summary>
        /// Returns the child node of a node
        /// </summary>
        /// <param name="index">Contents of the child node</param>
        /// <returns>An object of class TreeNode representing the node</returns>
        public TreeNode getChild(int index)
        {
            return (TreeNode)mChilds[index];
        }

        /// <summary>
        /// Attribute that are connected to Node
        /// </summary>
        public Attribute attribute
        {
            get
            {
                return mAttribute;
            }
        }

        /// <summary>
        /// Returns the son of a node by the name of the branch leading to it
        /// </summary>
        /// <param name="branchName">Name of branch</param>
        /// <returns>Tree node</returns>
        public TreeNode getChildByBranchName(double branchName)
        {
            int index = mAttribute.indexValue(branchName);
            if (index < 0)
                index = getClosestIndex(mAttribute, branchName);
            return (TreeNode)mChilds[index];
        }


        public int getClosestIndex(Attribute attribute, double value)
        {
            int iLenght = attribute.values.Length;
            double dDiff = Math.Abs(attribute.values[0] - value);
            double dTempDiff = 0.0;
            int iIndex = 0;
            
            for (int i = 1; i < iLenght; i++)
            {
                dTempDiff = Math.Abs(attribute.values[i] - value);
                if (dDiff > dTempDiff)
                {
                    dDiff = dTempDiff;
                    iIndex = i;
                }
            }

            return iIndex;
        }
    }
}
