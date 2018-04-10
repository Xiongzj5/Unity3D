﻿using UnityEngine;  
using System.Collections;  
using Com.Game;  

namespace Com.Game {  
	public interface IUserActions {  
		void priestStartOnBoat();  
		void priestEndOnBoat();  
		void devilStartOnBoat();  
		void devilEndOnBoat();  
		void moveBoat();  
		void offBoatLeft();  
		void offBoatRight();  
		void restart();  
	}  

	public interface IQueryGameStatus  {  
		bool isMoving();  
		void setMoving(bool state);  
		string isMessage();  
		void setMessage(string message);  
	}  

	public class GameSceneController: System.Object, IUserActions, IQueryGameStatus {  

		private static GameSceneController single_instance;  
		private BaseCode bcode;  
		private GenGameObject ggameobject;  
		private bool moving = false;  
		private string message = "";  

		public static GameSceneController GetInstance() {  
			if (null == single_instance) {  
				single_instance = new GameSceneController();  
			}  
			return single_instance;  
		}  

		// registration  
		public BaseCode getBaseCode() {  
			return bcode;  
		}  

		internal void setBaseCode(BaseCode bc) {  
			if (null == bcode) {  
				bcode = bc;  
			}  
		}  

		public GenGameObject getGenGameObject() {  
			return ggameobject;  
		}  

		internal void setGenGameObject(GenGameObject ggo) {  
			if (null == ggameobject) {  
				ggameobject = ggo;  
			}  
		}   

		// IUserActions  
		public void priestStartOnBoat() { ggameobject.priestStartOnBoat(); }  
		public void priestEndOnBoat() { ggameobject.priestEndOnBoat(); }  
		public void devilStartOnBoat() { ggameobject.devilStartOnBoat(); }  
		public void devilEndOnBoat() { ggameobject.devilEndOnBoat(); }  
		public void moveBoat() { ggameobject.moveBoat(); }  
		public void offBoatLeft() { ggameobject.getOffTheBoat(0); }  
		public void offBoatRight() { ggameobject.getOffTheBoat(1); }  
		// IQueryGameStatus  
		public bool isMoving() { return moving; }  
		public void setMoving(bool state) { this.moving = state; }  
		public string isMessage() { return message; }  
		public void setMessage(string message) { this.message = message; } 
		public void restart() {  
			moving = false;  
			message = "";  
			Application.LoadLevel(Application.loadedLevelName);  
		}  
	}  

	//----------------------------ActionManager-------------------------------//  
	public interface IU3dActionCompleted {  
		void OnActionCompleted (U3dAction action);  
	}  

	public class ActionManager :System.Object {  
		private static ActionManager single_instance;  

		public static ActionManager GetInstance(){  
			if (single_instance == null) {  
				single_instance = new ActionManager();  
			}  
			return single_instance;  
		}  

		// ApplyMoveToAction  
		public U3dAction ApplyMoveToAction(GameObject obj, Vector3 target, float speed, IU3dActionCompleted completed){  
			MoveToAction ac = obj.AddComponent <MoveToAction> ();  
			ac.setting (target, speed, completed);  
			return ac;  
		}  

		public U3dAction ApplyMoveToAction(GameObject obj, Vector3 target, float speed) {  
			return ApplyMoveToAction (obj, target, speed, null);  
		}  

		// ApplyMoveToYZAction  
		public U3dAction ApplyMoveToYZAction(GameObject obj, Vector3 target, float speed, IU3dActionCompleted completed){  
			MoveToYZAction ac = obj.AddComponent <MoveToYZAction> ();  
			ac.setting (obj, target, speed, completed);  
			return ac;  
		}  

		public U3dAction ApplyMoveToYZAction(GameObject obj, Vector3 target, float speed) {  
			return ApplyMoveToYZAction (obj, target, speed, null);  
		}  
	}  

	public class U3dActionException : System.Exception {}  

	public class U3dAction : MonoBehaviour {  
		public void Free() {  
			Destroy(this);  
		}  
	}  

	public class U3dActionAuto : U3dAction {}  

	public class U3dActionMan : U3dAction {}  

	public class MoveToAction :  U3dActionAuto {  
		public Vector3 target;  
		public float speed;  

		private IU3dActionCompleted monitor = null;  

		public void setting(Vector3 target, float speed, IU3dActionCompleted monitor){  
			this.target = target;  
			this.speed = speed;  
			this.monitor = monitor;  
			GameSceneController.GetInstance().setMoving(true);  
		}  

		void Update () {  
			float step = speed * Time.deltaTime;  
			transform.position = Vector3.MoveTowards(transform.position, target, step);  

			// Auto Destroy After Completed  
			if (transform.position == target) {   
				GameSceneController.GetInstance().setMoving(false);  
				if (monitor != null) {  
					monitor.OnActionCompleted(this);  
				}  
				Destroy(this);  
			}  
		}  
	}  
		
	public class MoveToYZAction : U3dActionAuto, IU3dActionCompleted {  
		public GameObject obj;  
		public Vector3 target;  
		public float speed;  

		private IU3dActionCompleted monitor = null;  

		public void setting(GameObject obj, Vector3 target, float speed, IU3dActionCompleted monitor) {  
			this.obj = obj;  
			this.target = target;  
			this.speed = speed;  
			this.monitor = monitor;  
			GameSceneController.GetInstance().setMoving(true);  

			/* If obj is higher than target, move to target.z first, then move to target.y 
             * If obj is lower than target, move to target.y first, then move to target.z  
             */  
			if (target.y < obj.transform.position.y) {  
				Vector3 targetZ = new Vector3(target.x, obj.transform.position.y, target.z);  
				ActionManager.GetInstance().ApplyMoveToAction(obj, targetZ, speed, this);  
			} else {  
				Vector3 targetY = new Vector3(target.x, target.y, obj.transform.position.z);  
				ActionManager.GetInstance().ApplyMoveToAction(obj, targetY, speed, this);  
			}  
		}  

		// Implement the turn  
		public void OnActionCompleted (U3dAction action) {  
			// Note not calling this callback again!  
			ActionManager.GetInstance().ApplyMoveToAction(obj, target, speed, null);  
		}  

		void Update() {  
			// Auto Destroy After Completed  
			if (obj.transform.position == target) {   
				GameSceneController.GetInstance().setMoving(false);  
				if (monitor != null) {  
					monitor.OnActionCompleted(this);  
				}  
				Destroy(this);  
			}  
		}  
	}  
}  

public class BaseCode : MonoBehaviour {  

	public string gameName;  
	public string gameRule;  

	void Start () {  
		GameSceneController my = GameSceneController.GetInstance();  
		my.setBaseCode(this);  
		gameName = "Priests and Devils";  
		gameRule = "If the priests are out numbered by the devils on either side of the river, they get killed and the game is over. Keep all priests alive! \nGreen -- Priest    Red -- Devil";  
	}  
}  