using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Com.Factory;

public class UserInterface : MonoBehaviour{
	/* 用户界面有两大作用，一个是记录用户得分和计时以及显示回合数，一个是处理用户的输入
	 * 而用户的输入有两个，一个是用户鼠标左键的点击，一个是用户的空格输入
	 * 每一次用户鼠标左键的点击都会从摄像头发出一条射线，代表了用户射出的子弹，我们只创立一个子弹对象，所以每一次射出子弹之前
	 * 都要重置子弹的位置并重置子弹的速度，当然施加在子弹上的力是保持不变的
	 * 飞碟发射就直接调用用户接口
	 * 得分计时和显示回合数就通过一个查询的接口
	*/
	public Text maintips;   // 倒计时和新回合的提示
	public Text scoresText; // 得分的提示
	public Text roundText;  // 回合的提示
	public Text modelTips;
	private int roundnow;   // 现在所处于的回合
	public GameObject bullet; // 子弹
	public float bulletInterval = 0.25f;   // 发射子弹的间隔
	public float bulletSpeed = 500f; // 子弹的速度
	private float nextBulletTime;

	public Button stateButton; // 通过这个button来切换运动学模式（false）和动力学模式（true）
	private bool gamestate = false;

	private IUserInterface userInt; // 这是用户接口 
	private IQueryStatus queryInt;  // 这是查询接口

	void Start(){
		// 子弹、爆炸碎片、用户接口和查询接口
		bullet = GameObject.Instantiate(bullet) as GameObject;
		userInt = SceneController.getInstance () as IUserInterface;
		queryInt = SceneController.getInstance () as IQueryStatus;
		Button btn = stateButton.GetComponent<Button> ();
		btn.onClick.AddListener (TaskOnClick);
		modelTips.text = "当前模式为运动学模式";
	}
	void TaskOnClick(){
		gamestate = !gamestate;
		userInt.setState (gamestate);
		if (gamestate == true) {
			modelTips.text = "当前模式为动力学模式";
		} else {
			modelTips.text = "当前模式为运动学模式";
		}
	}
	void Update(){
		if (queryInt.isCounting()) {
			maintips.text = ((int)queryInt.getEmitTime ()).ToString ();  // 显示游戏的倒计时
		} else {
			if (Input.GetKeyDown ("space")) {
				userInt.emitDisk ();    // 有监测到空格按下就发射飞碟
			}
			if (queryInt.isShooting ()) {
				maintips.text = "";     // 一旦开始射击了，表明游戏开始，隐藏游戏提示
			}
			if(gamestate == false)
				userInt.diskMove ();
			if (queryInt.isShooting () && Input.GetMouseButtonDown (0) && Time.time > nextBulletTime) {
				// 发射子弹
				nextBulletTime = Time.time + bulletInterval;
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition); // 由摄像机发出，方向为摄像机到鼠标点击位置的连线
				bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;     // 速度重置
				bullet.transform.position = transform.position;               // 子弹从摄像机的位置射出
				bullet.GetComponent<Rigidbody>().AddForce(bulletSpeed*ray.direction,ForceMode.Impulse);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "Disk") {  
					// 击中飞碟设置为不活跃，自动回收  
					hit.collider.gameObject.SetActive(false);  
				}  

			}
		}
		roundText.text = "Round: " + queryInt.getRound ().ToString (); // 通过查询接口查询关卡数
		scoresText.text = "Scores: " + queryInt.getScores ().ToString (); // 通过查询接口查询当前分数
		if (roundnow != queryInt.getRound ()) {
			// 如果关卡数更新要在mainTips里面进行提示
			roundnow = queryInt.getRound();
			maintips.text = "Round: " + roundnow.ToString () + " ! ";
		}
	}
}

