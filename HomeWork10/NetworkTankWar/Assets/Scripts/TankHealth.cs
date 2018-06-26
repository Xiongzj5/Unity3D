using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHealth : MonoBehaviour {
    public int hp = 100;
    public GameObject tankExplosion;
    public AudioClip tankExplosionAudio;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void TakeDamage()
    {
        if (hp <= 0) return;
        if(hp > 0)
            hp -= Random.Range(10, 20);
        if(hp <= 0)//受到伤害之后，血量为0，控制死亡效果
        {
            AudioSource.PlayClipAtPoint(tankExplosionAudio, transform.position);
            GameObject.Instantiate(tankExplosion, transform.position + Vector3.up, transform.rotation);
            GameObject.Destroy(this.gameObject);
        }



    }

}
