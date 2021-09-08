using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetworks
{
    /// <summary>
    /// A Neuron is the basic building block of a Neural Network
    /// </summary>
    public class Neuron
    {

        /// <summary>
        /// Public constructor for initializing a neuron  
        /// </summary>
        /// <param name="layer">Input Layer = 0, Hidden Layer = 1 and Output Layer = 2</param>
        /// <param name="index">A number assigned to a cell for ordering the cell or identifying it</param>
        public Neuron(int layer, int index)
        {

            this.player = layer;

            this.pindex = index;

            //player = 0 is the input layer, however, only hidden and output
            //layers initialize weights and bias to random values
            if (this.player > 0)
            {
                pbias = NeuralNetwork.Random;

                if (this.player == 2)
                {
                    this.pweight = new double[NeuralNetwork.hidden_count];
                }
                else
                {
                    this.pweight = new double[NeuralNetwork.input_count];
                }


                for (int i = 0; i < this.Weight.Length; i++)
                {
                    pweight[i] = NeuralNetwork.Random;

                    System.Diagnostics.Debug.WriteLine("Layer : " + this.player + " Weight " + pweight[i]);
                }

            }
        }



        /// <summary>
        /// Internal storage for Neuron.Layer public property
        /// </summary>
        protected int player;
        /// <summary>
        /// INPUT LAYER = 0, HIDDEN LAYER = 1, OUTPUT LAYER = 2
        /// </summary>
        public int Layer
        {
            get
            {
                return this.player;
            }
            set
            {
                this.player = value;
            }
        }

        /// <summary>
        /// Internal storage for Neuron.Index public property
        /// </summary>
        protected int pindex;
        /// <summary>
        /// Identifies a Neuron or the position of a Neuron in a LAYER
        /// </summary>
        public int Index
        {
            get
            {
                return this.pindex;
            }
            set
            {
                this.pindex = value;
            }
        }

        /// <summary>
        /// Internal storage for Neuron.Input public property
        /// </summary>
        protected double pinput;
        /// <summary>
        /// Input data fed to the Neural Network
        /// </summary>
        public double Input
        {
            get
            {
                return this.pinput;
            }
            set
            {
                this.pinput = value;

                //for input layers, neuron input = neuron output while 
                //an activation function is applied to get an output neuron
                //in hidden and output layers  
                if (this.player == 0)
                {
                    this.poutput = this.pinput;
                }
                else
                {
                    this.poutput = this.LogisticFunction(this.pinput);

                    //System.Diagnostics.Debug.WriteLine("Layer : " + this.player + " Input " + this.pinput + " Output " + this.poutput);				
                }
            }
        }

        /// <summary>
        /// Internal storage for Neuron.Output public property
        /// </summary>
        protected double poutput;
        /// <summary>
        /// Calculated Ouput from the INPUT, HIDDEN or OUTPUT LAYER
        /// </summary>
        public double Output
        {
            get
            {
                return this.poutput;
            }
            set
            {
                this.poutput = value;
            }
        }

        /// <summary>
        /// Internal storage for Neutron.OutputTraining public property
        /// </summary>
        protected double poutputTraining;
        /// <summary>
        /// Expected or Target or Learning output for a neutron
        /// in the OUPUT LAYER
        /// </summary>
        public double OutputTraining
        {
            get
            {
                return this.poutputTraining;
            }
            set
            {
                this.poutputTraining = value;
            }
        }

        /// <summary>
        /// Internal storage for Network.Weight public property  
        /// </summary>
        protected double[] pweight;
        /// <summary>
        /// Storage for array of weights from OUTPUT to 
        /// HIDDEN LAYER and from HIDDENLAYER TO INPUT 
        /// LAYER. Each Neuron in the OUTPUT and HIDDEN 
        /// LAYER is connected to an array of Neurons
        /// </summary>
        public double[] Weight
        {
            get
            {
                return this.pweight;
            }
        }

        /// <summary>
        /// Internal storage for Network.Bias public property
        /// </summary>        
        protected double pbias;
        /// <summary> 
        /// Varies the OUTPUT of a Neuron in a HIDDEN or OUTPUT LAYER 
        /// </summary> 
        public double Bias
        {
            get
            {
                return this.pbias;
            }
            set
            {
                this.pbias = value;
            }
        }

        /// <summary>
        /// Internal storage for Network.Error public property
        /// </summary>
        protected double perror;
        /// <summary>
        /// Error = (ACTUALOUTPUT * (1 - ACTUALOUTPUT) * (EXPECTEDOUTPUT - ACTUALOUTPUT)) 
        /// </summary> 
        public double Error
        {
            get
            {
                return this.perror;
            }
            set
            {
                this.perror = value;
            }
        }

        /// <summary> 
        /// Sigmoid Function or Logistic Function or Activation Function
        /// is applied to the INPUT to a NETWORK LAYER to get an OUTPUT
        /// </summary> 
        /// <param name="val">
        /// The value to calculate a Sigmoid or Logistic Function for
        /// </param> 
        /// <returns>
        /// System.Double datatype of the Logistic or Sigmoid Function result
        /// </returns> 
        public virtual double LogisticFunction(double val)
        {
            return (1.0 / (1.0 + System.Math.Exp(-val)));
        }


        /// <summary>
        /// Derivative of the Sigmoid Function or Logistic Function or Activation Function
        /// The derivative of the Logistic Activation Function is represented with 
        /// F(x) = F(x)*(1- F(x)) where F(x) is represented by 1/( 1 - e-x ). 
        /// </summary>
        /// <param name="val">
        /// The value to calculate the derivative of the Sigmoid Function for
        /// </param>
        /// <returns>
        /// System.Double datatype of the numeric result
        /// </returns>
        public virtual double LogisticFunctionDerivative(double val)
        {
            double sigmoidValue = 0.0;

            sigmoidValue = this.LogisticFunction(val);

            return (sigmoidValue * (1 - sigmoidValue));
        }
    }
}
