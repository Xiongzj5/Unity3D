using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour {

    public Transform player1;
    public GameObject shellPrefab;
    private Transform firePosition;
    public float shellSpeed = 10;
    private int framenumber = 0;
    // Use this for initialization
    void Start () {
        //StartCoroutine(Fire());
        firePosition = transform.Find("FirePosition");
    }

    // Update is called once per frame
    void Update()
    {
        framenumber++;
        if (player1 != null)
        {
            NavMeshAgent agent = this.GetComponent<NavMeshAgent>();
            agent.SetDestination(player1.position);
            if (Vector3.Distance(transform.position, player1.transform.position) < 20 && (framenumber % 24 == 0))
            {
                GameObject go = GameObject.Instantiate(shellPrefab, firePosition.position, firePosition.rotation) as GameObject;
                go.GetComponent<Rigidbody>().velocity = go.transform.forward * shellSpeed;
            }
        }
        else
        {
            NavMeshAgent agent = this.GetComponent<NavMeshAgent>();
            agent.velocity = Vector3.zero;
            agent.ResetPath();
        }
    }

}
