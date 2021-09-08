using System;
using System.Collections;


namespace NeuralNetworks
{

    /// <summary>
    /// MultiLayer FeedForward Neural Network With BackPropagation Algorithm
    /// </summary>
    public class NeuralNetwork
    {

        /// <summary>
        /// Static variable for random numbers
        /// </summary>
        static System.Random rand;

        /// <summary>
        /// Static variable for number of neurons or cells in INPUT LAYER 
        /// </summary>
        public static int input_count = 3;

        /// <summary>
        /// Static variable for number of neurons or cells in HIDDEN LAYER
        /// </summary>
        public static int hidden_count = 2;

        /// <summary>
        /// Static variable for number of neurons or cells in OUTPUT LAYER
        /// </summary>
        public static int output_count = 1;

        /// <summary>
        /// Static variable for the number of processing cycles of the Neural Network 
        /// </summary>
        public static int iterations = 75;

        /// <summary>
        /// Represents the LEARNING RATE used in gradient descent to 
        /// prevent weights from converging at sub-optimal solutions.
        /// </summary>
        public double learning_rate = 0.8;

        /// <summary>
        /// A two dimensional array of input data from a training sample
        /// An INPUT represented as double [,] inputDataArray = new Double [,] 
        ///	{{0.20, 0.80}, {0.80, 0.4}} would represent 2 training instances
        ///	for an INPUT LAYER with 2 NEURONS or CELLS
        /// </summary>
        public double[,] input_data;

        /// <summary>
        /// A two dimensional array of output data from a training sample
        /// An OUTPUT represented as double [,] outputDataArray = new Double [,] 
        ///	{{0}, {1}} would represent 2 training instances
        ///	for an OUTPUT LAYER with 1 NEURON or CELL
        /// </summary>
        public double[,] output_data;


        /// <summary>
        ///	A collection of neurons representing the input layer
        /// </summary>      
        public Neurons input;

        /// <summary>
        /// A collection of neurons representing the hidden layer
        /// </summary>
        Neurons hidden;

        /// <summary>
        /// A collection of neurons representing the output layer
        /// </summary>
        public Neurons output;

        public bool b_IsTraining = false;

        /// <summary>
        /// Initializes the LAYERS in the NEURAL NETWORK
        /// </summary>
        /// <param name="inputData">
        /// INPUTS data for the NEURONS in the INPUT LAYER
        /// </param>
        /// <param name="outputData">
        /// OUTPUT data for the NEURONS in the OUTPUT LAYER
        /// </param>
        /// <param name="hidden_layer_count">
        ///	The number of neurons in the HIDDEN LAYER  
        /// </param>                   
        public void Initialize(double[,] inputData, double[,] outputData, int hidden_layer_count)
        {
            this.input_data = inputData;

            input_count = (this.input_data.GetUpperBound(1) + 1);


            hidden_count = hidden_layer_count;


            this.output_data = outputData;

            output_count = (this.output_data.GetUpperBound(1) + 1);




            input = new Neurons();

            for (int i = 0; i < input_count; i++)
            {
                Neuron c = new Neuron(0, i);

                input.Add(c);
            }


            hidden = new Neurons();

            for (int i = 0; i < hidden_count; i++)
            {
                Neuron c = new Neuron(1, i);

                hidden.Add(c);
            }


            output = new Neurons();

            for (int i = 0; i < output_count; i++)
            {
                Neuron c = new Neuron(2, i);

                output.Add(c);
            }
        }


