using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetworks
{
    /// <summary>
    /// A Neural Network Layer or collection of cells or neurons
    /// </summary>
    public class Neurons : System.Collections.CollectionBase
    {

        /// <summary>
        /// Adds a neuron to a Neural Network Layer or collection of cells
        /// </summary>
        /// <param name="newNeuron">
        /// The neuron to add to a Neural Network Layer
        /// </param>
        public virtual void Add(Neuron newNeuron)
        {
            //forward our Add method on to 
            //CollectionBase.IList.Add   
            this.List.Add(newNeuron);
        }

        /// <summary>
        /// Adds a neuron to a Neural Network Layer at a specific position
        /// </summary>
        /// <param name="index">
        /// The position where a neuron will be inserted
        /// </param>
        /// <param name="newNeuron">
        /// The neuron to add to a Neural Network Layer
        /// </param>
        public virtual void Insert(int index, Neuron newNeuron)
        {
            this.List.Insert(index, newNeuron);
        }

        /// <summary>
        /// ReadOnly indexer - retrieves the neuron at a 
        /// specific position or location in the Neural 
        /// Network Layer or Neural Network Collection
        /// </summary> 
        public virtual Neuron this[int Index]
        {
            get
            {
                //return the Neuron at IList[Index] 
                return (Neuron)this.List[Index];
            }
        }
    }
}
