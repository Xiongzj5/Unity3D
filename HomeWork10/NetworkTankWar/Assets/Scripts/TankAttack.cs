using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class TankAttack : NetworkBehaviour {

    public GameObject shellPrefab;
    private Transform firePosition;
    public KeyCode fireKey = KeyCode.Space;
    public float shellSpeed = 10;
    public AudioClip shotAudio;
    
    
    // Use this for initialization
	void Start () {
        firePosition = transform.Find("FirePosition");
	}
	[Command]
    void CmdFire()
    {
        AudioSource.PlayClipAtPoint(shotAudio, transform.position);
        GameObject go = GameObject.Instantiate(shellPrefab, firePosition.position, firePosition.rotation) as GameObject;
        go.GetComponent<Rigidbody>().velocity = go.transform.forward * shellSpeed;
        NetworkServer.Spawn(go);
        Destroy(go, 2.0f);
    }

    // Update is called once per frame
    void Update () {
        if (isLocalPlayer)
        {
            fireKey = KeyCode.Space;
            if (Input.GetKeyDown(fireKey))
            {
                CmdFire();

            }
        }
        else
        {
            fireKey = KeyCode.KeypadEnter;
            if (Input.GetKeyDown(fireKey))
            {
                CmdFire();

            }
        }
	}
}
