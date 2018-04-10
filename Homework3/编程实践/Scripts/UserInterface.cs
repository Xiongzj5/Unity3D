using UnityEngine;  
using System.Collections;  
using Com.Game;  

public class UserInterface : MonoBehaviour {  

	GameSceneController scene;  
	IQueryGameStatus state;  
	IUserActions action;  

	float width, height;  

	float castw(float scale) {  
		return (Screen.width - width) / scale;  
	}  

	float casth(float scale) {  
		return (Screen.height - height) / scale;  
	}  

	void Start() {  
		scene = GameSceneController.GetInstance();  
		state = GameSceneController.GetInstance() as IQueryGameStatus;  
		action = GameSceneController.GetInstance() as IUserActions;  
	}  

	void OnGUI() {  
		width = Screen.width / 12;  
		height = Screen.height / 12;  

		string message = state.isMessage();  
		if (message != "") {  
			if (GUI.Button(new Rect(castw(2f), casth(6f), width, height), message)) {  
				action.restart();  
			}  
		}  
		else {  
			if (GUI.RepeatButton(new Rect(10, 10, 120, 20), scene.getBaseCode().gameName)) {  
				GUI.TextArea(new Rect(10, 40, Screen.width - 20, Screen.height/2), scene.getBaseCode().gameRule);  
			}  
			else if (!state.isMoving()) {  
				if (GUI.Button(new Rect(castw(2.05f), casth(8f), width, height), "Move")) {  
					action.moveBoat();  
				}  
				if (GUI.Button(new Rect(castw(1.3f), casth(4f), width, height), "On")) {  
					action.devilStartOnBoat();  
				}  
				if (GUI.Button(new Rect(castw(1.3f), casth(6.34f), width, height), "On")) {  
					action.priestStartOnBoat();  
				}  
				if (GUI.Button(new Rect(castw(10.5f), casth(4f), width, height), "On")) {  
					action.devilEndOnBoat();  
				}  
				if (GUI.Button(new Rect(castw(10.5f), casth(6.34f), width, height), "On")) {  
					action.priestEndOnBoat();  
				}  
				if (GUI.Button(new Rect(castw(2.5f), casth(8f), width, height), "Off")) {  
					action.offBoatLeft();  
				}  
				if (GUI.Button(new Rect(castw(1.73f), casth(8f), width, height), "Off")) {  
					action.offBoatRight();  
				}  
			}  
		}  
	}  
}  