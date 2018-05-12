using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSActionManager : MonoBehaviour, ISSActionCallback
{
    private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();    //将执行的动作的字典集合
    private List<SSAction> waitingAdd = new List<SSAction>();                       //等待去执行的动作列表
    private List<int> waitingDelete = new List<int>();                              //等待删除的动作的key                

    protected void Update()
    {
		//把等待去执行的动作列表放入字典集合里
        foreach (SSAction ac in waitingAdd)
        {
            actions[ac.GetInstanceID()] = ac;
        }
		//清空动作列表
        waitingAdd.Clear();

        foreach (KeyValuePair<int, SSAction> kv in actions)
        {
            SSAction ac = kv.Value;
            if (ac.destroy)
            {
                waitingDelete.Add(ac.GetInstanceID());
            }
            else if (ac.enable)
            {
                //运动学运动更新
                ac.Update();
            }
        }

        foreach (int key in waitingDelete)
        {
            SSAction ac = actions[key];
            actions.Remove(key);
            DestroyObject(ac);
        }
        waitingDelete.Clear();
    }
 
    public void RunAction(GameObject gameobject, SSAction action, ISSActionCallback manager)
    {
        action.gameobject = gameobject;
        action.transform = gameobject.transform;
        action.callback = manager;
        waitingAdd.Add(action);
        action.Start();
    }

    public void SSActionEvent(SSAction source, int intParam = 0, GameObject objectParam = null)
    {
        if(intParam == 0)
        {
            //侦查兵跟随玩家
			//获取靠近玩家的方法放在follow里
            PatrolFollowAction follow = PatrolFollowAction.GetSSAction(objectParam.gameObject.GetComponent<PatrolData>().player);
            this.RunAction(objectParam, follow, this);
        }
        else
        {
            //侦察兵按照初始位置开始继续巡逻
            GoPatrolAction move = GoPatrolAction.GetSSAction(objectParam.gameObject.GetComponent<PatrolData>().start_position);
            this.RunAction(objectParam, move, this);
            //玩家逃脱
            Singleton<GameEventManager>.Instance.PlayerEscape();
        }
    }

	// 游戏结束后，摧毁所有动作，巡逻兵不再移动
    public void DestroyAll()
    {
        foreach (KeyValuePair<int, SSAction> kv in actions)
        {
            SSAction ac = kv.Value;
            ac.destroy = true;
        }
    }
}

// 这个类可以合并进入SSActionManager
public class PatrolActionManager : SSActionManager
{
	private GoPatrolAction go_patrol;                            //巡逻兵巡逻
	//场景控制器调用此类中的方法，让巡逻兵开始巡逻
	public void GoPatrol(GameObject patrol)
	{
		go_patrol = GoPatrolAction.GetSSAction(patrol.transform.position);
		this.RunAction(patrol, go_patrol, this);
	}
	//游戏结束的时候，停止所有动作
	public void DestroyAllAction()
	{
		DestroyAll();
	}
}
