using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SerialCube : MonoBehaviour
{

	public SerialHandler serialHandler;
	public Text text;
	public GameObject cube;

	private float[] q_before = new float[4] { 0, 0, 0, 1 };
	private float[] q = new float[4];

	private float dt = 0.1f;

	private float[] r_tmp = new float[3];

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
			text.text = "x:" + angles[0] + "\n" + "y:" + angles[1] + "\n" + "z:" + angles[2] + "\n"; // シリアルの値をテキストに表示

            // 取得した角速度をオイラー角に変換する(参考:https://qiita.com/kanade_k_1228/items/ad782e683761cf5ad578)
            float x_value = -1.0f * float.Parse(angles[0]);
			float y_value = float.Parse(angles[1]);
			float z_value = float.Parse(angles[2]); // 右手系と左手系

			r_tmp = find_r(y_value, z_value, x_value);
			Vector3 angle = new Vector3(cube.transform.localEulerAngles.x + r_tmp[0], cube.transform.localEulerAngles.y + r_tmp[1], cube.transform.localEulerAngles.z + r_tmp[2]);
			this.transform.rotation = Quaternion.Euler(angle);
		}
		catch (System.Exception e)
		{
			Debug.LogWarning(e.Message);
		}
	}

	float[] find_r(float x, float y, float z)
    {
		float[] result = new float[3];
		float ganma = cube.transform.localEulerAngles.x;
		float beta = cube.transform.localEulerAngles.y;

		result[0] = 1 * (x * dt) + Mathf.Tan(beta * (Mathf.PI / 180))*Mathf.Cos(ganma * (Mathf.PI / 180))*(z*dt);
		result[1] = Mathf.Cos(beta * (Mathf.PI / 180)) * (y * dt) - Mathf.Sin(ganma * (Mathf.PI / 180)) * (z * dt);
		result[2] = (Mathf.Sin(ganma * (Mathf.PI / 180)) / Mathf.Cos(beta * (Mathf.PI / 180))) * (y * dt) + (Mathf.Cos(ganma * (Mathf.PI / 180)) / Mathf.Cos(beta * (Mathf.PI / 180))) * (z * dt);

		return result;
    }
}