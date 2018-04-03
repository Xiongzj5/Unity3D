using UnityEngine;  
using System.Collections;  
using Com.Game;  

public class UserInterface : MonoBehaviour {  

	GameSceneController Inst;  
	IUserActions action;  

	float width, height;  

	float castX(float scale) {  
		return (Screen.width - width) / scale;  
	}  

	float castY(float scale) {  
		return (Screen.height - height) / scale;  
	}  

	void Start() {  
		Inst = GameSceneController.GetInstance();  
		action = GameSceneController.GetInstance() as IUserActions;  
	}  

	void OnGUI() {  
		width = Screen.width / 12;  
		height = Screen.height / 12;  
		print (Inst.state);  
		if (Inst.state == State.WIN) {  
			if (GUI.Button(new Rect(castX(2f), castY(6f), width, height), "Win!")) {  
				action.restart();  
			}  
		}  
		else if (Inst.state == State.LOSE) {  
			if (GUI.Button(new Rect(castX(2f), castY(6f), width, height), "Lose!")) {  
				action.restart();  
			}  
		}  
		else {  
			if (GUI.RepeatButton(new Rect(10, 10, 120, 20), Inst.getBaseCode().gameName)) {  
				GUI.TextArea(new Rect(10, 40, Screen.width/2, Screen.height/4), Inst.getBaseCode().gameRule);  
			}  
			else if (Inst.state == State.BSTART || Inst.state == State.BEND) {  
				if (GUI.Button(new Rect(castX(2f), castY(8f), width, height), "Move")) {  
					action.moveBoat();  
				}  
				if (GUI.Button(new Rect(castX(1.3f), castY(4f), width*2, height), "DevilofStart")) {  
					action.devilStartOnBoat();  
				}  
				if (GUI.Button(new Rect(castX(1.3f), castY(6.34f), width*2, height), "PriestofStart")) {  
					action.priestStartOnBoat();  
				}  
				if (GUI.Button(new Rect(castX(10.5f), castY(4f), width*2, height), "DevilofEnd")) {  
					action.devilEndOnBoat();  
				}  
				if (GUI.Button(new Rect(castX(10.5f), castY(6.34f), width*2, height), "PriestofEnd")) {  
					action.priestEndOnBoat();  
				}  
				if (GUI.Button(new Rect(castX(2.75f), castY(8f), width*3/2, height), "LeftOff")) {  
					action.offBoatLeft();  
				}  
				if (GUI.Button(new Rect(castX(1.7f), castY(8f), width*3/2, height), "RightOff")) {  
					action.offBoatRight();  
				}  
			}  
		}  
	}  
}  