using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

	public enum State { BSTART, BSEMOVING, BESMOVING, BEND, WIN, LOSE }; 

	public class GameSceneController: System.Object, IUserActions {  

		private static GameSceneController single_instance;  
		private BaseCode baseCode;  
		private GGameObject ggameObject;  
		public State state = State.BSTART;  

		public static GameSceneController GetInstance() {  
			if (null == single_instance) {  
				single_instance = new GameSceneController();  
			}  
			return single_instance;  
		}  

		public BaseCode getBaseCode() {  
			return baseCode;  
		}  

		internal void setBaseCode(BaseCode bc) {  
			if (null == baseCode) {  
				baseCode = bc;  
			}  
		}  

		public GGameObject getGenGameObject() {  
			return ggameObject;  
		}  

		internal void setGenGameObject(GGameObject move) {  
			if (null == ggameObject) {  
				ggameObject = move;  
			}  
		}  

		public void priestStartOnBoat() {  
			ggameObject.priestStartOnBoat();  
		}  

		public void priestEndOnBoat() {  
			ggameObject.priestEndOnBoat();  
		}  

		public void devilStartOnBoat() {  
			ggameObject.devilStartOnBoat();  
		}  

		public void devilEndOnBoat() {  
			ggameObject.devilEndOnBoat();  
		}  

		public void moveBoat() {  
			ggameObject.moveBoat();  
		}  

		public void offBoatLeft() {  
			ggameObject.getOffTheBoat(0);  
		}  

		public void offBoatRight() {  
			ggameObject.getOffTheBoat(1);  
		}  

		public void restart() {  
			Application.LoadLevel(Application.loadedLevelName);  
			state = State.BSTART;  
		}  
	}  
}  

public class BaseCode : MonoBehaviour {  

	public string gameName;  
	public string gameRule;  

	void Start () {  
		GameSceneController Inst = GameSceneController.GetInstance();  
		Inst.setBaseCode(this);  
		gameName = "Priests and Devils";  
		gameRule = "If the priests are out numbered by the devils on either side of the river, they get killed and the game is over. Keep all priests alive! \nGreen -- Priest    Red -- Devil";  
	}  
}  