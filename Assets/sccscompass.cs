using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCCoreSystems
{
    public class sccscompass : MonoBehaviour
    {
        SC_AI[] SC_AI4LR = new SC_AI[maxPerceptronInstances];

        public Transform northpole; //2d game target direction bullseye
        public Transform compass; //2d game drone turret
        GameObject compassneedleobject;
        GameObject northpoleobject;

        public const int maxPerceptronInstances = 10;
        public const int maxPerceptronInstancesNeurons = 3; // 3 minimum i think
        float perceptronLearningRate = 0.001f;

        public float needle_rotation_speed = 0.5f;

        int totalRight = 0;
        int totalLeft = 0;
        int frame4RandomRorL = 0;

        public float totalDotgoalRL = 0;
        float dottogoal = 0;

        Vector3 playerlocation = Vector2.zero;
        Vector3 directionright = Vector2.zero;
        Vector3 dirturrettoplayer = Vector2.zero;
        Vector3 dirturrettoenemy = Vector2.zero;

        void Start()
        {
            compassneedleobject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            compassneedleobject.transform.localScale = new Vector3(0.1f, 0.5f, 0.1f);
            compass = compassneedleobject.transform;
            compass.position = new Vector3(0, 0.25f,0);
            compass.parent = this.transform;

            northpoleobject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            northpoleobject.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            northpole = northpoleobject.transform;
            northpole.transform.position = new Vector3(2, 2, 0);

            for (int i = 0; i < SC_AI4LR.Length; i++)
            {
                SC_AI4LR[i] = new SC_AI(compass, northpole, maxPerceptronInstancesNeurons, perceptronLearningRate);
                SC_AI4LR[i].SC_anglesNumber = 360;
                SC_AI4LR[i].SC_Angle_Divider = 4;
                SC_AI4LR[i].errormargin = 1;
                SC_AI4LR[i].swtchwaypointtype = 0;
            }
        }

        void Update()
        {
            totalRight = 0;
            totalLeft = 0;
            totalDotgoalRL = 0;
            for (int i = 0; i < SC_AI4LR.Length; i++)
            {
                SC_AI4LR[i].UpdatePerceptron();
                totalRight += SC_AI4LR[i]._guessedCorrectRight;
                totalLeft += SC_AI4LR[i]._guessedCorrectLeft;
                totalDotgoalRL += SC_AI4LR[i]._dotGoal;
            }
            totalDotgoalRL /= SC_AI4LR.Length;




            Debug.Log("dot: " + totalDotgoalRL); 
            if (totalDotgoalRL < 0 || totalDotgoalRL > 0) //0.01f //0.023454321
            {
                /*if (totalDotgoalRL < 0.35)
                {
                    totalDotgoalRL = 1 - totalDotgoalRL;
                }*/
                if (totalRight > totalLeft)
                {
                    Debug.Log("north pole is right");
                    transform.Rotate(new Vector3(0, 0, -needle_rotation_speed * Mathf.Abs(totalDotgoalRL)), Space.World);
                }
                else if (totalRight < totalLeft)
                {
                    Debug.Log("north pole is left");
                    transform.Rotate(new Vector3(0, 0, needle_rotation_speed * Mathf.Abs(totalDotgoalRL)), Space.World);
                }
                else
                {
                    if (frame4RandomRorL == 0)
                    {
                        transform.Rotate(new Vector3(0, 0, (-needle_rotation_speed * Mathf.Abs(totalDotgoalRL))), Space.World);
                    }
                    else
                    {
                        transform.Rotate(new Vector3(0, 0, (needle_rotation_speed * Mathf.Abs(totalDotgoalRL))), Space.World);
                    }
                }
            }
            else
            {
                Debug.Log("found north pole / bullseye");
            }

            frame4RandomRorL++;
        }
    }
}