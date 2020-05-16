

namespace Perceptron
{
    public class Trainer
    {
        public float[] inputs;
        public int answer0;

        public Trainer(int neurons,float x, float y, int a0)
        {
            inputs = new float[neurons];
            inputs[0] = x;
            inputs[1] = y;
            inputs[2] = 1;
            answer0 = a0;
        }
    }
}
