using Microsoft.CSharp.RuntimeBinder;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SerialCube_w_to_q : MonoBehaviour
{

    public SerialHandler serialHandler;
    public Text text;
    public GameObject cube;
    public Text culc_text;

    private float dt = 0.1f;

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
            // 取得した角速度をクォータニオンに変換する(参考:https://qiita.com/drken/items/0639cf34cce14e8d58a5)
            text.text = "x:" + angles[0] + "\n" + "y:" + angles[1] + "\n" + "z:" + angles[2] + "\n"; // シリアルの値をテキストに表示
            
            Vector3 angle = new Vector3(float.Parse(angles[1]), float.Parse(angles[2]), -1.0f * float.Parse(angles[0]));
            Quaternion tmp_e = Quaternion.Euler(angle * dt);
            Quaternion tmp_qe = cube.transform.rotation * tmp_e;
            cube.transform.rotation = tmp_qe;
            
            culc_text.text = "x:" + tmp_e.x + "\n" + "y:" + tmp_e.y + "\n" + "z:" + tmp_e.z + "\n" + "w:" + tmp_e.w + "\n";
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }
}