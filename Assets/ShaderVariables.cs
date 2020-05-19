using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Jiexi's script
//https://answers.unity.com/questions/543558/moving-vertices-in-shaders.html
public class ShaderVariables : MonoBehaviour {

	// Use this for initialization
	GameObject target;
	Shader targetShader;
	Material mat;
	float desired = 5.0f;

	void Start () {
		target = this.gameObject;//GameObject.Find("Plane"); //get my plane, I will do this later on by creating primitives.
		targetShader = target.GetComponent<MeshRenderer>().material.shader; //get the shader, this is handy if you want to switch shaders. 
		mat = target.GetComponent<Renderer>().material;    //and get my material, where I can send my variables to
	}

	// Update is called once per frame
	void Update () 
	{
		float tempVar = mat.GetFloat("_Amount"); //ask for the amount of the float
		Debug.Log( mat.GetFloat("_Amount") );     //Debugging to check


		mat.SetFloat("_Amount", tempVar+= Time.deltaTime); 

		/*if(mat.GetFloat("_Amount") < desired)    //and if "_Amount" is less than desired
		{
			mat.SetFloat("_Amount", tempVar+=0.1f);     //I will increase the amount by 0.1 (you can manipulate 0.1 to change the grow speed;
		}*/
	}
}
