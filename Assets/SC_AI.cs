using UnityEngine;
using System;
using Perceptron;

namespace SCCoreSystems
{
    public class SC_AI : MonoBehaviour
    {
        public const int inputsNumber = 3;

        public const int SC_Angle_Divider = 4;
        public const int SC_anglesNumber = 360;
        public const int errormargin = 5;

        public const int SC_anglesQuarterNumber = SC_anglesNumber * SC_Angle_Divider;
        public const int weightsNumber = inputsNumber;
        float[][] saveCurrentWeightForTheCurrentAngleInsideOfUnitsOf360 = new float[SC_anglesQuarterNumber][];

        public int swtchwaypointtype = 0;
        Transform northpoletransform;
        Transform compasspivot;
        public Vector2 waypointpos;
        Perceptron.Perceptron perc;
        float[] weights;
        float xmin, xmax, ymin, ymax;
        Trainer[] training = new Trainer[inputsNumber];
        public int _guessedCorrectRight = 0;
        public int _guessedCorrectLeft = 0;
        public float _dotGoal;
        int answer;
        System.Random random;
        float lastAngle = 0;
        Vector2 northpolepos;
        Vector2 compassPos;

        public SC_AI(Transform compass, Transform northpole, int maxPerceptronInstancesneurons, float perceptronLearningRate)
        {
            this.northpoletransform = northpole;
            this.compasspivot = compass;
            Starter(maxPerceptronInstancesneurons, perceptronLearningRate);
        }

        void Starter(int maxPerceptronInstancesneurons,float perceptronLearningRate)
        {
            random = new System.Random();
            perc = new Perceptron.Perceptron(maxPerceptronInstancesneurons, perceptronLearningRate);

            //perceptronLearningRate = sc_maths.getSomeRandNumThousandDecimal();

            for (int i = 0; i < saveCurrentWeightForTheCurrentAngleInsideOfUnitsOf360.Length; i++)
            {
                weights = perc.GetWeights();
                saveCurrentWeightForTheCurrentAngleInsideOfUnitsOf360[i] = weights;
            }
        }

