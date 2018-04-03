using UnityEngine;  
using System.Collections;  
using System.Collections.Generic;  
using Com.Game;  

public class GGameObject : MonoBehaviour {  


	GameObject[] boat = new GameObject[2];  
	GameObject boat_obj;  
	public float speed = 100f;  

	GameSceneController Inst;  

	Vector3 shoreStartPosition = new Vector3(0, 0, 15);  
	Vector3 shoreEndPosition = new Vector3(0, 0, -15);  
	Vector3 boatStartPosition = new Vector3(0, 1, 7);  
	Vector3 boatEndPosition = new Vector3(0, 1, -6);  

	float gap = 1.5f;  
	Vector3 priestStartPosition = new Vector3(0, 4, 11);  
	Vector3 priestEndPosition = new Vector3(0, 4, -14);  
	Vector3 devilStartPosition = new Vector3(0, 4, 15);  
	Vector3 devilEndPosition = new Vector3(0, 4, -19);  
	Vector3 riverpostion = new Vector3(0,-1,0);
	Stack<GameObject> priests_start = new Stack<GameObject>();  
	Stack<GameObject> priests_end = new Stack<GameObject>();  
	Stack<GameObject> devils_start = new Stack<GameObject>();  
	Stack<GameObject> devils_end = new Stack<GameObject>();  

	void Start () {  
		Inst = GameSceneController.GetInstance();  
		Inst.setGenGameObject(this);  
		loadSrc();  
	}  

	void loadSrc() {  
		// shore  
		Instantiate(Resources.Load("Prefabs/Bank"), shoreStartPosition, Quaternion.identity);  
		Instantiate(Resources.Load("Prefabs/Bank"), shoreEndPosition, Quaternion.identity);  
		// boat 
		Instantiate(Resources.Load("Prefabs/River"),riverpostion,Quaternion.identity);
		//river

		boat_obj = Instantiate(Resources.Load("Prefabs/Boat"), boatStartPosition, Quaternion.identity) as GameObject;  
		// priests & devils  
		for (int i = 0; i < 3; ++i) {  
			priests_start.Push(Instantiate(Resources.Load("Prefabs/Priest")) as GameObject);  
			devils_start.Push(Instantiate(Resources.Load("Prefabs/Devil")) as GameObject);  
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
			if (boat[0] == null) {  
				boat[0] = obj;  
				obj.transform.localPosition = new Vector3(0, 1.2f, -0.3f);  
			} else {  
				boat[1] = obj;  
				obj.transform.localPosition = new Vector3(0, 1.2f, 0.3f);  
			}  
		}  
	}  

	public void moveBoat() {  
		if (boatCapacity() != 2) {  
			if (Inst.state == State.BSTART) {  
				Inst.state = State.BSEMOVING;  
			}  
			else if (Inst.state == State.BEND) {  
				Inst.state = State.BESMOVING;  
			}  
		}  
	}  

	public void getOffTheBoat(int side) {  
		if (boat[side] != null) {  
			boat[side].transform.parent = null;  
			if (Inst.state == State.BEND) {  
				if (boat[side].tag == "Priest") {  
					priests_end.Push(boat[side]);  
				}  
				else if (boat[side].tag == "Devil") {  
					devils_end.Push(boat[side]);  
				}  
			}  
			else if (Inst.state == State.BSTART) {  
				if (boat[side].tag == "Priest") {  
					priests_start.Push(boat[side]);  
				}  
				else if (boat[side].tag == "Devil") {  
					devils_start.Push(boat[side]);  
				}  
			}  
			boat[side] = null;  
		}  
	}  

	public void priestStartOnBoat() {  
		if (priests_start.Count != 0 && boatCapacity() != 0 && Inst.state == State.BSTART)  
			getOnTheBoat(priests_start.Pop());  
	}  

	public void priestEndOnBoat() {  
		if (priests_end.Count != 0 && boatCapacity() != 0 && Inst.state == State.BEND)  
			getOnTheBoat(priests_end.Pop());  
	}  

	public void devilStartOnBoat() {  
		if (devils_start.Count != 0 && boatCapacity() != 0 && Inst.state == State.BSTART)  
			getOnTheBoat(devils_start.Pop());  
	}  

	public void devilEndOnBoat() {  
		if (devils_end.Count != 0 && boatCapacity() != 0 && Inst.state == State.BEND)  
			getOnTheBoat(devils_end.Pop());  
	}  

	void setCharacterPositions(Stack<GameObject> stack, Vector3 pos) {  
		GameObject[] array = stack.ToArray();  
		for (int i = 0; i < stack.Count; ++i) {  
			array[i].transform.position = new Vector3(pos.x, pos.y, pos.z + gap*i);  
		}  
	}  

	void check() {  
		int pOnb = 0, dOnb = 0;  
		int priests_s = 0, devils_s = 0, priests_e = 0, devils_e = 0;  

		if (priests_end.Count == 3 && devils_end.Count == 3) {  
			Inst.state = State.WIN;  
			return;  
		}  

		for (int i = 0; i < 2; ++i) {  
			if (boat[i] != null && boat[i].tag == "Priest") pOnb++;  
			else if (boat[i] != null && boat[i].tag == "Devil") dOnb++;  
		}  
		if (Inst.state == State.BSTART) {  
			priests_s = priests_start.Count + pOnb;  
			devils_s = devils_start.Count + dOnb;  
			priests_e = priests_end.Count;  
			devils_e = devils_end.Count;  
		}  
		else if (Inst.state == State.BEND) {  
			priests_s = priests_start.Count;  
			devils_s = devils_start.Count;  
			priests_e = priests_end.Count + pOnb;  
			devils_e = devils_end.Count + dOnb;  
		}  
		if ((priests_s != 0 && priests_s < devils_s) || (priests_e != 0 && priests_e < devils_e)) {  
			Inst.state = State.LOSE;  
		}  
	}  

	void Update() {  
		setCharacterPositions(priests_start, priestStartPosition);  
		setCharacterPositions(priests_end, priestEndPosition);  
		setCharacterPositions(devils_start, devilStartPosition);  
		setCharacterPositions(devils_end, devilEndPosition);  

		if (Inst.state == State.BSEMOVING) {  
			boat_obj.transform.position = Vector3.MoveTowards(boat_obj.transform.position, boatEndPosition, speed*Time.deltaTime);  
			if (boat_obj.transform.position == boatEndPosition) {  
				Inst.state = State.BEND;  
			}  
		}  
		else if (Inst.state == State.BESMOVING) {  
			boat_obj.transform.position = Vector3.MoveTowards(boat_obj.transform.position, boatStartPosition, speed*Time.deltaTime);  
			if (boat_obj.transform.position == boatStartPosition) {  
				Inst.state = State.BSTART;  
			}  
		}  
		else check();  
	}  
}  