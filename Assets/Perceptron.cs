using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perceptron
{
    public class Perceptron
    {
        float[] weights;
        public float learningRate;
        static public Random r = new Random();

        public Perceptron(int n, float rate)
        {
            this.learningRate = rate;

            this.weights = new float[n];
            // Start with random weights
            for (int i = 0; i < n; i++)
            {
                this.weights[i] = (float)r.NextDouble() * 4 - 1; // range <-1:1>
            }
        }

        public void _SC_Perceptron_SetRotWeights(float[] w)
        {
            weights = w;
        }

        public void Train(float[] inputs, int desired)
        {
            int guess = this.Guess(inputs);
            float error = desired - guess;

            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] += inputs[i] * this.learningRate * error;
            }

            /*// if the result does not match expected
            if (result != expectedResult)
            {
                // calculate error (need to convert boolean to a number
                double error = (expectedResult ? 1 : 0) - (result ? 1 : 0);
                for (int i = 0; i < Weights.Length; i++)
                {
                    // adjust the weights
                    Weights[i] += LearningRate * error * inputs[i];
                }
            }*/
        }

        public int Guess(float[] inputs)
        {
            float sum = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                sum += inputs[i] * weights[i];
            }
            return this.Activate(sum);
        }

        private int Activate(float sum)
        {
            if (sum > 0)
                return 1;
            else
                return -1;
        }

        public float[] GetWeights()
        {
            return this.weights;
        }
    }
}
