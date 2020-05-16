using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace SCCoreSystems
{
    public static class sc_maths
    {


        static System.Random randomer;
        public static double getSomeRandNum()
        {
            var num = (Mathf.Floor((float)randomer.NextDouble() * 999999999)) + 1; // this will get a number between 1 and 99;
            num *= Mathf.Floor((float)randomer.NextDouble() * 2) == 1 ? 1 : -1; // this will add minus sign in 50% of cases
            if (num == 0)
            {
                return getSomeRandNum();
            }
            return num * 0.000000001;
        }

        public static  float getSomeRandNumThousandDecimal()
        {
            var num = Mathf.Floor((float)randomer.NextDouble() * 999) + 1; // this will get a number between 1 and 99;
            num *= Mathf.Floor((float)randomer.NextDouble() * 2) == 1 ? 1 : -1; // this will add minus sign in 50% of cases
            if (num == 0)
            {
                return (float)getSomeRandNum();
            }
            return (float)(num * 0.001);
        }

        public static float ClampValue(float value, float min, float max)
        {
            value = value % max;
            if (value < min)
            {
                return min;
            }
            else if (value > max)
            {
                return max;
            }

            return value;
        }


        public static float Dot(float aX, float aY, float bX, float bY)
        {
            return (aX * bX) + (aY * bY);
        }

        //https://pastebin.com/fAFp6NnN // Also found on the unity3D forums by aldonaletto or Bunny83 or Robertbu but i couldn't find it anymore.
        public static Vector3 _getDirection(Vector3 value, Quaternion rotation)
        {
            Vector3 vector;
            double num12 = rotation.x + rotation.x;
            double num2 = rotation.y + rotation.y;
            double num = rotation.z + rotation.z;
            double num11 = rotation.w * num12;
            double num10 = rotation.w * num2;
            double num9 = rotation.w * num;
            double num8 = rotation.x * num12;
            double num7 = rotation.x * num2;
            double num6 = rotation.x * num;
            double num5 = rotation.y * num2;
            double num4 = rotation.y * num;
            double num3 = rotation.z * num;
            double num15 = ((value.x * ((1f - num5) - num3)) + (value.y * (num7 - num9))) + (value.z * (num6 + num10));
            double num14 = ((value.x * (num7 + num9)) + (value.y * ((1f - num8) - num3))) + (value.z * (num4 - num11));
            double num13 = ((value.x * (num6 - num10)) + (value.y * (num4 + num11))) + (value.z * ((1f - num8) - num5));
            vector.x = (float)num15;
            vector.y = (float)num14;
            vector.z = (float)num13;
            return vector;
        }
    }
}