using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GameObject.Find("Mercury").transform.RotateAround(Vector3.zero, new Vector3(1, 1, 0), 17 * Time.deltaTime);  
		GameObject.Find("Mercury").transform.Rotate(Vector3.up * Time.deltaTime * 200);  

		GameObject.Find("Venus").transform.RotateAround(Vector3.zero, new Vector3(1, 0, 1), 22 * Time.deltaTime);  
		GameObject.Find("Venus").transform.Rotate(Vector3.up * Time.deltaTime * 200);  

		GameObject.Find("Earth").transform.RotateAround(Vector3.zero, new Vector3(0, 1, 1), 34 * Time.deltaTime);  
		GameObject.Find("Earth").transform.Rotate(Vector3.up * Time.deltaTime * 200);  

		GameObject.Find("Mars").transform.RotateAround(Vector3.zero, new Vector3(0, 1, 0), 43 * Time.deltaTime);  
		GameObject.Find("Mars").transform.Rotate(Vector3.up * Time.deltaTime * 200);  

		GameObject.Find("Jupiter").transform.RotateAround(Vector3.zero, new Vector3(1, 2, 0), 35 * Time.deltaTime);  
		GameObject.Find("Jupiter").transform.Rotate(Vector3.up * Time.deltaTime * 200); 

		GameObject.Find("Saturn").transform.RotateAround(Vector3.zero, new Vector3(1, 0, 0), 42 * Time.deltaTime);  
		GameObject.Find("Saturn").transform.Rotate(Vector3.up * Time.deltaTime * 200);

		GameObject.Find("Uranus").transform.RotateAround(Vector3.zero, new Vector3(1, 0, 2), 20 * Time.deltaTime);  
		GameObject.Find("Uranus").transform.Rotate(Vector3.up * Time.deltaTime * 200);  

		GameObject.Find("Neptune").transform.RotateAround(Vector3.zero, new Vector3(0, 2, 1), 30 * Time.deltaTime);  
		GameObject.Find("Neptune").transform.Rotate(Vector3.up * Time.deltaTime * 200); 
	}
}
