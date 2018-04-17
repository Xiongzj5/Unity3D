using UnityEngine;
using System.Collections;
using Com.Factory;
namespace Com.Factory{
	public interface IUserInterface{
		void emitDisk();
	}
	public interface IQueryStatus{
		bool isCounting();
		bool isShooting();
		int getRound();
		int getScores();
		int getEmitTime();
	}
	public interface IJudgeEvent{
		void nextRound();
		void setPoint (int point);
	}


}

public class SceneControllerBC : MonoBehaviour {
	private Color color;  // 飞碟的颜色
	private Vector3 emitPos;  // 飞碟发射的位置
	private Vector3 emitDir;  // 飞碟发射的角度
	private float speed;      // 飞碟发射的速度

	void Awake() {  
		SceneController.getInstance().setSceneControllerBC(this);  
	}  

	// 函数loadRoundData()，用来初始化游戏场景
	public void loadRoundData(int round) {  
		switch(round) {  
		case 1:     // 第一关  
			color = Color.green;  
			emitPos = new Vector3 (1f, 10f, -5f);  
			emitDir = new Vector3 (24.5f, 40f, 67f);  
			speed = 5;  
			SceneController.getInstance ().getGameModel ().setting (1, color, emitPos, emitDir.normalized, speed, 1); 
			break;  
		case 2:     // 第二关  
			color = Color.blue;  
			emitPos = new Vector3 (5f, 10f, -5f);
			emitDir = new Vector3 (-24.5f, 40f, 67f);  
			speed = 7;  
			SceneController.getInstance ().getGameModel ().setting (1, color, emitPos, emitDir.normalized, speed, 2);  
			break;  
		case 3:     // 第三关
			color = Color.red;
			emitPos = new Vector3 (2f, 20f, -5f);
			emitDir = new Vector3 (-23f, 44f, 67f);
			speed = 8;
			SceneController.getInstance ().getGameModel ().setting (1, color, emitPos, emitDir.normalized, speed, 3);
			break;
		case 4:     // 第四关
			color = Color.yellow;
			emitPos = new Vector3 (1f, 20f, -5f);
			emitDir = new Vector3 (23f, 44f, 67f);
			speed = 10;
			SceneController.getInstance ().getGameModel ().setting (1, color, emitPos, emitDir.normalized, speed, 4);
			break;
		}  


	}  
}  

public class SceneController:System.Object,IQueryStatus,IUserInterface,IJudgeEvent{
	private static SceneController instance;  // 场景控制器的实例
	private SceneControllerBC baseCode;       
	private GameModel gameModel;
	private Judge judge;

	private int point_record;
	private int round_record;
	public static SceneController getInstance(){
		// get到场景控制器的实例
		if(instance == null){
			instance = new SceneController ();
		}
		return instance;
	}
	// 实现每个接口函数
	public void emitDisk(){
		gameModel.prepareToEmitDisk ();
	}
	public bool isCounting(){
		return gameModel.isCounting ();
	}
	public bool isShooting(){
		return gameModel.isShooting ();
	}
	public int getRound(){
		return round_record;
	}
	public int getScores(){
		return point_record;
	}
	public int getEmitTime(){
		return (int)gameModel.timeToEmit + 1;
	}
	public void nextRound(){
		point_record = 0;
		baseCode.loadRoundData (++round_record);
	}
	public void setPoint (int point){
		point_record = point;
	}


	public void setGameModel(GameModel model){
		gameModel = model;
	}
	internal GameModel getGameModel(){
		return gameModel;
	}

	public void setJudge(Judge judgement){
		judge = judgement;
	}
	internal Judge getJudge(){
		return judge;
	}
	public void setSceneControllerBC(SceneControllerBC bc){
		baseCode = bc;
	}
	internal SceneControllerBC getSceneControllerBC(){
		return baseCode;
	}

}


