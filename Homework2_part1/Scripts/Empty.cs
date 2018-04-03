using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Empty : MonoBehaviour {
	public GameObject ob;
	// Use this for initialization
	void Start () {
		this.transform.position = ob.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = ob.transform.position;

	}
}
