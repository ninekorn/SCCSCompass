using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//https://answers.unity.com/questions/416169/finding-pitchrollyaw-from-quaternions.html



public class copyRotation : MonoBehaviour
{
    public Transform bullseye;
    public Transform turretPivot;
    public Transform turretBarrel;

    public Transform compassRL;
    public Transform compassTB;
    public Transform compassFB;

    Vector3 eulerAnglesRL = Vector3.zero;
    //Vector3 eulerAnglesFB = Vector3.zero;
    //Vector3 eulerAnglesTB = Vector3.zero;

    float lastEulersFB = 0.0f;
    float lastEulersTB = 0.0f;

    float lastTargetPosX = 0.0f;
    float lastTargetPosZ = 0.0f;


    float lastTargetposx = 0.0f;
    float lastTargetposy = 0.0f;
    float lastTargetposz = 0.0f;


    void Start ()
    {
		
	}
	
	void Update ()
    {

        eulerAnglesRL = turretPivot.transform.eulerAngles;
        eulerAnglesRL.y = compassRL.transform.eulerAngles.y;

        turretPivot.transform.eulerAngles = eulerAnglesRL;


        /*if (compassTB.transform.eulerAngles.z > compassFB.transform.eulerAngles.x)
        {
            eulerAnglesRL = turretBarrel.transform.eulerAngles;
            eulerAnglesRL.x = compassTB.transform.eulerAngles.z;
            turretBarrel.transform.eulerAngles = eulerAnglesRL;
        }
        else
        {
            eulerAnglesRL = turretBarrel.transform.eulerAngles;
            eulerAnglesRL.x = compassFB.transform.eulerAngles.x;
            turretBarrel.transform.eulerAngles = eulerAnglesRL;
        }*/



        /*if (bullseye.transform.position.z != lastTargetPosZ)
        {
            eulerAnglesRL = turretBarrel.transform.eulerAngles;
            eulerAnglesRL.x = compassTB.transform.eulerAngles.z;
            turretBarrel.transform.eulerAngles = eulerAnglesRL;
        }*/

        /*if (bullseye.transform.position.x != lastTargetPosX)
        {
            eulerAnglesRL = turretBarrel.transform.eulerAngles;
            eulerAnglesRL.x = compassFB.transform.eulerAngles.x;
            turretBarrel.transform.eulerAngles = eulerAnglesRL;
        }*/



        /*if (bullseye.transform.position.z != lastTargetPosZ)
        {

        }*/

        //lastEulersFB = compassRL.transform.eulerAngles.x;
        //lastEulersTB = compassRL.transform.eulerAngles.z;

        /*if (lastTargetposy != bullseye.transform.position.y)
        {
            //Debug.Log("diff height");
        }*/
        /**/


        //var right = compassTB.transform.right;
        //right.y = 0;
        //right *= Mathf.Sign(compassTB.transform.up.y);
        //var fwd = Vector3.Cross(right, Vector3.up).normalized;
        //float pitch = Vector3.Angle(fwd, compassTB.transform.forward) * Mathf.Sign(compassTB.transform.forward.y);
        //Debug.Log(pitch);



        Quaternion quatTB = compassTB.transform.rotation;
        float xq = quatTB.x;
        float yq = quatTB.y;
        float zq = quatTB.z;
        float wq = quatTB.w;

        var pitcha = (float)Mathf.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Mathf.PI);
        var yawa = (float)Mathf.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Mathf.PI);
        var rolla = (float)Mathf.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180 / Mathf.PI);








        /*if (lastTargetposx != bullseye.transform.position.x)
        {
            eulerAnglesRL = turretBarrel.transform.eulerAngles;

            if (Mathf.Abs(compassTB.transform.eulerAngles.z) > Mathf.Abs(compassFB.transform.eulerAngles.x))
            {
                eulerAnglesRL.z = compassTB.transform.eulerAngles.z;
                turretBarrel.transform.eulerAngles = eulerAnglesRL;
                Debug.Log("diff z");
            }
            else
            {

                eulerAnglesRL.z = compassFB.transform.eulerAngles.x;
                turretBarrel.transform.eulerAngles = eulerAnglesRL;
                Debug.Log("diff x");
            }
        }*/
        /*else if (lastTargetposz != bullseye.transform.position.z)
        {
            eulerAnglesRL = turretBarrel.transform.eulerAngles;
   
            Debug.Log("diff x");
        }*/


        lastTargetposx = bullseye.transform.position.x;
        lastTargetposy = bullseye.transform.position.y;
        lastTargetposz = bullseye.transform.position.z;
    }
}
