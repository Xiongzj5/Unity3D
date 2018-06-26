using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TankMovement : NetworkBehaviour {
    public float speed = 5;
    public float angularSpeed = 30;
    private Rigidbody rigidbody;
    public float number = 1;  //增加一个玩家的编号，通过编号区分控制

    public AudioClip idleAduio;
    public AudioClip drivingAduio;
    private AudioSource audio;

	// Use this for initialization
	void Start () {
        rigidbody = this.GetComponent<Rigidbody>();
        audio = this.GetComponent<AudioSource>();
	}
    public override void OnStartLocalPlayer()
    {
        this.transform.position = new Vector3(-10,0,0);
        GetComponent<MeshRenderer>().material.color = Color.green;
    }
    void Update()
    {
        if (isLocalPlayer)
        {
            float v = Input.GetAxis("Verticalplayer1");
            rigidbody.velocity = transform.forward * v * speed;

            float h = Input.GetAxis("Horizontalplayer1");
            rigidbody.angularVelocity = transform.up * h * angularSpeed;

            if (Mathf.Abs(h) > 0.1 || Mathf.Abs(v) > 0.1)
            {
                audio.clip = drivingAduio;
                if (audio.isPlaying == false)
                    audio.Play();
            }
            else
            {
                audio.clip = idleAduio;
                if (audio.isPlaying == false)
                    audio.Play();
            }
        }
        else
        {
            float v = Input.GetAxis("Verticalplayer2");
            rigidbody.velocity = transform.forward * v * speed;

            float h = Input.GetAxis("Horizontalplayer2");
            rigidbody.angularVelocity = transform.up * h * angularSpeed;

            if (Mathf.Abs(h) > 0.1 || Mathf.Abs(v) > 0.1)
            {
                audio.clip = drivingAduio;
                if (audio.isPlaying == false)
                    audio.Play();
            }
            else
            {
                audio.clip = idleAduio;
                if (audio.isPlaying == false)
                    audio.Play();
            }
        }

    }
}
