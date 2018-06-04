using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fold : MonoBehaviour
{

   
    public Text text;//文本框对象
    private int frame = 20;//规定公告栏折叠和展开的帧数

    // Use this for initialization
    void Start()
    {
        text.gameObject.SetActive(false);//在我的设置中，一开始文本框是处于折叠状态的
        Button btn = this.gameObject.GetComponent<Button>();//获取到button对象
        btn.onClick.AddListener(TaskOnClick);//为button设置点击监听
    }

    IEnumerator rotateIn() // 让公告折叠的函数
    {
        float rx = 0;
        float xy = 120;
        for (int i = 0; i < frame; i++)
        {
            rx -= 90f / frame;
            xy -= 70f / frame;
            text.transform.rotation = Quaternion.Euler(rx, 0, 0);
            text.rectTransform.sizeDelta = new Vector2(text.rectTransform.sizeDelta.x, xy);
            if (i == frame - 1)
            {
                text.gameObject.SetActive(false);
            }
            yield return null;
        }
    }

    IEnumerator rotateOut() //让公告展开的函数
    {
        float rx = -90;
        float xy = 0;
        for (int i = 0; i < frame; i++)
        {
            rx += 90f / frame;
            xy += 70f / frame;
            text.transform.rotation = Quaternion.Euler(rx, 0, 0);
            text.rectTransform.sizeDelta = new Vector2(text.rectTransform.sizeDelta.x, xy);
            if (i == 0)
            {
                text.gameObject.SetActive(true);
            }
            yield return null;
        }
    }


    void TaskOnClick()
    {
        if (text.gameObject.activeSelf)
        {
            StartCoroutine(rotateIn());
        }
        else
        {
            StartCoroutine(rotateOut());
        }

    }
}
