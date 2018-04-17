using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Factory;
namespace Com.Factory{
	/*新建一个飞碟工厂类，飞碟工厂的作用主要是管理飞碟实例，还需要屏蔽飞碟实例的提取和回收，主要要实现的函数(提供给其他对象调用的函数)如下：
		1、getDisk 
		2、getDiskObject
		3、free
	*/
	public class DiskFactory : System.Object{
		//需要两个东西，一个是工厂类的实例，一个是飞碟队列
		private static DiskFactory single_instance; 
		private static List<GameObject> listOfDisk;
		public GameObject DiskPrefab;
		// 获取工厂实例
		public static DiskFactory getInstance(){
			if (single_instance == null) {
				single_instance = new DiskFactory ();
				listOfDisk = new List<GameObject> ();
			}
			return single_instance;
		}
		// getDisk
		// 有空闲的飞碟就返回其id，否则就在队列中添加一个新的飞碟对象并返回其id
		public int getDisk(){
			for (int i = 0; i < listOfDisk.Count; ++i) {
				if (!listOfDisk [i].activeInHierarchy) {
					return i;
				}
			}
			listOfDisk.Add (GameObject.Instantiate (DiskPrefab) as GameObject);
			listOfDisk [listOfDisk.Count - 1].AddComponent<Rigidbody>();
			return listOfDisk.Count - 1;
		}
		// getDiskObject
		public GameObject getDiskObject(int i){
			if (i >= 0 && i < listOfDisk.Count) {
				return listOfDisk [i];
			}
			return null;
		}

	 	// free
		public void free(int i){
			if (i >= 0 && i < listOfDisk.Count) {
				listOfDisk [i].GetComponent<Rigidbody> ().velocity = Vector3.zero;     // 重置速度
				listOfDisk [i].transform.localScale = DiskPrefab.transform.localScale; // 重制大小
				listOfDisk [i].SetActive (false); // 使其不显示　
			}
		}
	}
}
public class DFactory : MonoBehaviour {
	public GameObject disk;

	void Awake(){
		// 初始化对象
		DiskFactory.getInstance ().DiskPrefab = disk;
	}
}
