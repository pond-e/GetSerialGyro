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
		//�M������M�����Ƃ��ɁA���̃��b�Z�[�W�̏������s��
		serialHandler.OnDataReceived += OnDataReceived;
	}

	// Update is called once per frame
	void Update()
	{

	}

	/*
	 * �V���A�����󂯎�������̏���
	 */
	void OnDataReceived(string message)
	{
		try
		{
			string[] angles = message.Split(',');
			// MPU9250��DMP����N�H�[�^�j�I�����擾���Ďg�p���邱�Ƃ�ڎw���X�N���v�g
			text.text = "w:" + angles[0] + "\n" + "x:" + angles[1] + "\n" + "y:" + angles[2] + "\n" + "z:" + angles[3] + "\n"; // �V���A���̒l���e�L�X�g�ɕ\��

			/*
			float x_value = -1.0f * float.Parse(angles[0]);
			float y_value = float.Parse(angles[1]);
			float z_value = float.Parse(angles[2]); // �E��n�ƍ���n
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