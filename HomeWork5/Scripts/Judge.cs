using UnityEngine;
using System.Collections;
using Com.Factory;

public class Judge : MonoBehaviour {  
	public int oneDiskScore = 10;  // 设置打中一个飞碟获得多少分
	public int oneDiskFail = 10;   // 设置没打中一个飞碟获得多少分
	public int disksToWin = 4;  

	private SceneController scene;  

	void Awake() {  
		scene = SceneController.getInstance();  
		scene.setJudge(this);  
	}  

	void Start() {  
		scene.nextRound();  // 默认开始第一关  
	}  

	// 击中飞碟得分  
	public void scoreADisk() {  
		scene.setPoint(scene.getScores() + oneDiskScore);  // 已得的分数加上打中一个盘子的分数
		if (scene.getScores () == disksToWin * oneDiskScore) {
			// 如果得分符合过关的要求就进入下一关
			scene.nextRound ();  
		}
	}  

	// 掉落飞碟失分  
	public void failADisk() {  
		scene.setPoint(scene.getScores() - oneDiskFail);  
	}  
}  