using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SerialCube_q : MonoBehaviour
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
			text.text = "w:" + angles[0] + "\n" + "x:" + angles[1] + "\n" + "y:" + angles[2] + "\n" + "z:" + angles[3] + "\n"; // シリアルの値をテキストに表示

			// Vectorは前から順番にx,y,zだけど、そのままセットすると
			// Unity上の回転の見た目が変になるので、y,zの値を入れ替えている。
			//Vector3 angle = new Vector3(float.Parse(angles[0]), float.Parse(angles[2]), float.Parse(angles[1]));
			//cube.transform.rotation = Quaternion.Euler(angle);

			/*
			float x_value = -1.0f * float.Parse(angles[0]);
			float y_value = float.Parse(angles[1]);
			float z_value = float.Parse(angles[2]); // 右手系と左手系
			*/
			Vector3 angle = new Vector3(Mathf.Sqrt(1.0f - Mathf.Pow(float.Parse(angles[1]), 2)), Mathf.Sqrt(1.0f - Mathf.Pow(-1.0f*float.Parse(angles[3]), 2)), Mathf.Sqrt(1.0f - Mathf.Pow(float.Parse(angles[2]), 2)));
			cube.transform.rotation = Quaternion.AngleAxis(180f * float.Parse(angles[0]), angle);
		}
		catch (System.Exception e)
		{
			Debug.LogWarning(e.Message);
		}
	}
}