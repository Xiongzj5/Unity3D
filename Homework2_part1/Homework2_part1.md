# 简答题部分
<li>游戏对象运动的本质是什么？</li>

> 游戏对象运动的本质其实就是游戏对象随着游戏每一帧的变化过
> 程中，游戏对象的transform组件中的position、rotation  以及scale属性参数的变化， 第一个是游戏对象位置上的改变  （又可以分为绝对位置和相对位置），第 二个是游戏对象所处位 置的角度的旋转变化、第三个是游戏对象的大小、 规模的变化

<li>请用三种方法以上方法，实现物体的抛物线运动</li>
（如，修改Transform属性，使用向量Vector3的方法…）

> 1) 通过修改Transform属性来实现. 我们拿落体运动举例，落体运动的本质就是水平方向上做匀速运动而在y轴上做一个加速运动，我们把水平方向上的速度Vx和垂直方向上的速度Vy看作是两个向量，我们做一个向量的合成；我们会发现随着时间的推移，这个合成出来的向量会越来越偏向垂直方向，这也是落体运动的本质。

```c#
public class Move : MonoBehaviour {
	public float speedup = 1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position += Vector3.right * Time.deltaTime * 8; //在每一帧这个很短的时间内，可以把x轴上的运动看作匀速运动
		this.transform.position += Vector3.down * Time.deltaTime * speedup;
		/*这个speedup每一帧都会增长从而给物体一个y轴上更快的速度和更大的位移来达到抛物线运动*/
		speedup += 10 * Time.deltaTime;
	}
}

```

> 2) 使用Vector3方法来实现抛物线运动.
 我们可以计算每一帧物体发生的位移，并且新建一个Vector3向量来表示这个位移。然后再把这个Vector3向量加到我们物体的position上即可。仍然需要注意的是，我们水平方向上做的是匀速运动（即下面代码中的speedX，这个速度是不变的）；而在垂直方向上由于物体做的是一个落体运动，所以垂直速度（下面代码中的speedY是一个每一帧都会增加的变量，但是在每一帧的时间中我们依然可以把垂直方向上的运动看作是匀速运动）

```c#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
	public float speedX = 5;
	public float speedY = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 temp = new Vector3 (Time.deltaTime * speedX, -speedY * Time.deltaTime, 0);
		this.transform.position += temp;
		speedY += 10 * Time.deltaTime;
	}
}

```

> 3) 通过调用Rigidbody类里面的MovePosition以及物体的刚体属性实现抛物线运动.因为刚体本身是具有重力属性的，所以我们无需去管垂直方向上物体的运动，我们只需要让物体每一帧的水平位置发生改变即可。需要注意的是，由于这个物体是刚体，所以我们应该在FixedUpdate里面来实现我们的操作。我们调用MovePosition函数，括号内是我们本来的位置加上每一帧的位移，这样就可以得到每一帧过后物体的位置了

```c#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
	public float speedX = 5;
	public Rigidbody rb;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void FixedUpdate(){
		rb.MovePosition (Vector3.right * speedX * Time.deltaTime + transform.position);
	}
}

```

><li>写一个程序，实现一个完整的太阳系， 其他星球围绕太阳的转速必须不一样，且不在一个法平面上</li>
>1、设置好八大行星的运动，用一个脚本里面实现，再把这个脚本挂在Sun对象上。首先利用GameObject.Find函数找到该游戏对象，再通过其transform类里的函数分别来实现游戏对象的公转跟自转。在下图中我们可以看到，每一个游戏对象都相应对应两条语句，第一条语句所用到的函数是RotateAround函数，这个函数有三个参数，其作用是实现公转，这三个参数的意义分别是旋转中心，旋转轴，旋转角度。第二条语句所用的函数是Rotate函数，这个函数只有一个参数，其作用是实现自转，这个函数的意义是以欧拉角旋转,顺序是ZXY，拿right举例，Vector.right是向X轴旋转1度.
下面是挂载在sun上面的代码

```c#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GameObject.Find("Mercury").transform.RotateAround(Vector3.zero, new Vector3(1, 1, 0), 17 * Time.deltaTime);  
		GameObject.Find("Mercury").transform.Rotate(Vector3.up * Time.deltaTime * 200);  

		GameObject.Find("Venus").transform.RotateAround(Vector3.zero, new Vector3(1, 0, 1), 22 * Time.deltaTime);  
		GameObject.Find("Venus").transform.Rotate(Vector3.up * Time.deltaTime * 200);  

		GameObject.Find("Earth").transform.RotateAround(Vector3.zero, new Vector3(0, 1, 1), 34 * Time.deltaTime);  
		GameObject.Find("Earth").transform.Rotate(Vector3.up * Time.deltaTime * 200);  

		GameObject.Find("Mars").transform.RotateAround(Vector3.zero, new Vector3(0, 1, 0), 43 * Time.deltaTime);  
		GameObject.Find("Mars").transform.Rotate(Vector3.up * Time.deltaTime * 200);  

		GameObject.Find("Jupiter").transform.RotateAround(Vector3.zero, new Vector3(1, 2, 0), 35 * Time.deltaTime);  
		GameObject.Find("Jupiter").transform.Rotate(Vector3.up * Time.deltaTime * 200); 

		GameObject.Find("Saturn").transform.RotateAround(Vector3.zero, new Vector3(1, 0, 0), 42 * Time.deltaTime);  
		GameObject.Find("Saturn").transform.Rotate(Vector3.up * Time.deltaTime * 200);

		GameObject.Find("Uranus").transform.RotateAround(Vector3.zero, new Vector3(1, 0, 2), 20 * Time.deltaTime);  
		GameObject.Find("Uranus").transform.Rotate(Vector3.up * Time.deltaTime * 200);  

		GameObject.Find("Neptune").transform.RotateAround(Vector3.zero, new Vector3(0, 2, 1), 30 * Time.deltaTime);  
		GameObject.Find("Neptune").transform.Rotate(Vector3.up * Time.deltaTime * 200); 
	}
}

```

>对于月亮之于地球的公转，我们采用课件上新建一个空对象的方法，每一帧刷新的时候都让空对象的位置和地球的位置处于同一位置。再让moon绕着这个空对象公转即可
以下是挂载在空对象上的代码

```c#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Empty : MonoBehaviour {
	public GameObject ob;
	// Use this for initialization
	void Start () {
		this.transform.position = ob.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = ob.transform.position;

	}
}

```

以下是挂载在月亮对象上的代码

```c#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.RotateAround(Vector3.zero, Vector3.up, 100 * Time.deltaTime);   
	}
}

```

以下是运行时的截图，轨迹是添加的组件Trail Render所显示出来的
![Alt text](./Solarsystem.png)
