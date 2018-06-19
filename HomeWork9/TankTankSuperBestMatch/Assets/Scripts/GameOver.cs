using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {
    public GameObject player1;
    public GameObject player2;
    public Text gameInfo;
	// Use this for initialization
	void Start () {
        gameInfo.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		if(player1 == null || player2 == null)
        {
            if(player1 == null)
            {
                gameInfo.text = "You Lose!";
            }
            else
            {
                gameInfo.text = "You Win!";
            }
        }
	}
}
