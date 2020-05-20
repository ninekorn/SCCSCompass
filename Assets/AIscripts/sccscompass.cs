//by steve chassé aka ninekorn

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SCCoreSystems
{
    public class sccscompass : MonoBehaviour
	{
        sccsaiguess[] SC_AI4LR = new sccsaiguess[maxPerceptronInstances];

        public Transform northpole; //2d game target direction bullseye
        public Transform compass; //2d game drone turret
        GameObject compassneedleobject;
        GameObject northpoleobject;

        public const int maxPerceptronInstances = 10;
        public const int maxPerceptronInstancesNeurons = 3; // 3 minimum i think
        float perceptronLearningRate = 0.001f;

        public float needle_rotation_speed = 0.5f;
        public int swtchwaypointtype = 0;

        int totalRight = 0;
        int totalLeft = 0;
        int frame4RandomRorL = 0;

        public float totalDotgoalRL = 0;
        float dottogoal = 0;

        Vector3 playerlocation = Vector2.zero;
        Vector3 directionright = Vector2.zero;
        Vector3 dirturrettoplayer = Vector2.zero;
        Vector3 dirturrettoenemy = Vector2.zero;

        public Vector3 needleScale = new Vector3(0.8f, 4, 0.8f);
        public Vector3 needlePosition = new Vector3(0, 2, 0);

        void Start()
        {
            compassneedleobject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            compassneedleobject.transform.localScale = needleScale;
            compass = compassneedleobject.transform;

            needlePosition.y += transform.position.y;
            compass.position = needlePosition;

            compass.parent = this.transform;

            if (swtchwaypointtype == 0) // Z AXIS
            {
                compass.GetComponent<MeshRenderer>().material.color = Color.blue;
            }
            else if (swtchwaypointtype == 1) // Y AXIS
            {
                compass.GetComponent<MeshRenderer>().material.color = Color.green;
            }
            else if (swtchwaypointtype == 2) // X AXIS
            {
                compass.GetComponent<MeshRenderer>().material.color = Color.red;
            }

            /*northpoleobject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            northpoleobject.transform.localScale = new Vector3(2, 2, 2);
            northpole = northpoleobject.transform;
            northpole.transform.position = new Vector3(6, 6, 0);*/

            for (int i = 0; i < SC_AI4LR.Length; i++)
            {
                SC_AI4LR[i] = new sccsaiguess(compass, northpole, maxPerceptronInstancesNeurons, perceptronLearningRate);
                SC_AI4LR[i].SC_anglesNumber = 360;
                SC_AI4LR[i].SC_Angle_Divider = 10;
                SC_AI4LR[i].weightsNumber = 10;
                SC_AI4LR[i].inputsNumber = 20; // minimum 3 for the Trainer class
                SC_AI4LR[i].errormargin = 5;
                SC_AI4LR[i].swtchwaypointtype = swtchwaypointtype;
            }
        }

        void Update()
        {
            if (swtchwaypointtype == 0) // Z AXIS ROTATION
            {
                Debug.DrawRay(this.transform.position, this.transform.up, Color.blue);
            }
            else if(swtchwaypointtype == 1) // Y AXIS ROTATION
            {
                Debug.DrawRay(this.transform.position, this.transform.right, Color.green);
            }
            else if (swtchwaypointtype == 2) // X AXIS ROTATION
            {
                Debug.DrawRay(this.transform.position, this.transform.up, Color.red);
            }

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

            if (swtchwaypointtype == 0)
            {
                //Vector3 eulerAngles = transform.eulerAngles;
                //eulerAngles.x = 0;
                //eulerAngles.y = gearLR.transform.eulerAngles.y;
                //transform.eulerAngles = eulerAngles;

                //Debug.Log("dot: " + totalDotgoalRL); 
                if (totalDotgoalRL < -0.0025f || totalDotgoalRL > 0.0025f)
                {
                    if (totalRight > totalLeft)
                    {
                        //Debug.Log("north pole is right");
                        transform.Rotate(new Vector3(0, 0, -needle_rotation_speed * Mathf.Abs(totalDotgoalRL)), Space.World);
                    }
                    else if (totalRight < totalLeft)
                    {
                        //Debug.Log("north pole is left");
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
                    //Debug.Log("found north pole / bullseye");
                }
            }
            else if (swtchwaypointtype == 1)
            {
                Vector3 eulerAngles = transform.eulerAngles;

                eulerAngles.x = 0;
                eulerAngles.z = 0;

                transform.eulerAngles = eulerAngles;

                //Debug.Log("dot: " + totalDotgoalRL); 
                if (totalDotgoalRL < -0.0025f || totalDotgoalRL > 0.0025f)
                {
                    if (totalRight > totalLeft)
                    {
                        //Debug.Log("north pole is right");
                        transform.Rotate(new Vector3(0, -needle_rotation_speed * Mathf.Abs(totalDotgoalRL), 0), Space.World);
                    }
                    else if (totalRight < totalLeft)
                    {
                        //Debug.Log("north pole is left");
                        transform.Rotate(new Vector3(0, needle_rotation_speed * Mathf.Abs(totalDotgoalRL), 0), Space.World);
                    }
                    else
                    {
                        if (frame4RandomRorL == 0)
                        {
                            transform.Rotate(new Vector3(0, -needle_rotation_speed * Mathf.Abs(totalDotgoalRL), 0), Space.World);
                        }
                        else
                        {
                            transform.Rotate(new Vector3(0, needle_rotation_speed * Mathf.Abs(totalDotgoalRL), 0), Space.World);
                        }
                    }
                }
                else
                {
                    //Debug.Log("found north pole / bullseye");
                }
            }
            else if (swtchwaypointtype == 2)
            {
                //Debug.Log("dot: " + totalDotgoalRL); 
                if (totalDotgoalRL < -0.0025f || totalDotgoalRL > 0.0025f)
                {
                    if (totalRight > totalLeft)
                    {
                        //Debug.Log("north pole is right");
                        transform.Rotate(new Vector3(-needle_rotation_speed * Mathf.Abs(totalDotgoalRL), 0, 0), Space.World);
                    }
                    else if (totalRight < totalLeft)
                    {
                        //Debug.Log("north pole is left");
                        transform.Rotate(new Vector3(needle_rotation_speed * Mathf.Abs(totalDotgoalRL), 0, 0), Space.World);
                    }
                    else
                    {
                        if (frame4RandomRorL == 0)
                        {
                            transform.Rotate(new Vector3(-needle_rotation_speed * Mathf.Abs(totalDotgoalRL), 0, 0), Space.World);
                        }
                        else
                        {
                            transform.Rotate(new Vector3(needle_rotation_speed * Mathf.Abs(totalDotgoalRL), 0, 0), Space.World);
                        }
                    }
                }
                else
                {
                    //Debug.Log("found north pole / bullseye");
                }
            }
            frame4RandomRorL++;
        }
    }
}