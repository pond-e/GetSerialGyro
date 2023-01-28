using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SerialCube_q : MonoBehaviour
{

	public SerialHandler serialHandler;
	public Text text;
	public GameObject cube;

	// Use this for initialization
	void Start()
	{
		//信号を受信したときに、そのメッセージの処理を行う
		serialHandler.OnDataReceived += OnDataReceived;
	}

	// Update is called once per frame
	void Update()
	{

	}

	/*
	 * シリアルを受け取った時の処理
	 */
	void OnDataReceived(string message)
	{
		try
		{
			string[] angles = message.Split(',');
			// MPU9250のDMPからクォータニオンを取得して使用することを目指すスクリプト
			text.text = "w:" + angles[0] + "\n" + "x:" + angles[1] + "\n" + "y:" + angles[2] + "\n" + "z:" + angles[3] + "\n"; // シリアルの値をテキストに表示

            // toAxisAngle
            float q_angle;
			Vector3 angle;
			float x_value = -1.0f * float.Parse(angles[2]); // 右手系と左手系
            float y_value = -1.0f * float.Parse(angles[3]);
			float z_value = float.Parse(angles[1]);
			float w_value = float.Parse(angles[0]);

			float wAcos = 0f;
			if (w_value <= -1.0f)
			{
				wAcos = 2 * Mathf.PI;
			}else if(w_value >= 1.0)
			{
				wAcos = 0f;
			}
			else
			{
				wAcos = 2.0f * Mathf.Acos(w_value);
			}

			float wAsin = 2* Mathf.PI - wAcos;
			if (wAsin == 0f)
			{
				q_angle = wAcos;
				angle = new Vector3(0, 0, 1.0f);
				cube.transform.rotation = Quaternion.AngleAxis((q_angle / Mathf.PI) * 180f, angle);
				return;
			}

			float sInv = 1.0f / wAsin;
			float x = x_value * sInv;
			float y = y_value * sInv;
			float z = z_value * sInv;

			float mSq = x * x + y * y + z * z;
			if (mSq == 0)
			{
				q_angle = wAcos;
				angle = new Vector3(0, 0, 1.0f);
				cube.transform.rotation = Quaternion.AngleAxis((q_angle/Mathf.PI) * 180f, angle);
				return;
			}else if(mSq == 1.0f)
			{
				q_angle = wAcos;
				angle = new Vector3(x, y, z);
				cube.transform.rotation = Quaternion.AngleAxis((q_angle / Mathf.PI) * 180f, angle);
				return;
			}

			float mInv = 1.0f / Mathf.Sqrt(mSq);
			q_angle = wAcos;
			angle = new Vector3(x * mInv, y * mInv, z* mInv);
			cube.transform.rotation = Quaternion.AngleAxis((q_angle / Mathf.PI) * 180f, angle);
		}
		catch (System.Exception e)
		{
			Debug.LogWarning(e.Message);
		}
	}
}