using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FollowTarget : NetworkBehaviour {

    private Vector3 offset;
    private Camera camera;
    private GameObject[] player = new GameObject[2];
	// Use this for initialization
	void Start () { 
        player = GameObject.FindGameObjectsWithTag("Tank");
        camera = this.GetComponent<Camera>();
       
	}
	
	// Update is called once per frame
	void Update () {
        player = GameObject.FindGameObjectsWithTag("Tank");
        if (player[0] == null || player[1] == null) return;
        offset = transform.position - (player[0].transform.position + player[1].transform.position) / 2;
        transform.position = (player[0].transform.position + player[1].transform.position) / 2 + offset;
        float distance = Vector3.Distance(player[0].transform.position, player[1].transform.position);
        float size = distance * 0.8f;
        camera.orthographicSize = size;
    }
}
