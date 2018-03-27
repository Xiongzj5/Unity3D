using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFile : MonoBehaviour {
	private int whoseturn = 0;   //use 0 to express player1's turn and use 1 to express player2's turn   
	private int[,] chessBoard = new int[3,3];    /*use 0 to express unused area, use 1 to express area used 
	by user1, use 2 to express area used by user2 */ 
	public Texture2D BackGround_img;  
	public Texture2D Player1_img;  
	public Texture2D Player2_img;  




	// Use this for initialization  
	void Start () {  
		Reset ();  
	}  

	//every frame, we make chessboard dynamically
	//需要注意的是，我们是在每一帧都去检查棋盘中每个格子的状态，并在相应的格子进行相应的操作
	void OnGUI() {  
		GUIStyle fontStyle = new GUIStyle ();  
		GUIStyle fontStyle1 = new GUIStyle ();  
		GUIStyle fontStyle2 = new GUIStyle (); 

		fontStyle.fontSize = 40;  
		fontStyle1.normal.background = BackGround_img;  
		fontStyle2.fontSize = 30;  
		fontStyle2.normal.textColor = new Color (0, 0, 0);  

		GUI.Label (new Rect (0, 0, 1024, 781), "", fontStyle1);  
		GUI.Label (new Rect (295, 120, 100, 100), "Tic-Tac-Toe", fontStyle);  

		GUI.Label (new Rect (50, 150, 200, 100), Player1_img);  
		GUI.Label (new Rect (600, 150, 200, 100), Player2_img);  

		if (GUI.Button (new Rect (350, 500, 100, 50), "RESET"))
			Reset ();  
		int result = check (); 

		if (result == 1) {    
			GUI.Label (new Rect (50, 250, 100, 50), "Fire wins!", fontStyle2);    
		} else if (result == 2) {
			GUI.Label (new Rect (600, 250, 100, 50), "Water wins!", fontStyle2);    
		}   

		for (int i = 0; i < 3; ++i) {  
			for (int j = 0; j < 3; ++j) {   
				if (chessBoard [i, j] == 1)    
					GUI.Button (new Rect (280 + i * 80, 220 + j * 80, 80, 80), Player1_img); 
				
				if (chessBoard [i, j] == 2)  
					GUI.Button (new Rect (280 + i * 80, 220 + j * 80, 80, 80), Player2_img); 
				
				if (GUI.Button (new Rect (280 + i * 80, 220 + j * 80, 80, 80), "")) {  //return true if clicked the button  
					if (result == 0) { 
						if (whoseturn == 0) {
							chessBoard [i, j] = 1;
							whoseturn = 1;
						} else {
							chessBoard [i, j] = 2;    
							whoseturn = 0;
						}
						    
					}    
				}    
			}  
		}  
	}  
	//reset the game when script is allocated
	void Reset() {  
		whoseturn = 0;    
		for (int i = 0; i < 3; ++i) {    
			for (int j = 0; j < 3; ++j) {
			   	chessBoard [i, j] = 0;    
			}    
		}  
	}  

	//determine the game whether it is over  
	int check(){
		//行判断
		for (int i = 0; i < 3; ++i) {
			if (chessBoard [i, 0] == chessBoard [i, 1] && chessBoard [i, 1] == chessBoard [i, 2] && chessBoard [i, 2] != 0) {
				return chessBoard [i, 0];
			}
		}
		//列判断
		for (int j = 0; j < 3; ++j) {
			if (chessBoard [0, j] == chessBoard [1, j] && chessBoard [1, j] == chessBoard [2, j] && chessBoard [2, j] != 0) {
				return chessBoard [0, j];
			}
		}
		//斜线判断
		if ((chessBoard [1, 1] != 0 && chessBoard [0, 0] == chessBoard [1, 1] && chessBoard [1, 1] == chessBoard [2, 2]) ||
			(chessBoard [0, 2] == chessBoard [1, 1] && chessBoard [1, 1] == chessBoard [2, 0])) {
			return chessBoard [1, 1];
		}

		return 0;
	}
}