        public void UpdatePerceptron()
        {
            _guessedCorrectRight = 0;
            _guessedCorrectLeft = 0;

            compassPos = new Vector2(compasspivot.transform.position.x, compasspivot.transform.position.y);

            var angle = sc_maths.ClampValue(compasspivot.transform.eulerAngles.z, 0, SC_anglesQuarterNumber);
            var angleRounded = Mathf.Round(angle);
            var currentDiff = (angle - angleRounded);
            var currentQuarterRoundedAngle = Mathf.Round(currentDiff * SC_Angle_Divider) / SC_Angle_Divider;
            currentQuarterRoundedAngle *= 100;
            currentQuarterRoundedAngle = (angle * SC_Angle_Divider);
            currentQuarterRoundedAngle = sc_maths.ClampValue(currentQuarterRoundedAngle, 0, SC_anglesQuarterNumber);

            weights = perc.GetWeights();

            if ((int)currentQuarterRoundedAngle < saveCurrentWeightForTheCurrentAngleInsideOfUnitsOf360.Length)
            {
                perc._SC_Perceptron_SetRotWeights(saveCurrentWeightForTheCurrentAngleInsideOfUnitsOf360[(int)currentQuarterRoundedAngle]);

                float pointForwardDirNPCX = (float)(1 * Math.Cos(Math.PI * angle / 180.0)) + compassPos.x; // * Math.PI / 180
                float pointForwardDirNPCY = (float)(1 * Math.Sin(Math.PI * angle / 180.0)) + compassPos.y;

                Vector2 dirRightNPC = new Vector2(pointForwardDirNPCY - compassPos.y, -1 * (pointForwardDirNPCX - compassPos.x));
                Vector2 dirNPCToPlayer = new Vector2(northpolepos.x - compassPos.x, northpolepos.y - compassPos.y);

                var someOtherMAG = (float)Math.Sqrt((dirNPCToPlayer.x * dirNPCToPlayer.x) + (dirNPCToPlayer.y * dirNPCToPlayer.y));
                dirNPCToPlayer.x /= someOtherMAG;
                dirNPCToPlayer.y /= someOtherMAG;

                if (swtchwaypointtype == 0)
                {
                    Vector2 dirbulletprimerright = new Vector2(compasspivot.transform.right.x, compasspivot.transform.right.y);
                    dirbulletprimerright.Normalize();

                    Vector2 dirbulletprimerforward = new Vector2(compasspivot.transform.up.x, compasspivot.transform.up.y);
                    dirbulletprimerforward.Normalize();
                    Vector2 dirprimertonorthpoletransform = new Vector2(northpoletransform.position.x, northpoletransform.position.y) - new Vector2(compasspivot.position.x, compasspivot.position.y);
                    dirprimertonorthpoletransform.Normalize();

                    _dotGoal = sc_maths.Dot(dirbulletprimerright.x, dirbulletprimerright.y, dirprimertonorthpoletransform.x, dirprimertonorthpoletransform.y);

                    if (_dotGoal >= 0.001f)
                    {
                        answer = 1;
                    }
                    else if (_dotGoal < -0.001f)
                    {
                        answer = -1;
                    }
                }

                for (int i = 0; i < training.Length; i++)
                {
                    double angleInRadians = random.Next(360) / (2 * Math.PI);

                    // randomly getting a point at the location of the compass
                    int x = (int)(0.001f * Math.Cos(angleInRadians) + compassPos.x); 
                    int y = (int)(0.001f * Math.Sin(angleInRadians) + compassPos.y);

                    training[i] = new Trainer(weightsNumber, x, y, answer);
                    perc.Train(training[i].inputs, training[i].answer0);
                }

                int guessedCorrect = 0;
                int guessedWrong = 0;

                int turnRight = 0;
                int turnLeft = 0;

                for (int i = 0; i < training.Length; i++)
                {
                    int guess = perc.Guess(training[i].inputs);
                    Vector2 neededPos = new Vector2(training[i].inputs[0], training[i].inputs[1]);

                    if (training[i].answer0 == 1)
                    {
                        turnRight++;
                    }
                    else if (training[i].answer0 == -1)
                    {
                        turnLeft++;
                    }

                    if (guess > 0)
                    {

                        guessedCorrect++;
                    }
                    else
                    {
                        guessedWrong++;
                    }
                }

                if (guessedCorrect >= (training.Length * 0.5f) - errormargin && guessedCorrect <= (training.Length) ||
                    guessedWrong >= (training.Length * 0.5f) - errormargin && guessedWrong <= (training.Length))
                {
                    if (turnRight >= (training.Length * 0.5f) - errormargin && turnRight <= (training.Length) ||
                        turnLeft >= (training.Length * 0.5f) - errormargin && turnLeft <= (training.Length))
                    {
                        if (turnRight > turnLeft)
                        {
                            _guessedCorrectRight++;   
                        }
                        else if (turnRight < turnLeft)
                        {
                            _guessedCorrectLeft++;
                        }
                    }
                    else if (turnRight <= (training.Length * 0.5f) + errormargin && turnRight >= (training.Length) ||
                            turnLeft <= (training.Length * 0.5f) + errormargin && turnLeft >= (training.Length))
                    {
                        if (turnRight > turnLeft)
                        {
                            _guessedCorrectRight++;     
                        }
                        else if (turnRight < turnLeft)
                        {
                            _guessedCorrectLeft++;
                        }
                    }
                    else
                    {
                        Debug.Log("Data is too similar");
                    }
                }
                else if (guessedCorrect <= (training.Length * 0.5f) + errormargin && guessedCorrect >= (training.Length) ||
                        guessedWrong <= (training.Length * 0.5f) + errormargin && guessedWrong >= (training.Length))
                {

                    if (turnRight >= (training.Length * 0.5f) - errormargin && turnRight <= (training.Length) ||
                        turnLeft >= (training.Length * 0.5f) - errormargin && turnLeft <= (training.Length))
                    {
                        if (turnRight > turnLeft)
                        {
                            _guessedCorrectRight++;    
                        }
                        else if (turnRight < turnLeft)
                        {
                            _guessedCorrectLeft++;
                        }
                    }
                    else if (turnRight <= (training.Length * 0.5f) + errormargin && turnRight >= (training.Length) ||
                            turnLeft <= (training.Length * 0.5f) + errormargin && turnLeft >= (training.Length))
                    {
                        if (turnRight > turnLeft)
                        {
                            _guessedCorrectRight++;     
                        }
                        else if (turnRight < turnLeft)
                        {
                            _guessedCorrectLeft++;
                        }
                    }
                    else
                    {
                        Debug.Log("Data is too similar 1");
                    }
                }
                else
                {
                    Debug.Log("Data is too similar 0");
                }
                saveCurrentWeightForTheCurrentAngleInsideOfUnitsOf360[(int)currentQuarterRoundedAngle] = perc.GetWeights();
            }
            else
            {
                Debug.Log("out of range: " + currentQuarterRoundedAngle);
            }
        }
    }
}
