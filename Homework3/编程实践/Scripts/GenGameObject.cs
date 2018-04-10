using UnityEngine;  
using System.Collections;  
using System.Collections.Generic;  
using Com.Game;  

public class GenGameObject : MonoBehaviour {  

	GameObject[] boat = new GameObject[2];  
	GameObject boat_obj;  
	int side = 1;               // side records where boat docks  
	public float speed = 15f;  

	Vector3 bankStartPosition = new Vector3(0, 0, 15);  
	Vector3 bankEndPostion = new Vector3(0, 0, -15);  
	Vector3 boatStartPosition = new Vector3(0, 1, 7);   
	float gap = 1.5f;  
	Vector3 priestStartPosition = new Vector3(0, 4, 11);  
	Vector3 priestEndPosition = new Vector3(0, 4, -14);  
	Vector3 devilStartPosition = new Vector3(0, 4, 15);  
	Vector3 devilEndPosition = new Vector3(0, 4, -19);  
	Vector3 boatEndPosition = new Vector3(0, 1, -6);  
	Vector3 riverpostion = new Vector3(0,-1,0);
	Vector3 leftBoatPosition = new Vector3(0, 1, -1);  
	Vector3 rightBoatPosition = new Vector3(0, 1, 1); 
	Stack<GameObject> priests_start = new Stack<GameObject>();  
	Stack<GameObject> priests_end = new Stack<GameObject>();  
	Stack<GameObject> devils_start = new Stack<GameObject>();  
	Stack<GameObject> devils_end = new Stack<GameObject>();  



	void Start () {  
		GameSceneController.GetInstance().setGenGameObject(this);  
		loadSrc();  
	}  

	void loadSrc() {  
		// shore  
		Instantiate(Resources.Load("Prefabs/Bank"), bankStartPosition, Quaternion.identity);  
		Instantiate(Resources.Load("Prefabs/Bank"), bankEndPostion, Quaternion.identity);  
		// boat  
		boat_obj = Instantiate(Resources.Load("Prefabs/Boat"), boatStartPosition, Quaternion.identity) as GameObject;
		//river
		Instantiate(Resources.Load("Prefabs/River"),riverpostion,Quaternion.identity);

		// priests & devils  
		for (int i = 0; i < 3; ++i) {  
			GameObject priest = Instantiate(Resources.Load("Prefabs/Priest")) as GameObject;  
			priest.transform.position = getCharacterPosition(priestStartPosition, i);  
			priest.tag = "Priest";  
			priests_start.Push(priest);  
			GameObject devil = Instantiate(Resources.Load("Prefabs/Devil")) as GameObject;  
			devil.transform.position = getCharacterPosition(devilStartPosition, i);  
			devil.tag = "Devil";  
			devils_start.Push(devil);  
		}   
	}  

	int boatCapacity() {  
		int capacity = 0;  
		for (int i = 0; i < 2; ++i) {  
			if (boat[i] == null) capacity++;  
		}  
		return capacity;  
	}  

	void getOnTheBoat(GameObject obj) {  
		if (boatCapacity() != 0) {  
			obj.transform.parent = boat_obj.transform;  
			Vector3 target = new Vector3();  
			if (boat[0] == null) {  
				boat[0] = obj;  
				target = boat_obj.transform.position + leftBoatPosition;  
			} else {  
				boat[1] = obj;  
				target = boat_obj.transform.position + rightBoatPosition;  
			}  
			ActionManager.GetInstance().ApplyMoveToYZAction(obj, target, speed);  
		}  
	}  

	public void moveBoat() {  
		if (boatCapacity() != 2) {  
			if (side == 1) {  
				ActionManager.GetInstance().ApplyMoveToAction(boat_obj, boatEndPosition, speed);  
				side = 2;  
			}  
			else if (side == 2) {  
				ActionManager.GetInstance().ApplyMoveToAction(boat_obj, boatStartPosition, speed);  
				side = 1;  
			}  
		}  
	}  

	public void getOffTheBoat(int bside) {  
		if (boat[bside] != null) {  
			boat[bside].transform.parent = null;  
			Vector3 target = new Vector3();  
			if (side == 1) {  
				if (boat[bside].tag == "Priest") {  
					priests_start.Push(boat[bside]);  
					target = getCharacterPosition(priestStartPosition, priests_start.Count - 1);  
				}  
				else if (boat[bside].tag == "Devil") {  
					devils_start.Push(boat[bside]);  
					target = getCharacterPosition(devilStartPosition, devils_start.Count - 1);  
				}  
			}  
			else if (side == 2) {  
				if (boat[bside].tag == "Priest") {  
					priests_end.Push(boat[bside]);  
					target = getCharacterPosition(priestEndPosition, priests_end.Count - 1);  
				}  
				else if (boat[bside].tag == "Devil") {  
					devils_end.Push(boat[bside]);  
					target = getCharacterPosition(devilEndPosition, devils_end.Count - 1);  
				}  
			}  
			ActionManager.GetInstance().ApplyMoveToYZAction(boat[bside], target, speed);  
			boat[bside] = null;  
		}  
	}  

	public void priestStartOnBoat() {  
		if (priests_start.Count != 0 && boatCapacity() != 0 && side == 1)  
			getOnTheBoat(priests_start.Pop());  
	}  

	public void priestEndOnBoat() {  
		if (priests_end.Count != 0 && boatCapacity() != 0 && side == 2)  
			getOnTheBoat(priests_end.Pop());  
	}  

	public void devilStartOnBoat() {  
		if (devils_start.Count != 0 && boatCapacity() != 0 && side == 1)  
			getOnTheBoat(devils_start.Pop());  
	}  

	public void devilEndOnBoat() {  
		if (devils_end.Count != 0 && boatCapacity() != 0 && side == 2)  
			getOnTheBoat(devils_end.Pop());  
	}  

	Vector3 getCharacterPosition(Vector3 pos, int index) {  
		return new Vector3(pos.x, pos.y, pos.z + gap*index);  
	}  

	void check() {  
		GameSceneController scene = GameSceneController.GetInstance();  
		int priestOnBoat = 0, devilOnBoat = 0;  
		int priests_s = 0, devils_s = 0, priests_e = 0, devils_e = 0;  

		if (priests_end.Count == 3 && devils_end.Count == 3) {  
			scene.setMessage("Win!");  
			return;  
		}  

		for (int i = 0; i < 2; ++i) {  
			if (boat[i] != null && boat[i].tag == "Priest") priestOnBoat++;  
			else if (boat[i] != null && boat[i].tag == "Devil") devilOnBoat++;  
		}  
		if (side == 1) {  
			priests_s = priests_start.Count + priestOnBoat;  
			devils_s = devils_start.Count + devilOnBoat;  
			priests_e = priests_end.Count;  
			devils_e = devils_end.Count;  
		}  
		else if (side == 2) {  
			priests_s = priests_start.Count;  
			devils_s = devils_start.Count;  
			priests_e = priests_end.Count + priestOnBoat;  
			devils_e = devils_end.Count + devilOnBoat;  
		}  
		if ((priests_s != 0 && priests_s < devils_s) || (priests_e != 0 && priests_e < devils_e)) {  
			scene.setMessage("Lose!");  
		}  
	}  

	void Update() {  
		check();  
	}  

}  