        /// <summary>
        /// Initializes the NEURAL NETWORK with training data input
        /// or a real world data input for classification.
        /// FeedForward feeds the INPUT to the INPUT LAYER neurons
        /// FeedForward feeds the OUTPUT from the INPUT LAYER to the HIDDEN LAYER
        /// FeedForward feeds the OUTPUT from the HIDDEN LAYER to the OUTPUT LAYER
        /// FeedForward uses the Sigmoid Function or Logistic Function to calculate  
        /// the OUTPUT from the INPUT in the HIDDEN and OUTPUT LAYERS
        /// </summary>
        /// <param name="sampleNumber">
        /// A numeric ordered value representing the training or 
        /// classification instance. If the dataset contains 10 
        /// instances or rows, the first row has a sampleNumber = 0
        /// and the last row or instance has a sample number = 9
        /// </param>
        public virtual void FeedForward(int sampleNumber)
        {
            double total;

            Neuron ch = null;

            Neuron ci = null;

            Neuron co = null;


            //feed the input data to the input Neurons layer
            for (int i = 0; i < input.Count; i++)
            {
                ci = input[i];

                ci.Input = this.input_data[sampleNumber, i];
            }


            //feedforward from input to hidden Neurons 
            for (int h = 0; h < hidden.Count; h++)
            {
                total = 0.0;

                ch = hidden[h];

                for (int i = 0; i < input.Count; i++)
                {
                    ci = input[i];

                    total = total + (ci.Output * ch.Weight[i]);
                }

                ch.Input = total + ch.Bias;
            }

            //feedforward from hidden to output Neurons 
            for (int o = 0; o < output.Count; o++)
            {
                total = 0.0;

                co = output[o];

                //feed the expected training result to the output Neurons layer
                if(b_IsTraining == true)
                    co.OutputTraining = this.output_data[sampleNumber, o];

                for (int h = 0; h < hidden.Count; h++)
                {

                    ch = hidden[h];

                    total = total + (ch.Output * co.Weight[h]);
                }

                co.Input = total + co.Bias;
            }

        }
        
        
        /// <summary>
        /// Recalculates the BIAS and ERROR in the HIDDEN LAYER
        /// and OUTPUT LAYER. Adjustes the WEIGHTS between the 
        /// OUTPUT LAYER and HIDDEN LAYER and between the HIDDEN
        /// LAYER and the INPUT LAYER using the derivative of
        /// the Sigmoid Function or Logistic Function
        /// </summary>
        public virtual void BackPropagate()
        {
            double total;

            Neuron ch = null;

            Neuron ci = null;

            Neuron co = null;

            //calculate error rate for Output layer 
            for (int o = 0; o < output.Count; o++)
            {
                co = output[o];

                co.Error = co.LogisticFunctionDerivative(co.Output) * (co.OutputTraining - co.Output);
            }

            //error from output to hidden layer
            for (int h = 0; h < hidden.Count; h++)
            {
                total = 0.0;

                ch = hidden[h];

                for (int o = 0; o < output.Count; o++)
                {
                    co = output[o];

                    total = total + (co.Error * co.Weight[o]);
                }

                ch.Error = (ch.LogisticFunction(ch.Output) * total);
            }

            //update all weights in the network 
            //from output Neurons to hidden Neurons 
            for (int o = 0; o < output.Count; o++)
            {
                co = output[o];

                for (int h = 0; h < hidden.Count; h++)
                {

                    ch = hidden[h];

                    co.Weight[h] = co.Weight[h] + (this.learning_rate * co.Error * ch.Output);
                }

                co.Bias = co.Bias + (this.learning_rate * co.Error);
            }


            //update all weights in the network 
            //from hidden Neurons to input Neurons 
            for (int h = 0; h < hidden.Count; h++)
            {
                ch = hidden[h];

                for (int i = 0; i < input.Count; i++)
                {
                    ci = input[i];

                    ch.Weight[i] = ch.Weight[i] + (this.learning_rate * ch.Error * ci.Output);
                }

                ch.Bias = ch.Bias + (this.learning_rate * ch.Error);
            }
        }


        /// <summary> 
        /// Generates random double values between -1.0 and +1.0
        /// </summary> 
        /// <returns>
        /// System.Double data type between -1.0 and +1.0 
        /// </returns> 
        public static double Random
        {
            get
            {
                if (rand == null)
                {
                    rand = new System.Random();
                }

                int MaxLimit = +1000;

                int MinLimit = -1000;

                double number = (double)(rand.Next(MinLimit, MaxLimit)) / 2000;

                return number;
            }
        }

    }
}

