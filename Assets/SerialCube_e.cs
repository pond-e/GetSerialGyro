using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SerialCube_e : MonoBehaviour
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
			// MPU9250��DMP����I�C���[�p���擾���Ďg�p����X�N���v�g
			text.text = "x:" + angles[0] + "\n" + "y:" + angles[1] + "\n" + "z:" + angles[2] + "\n" + "z:"; // �V���A���̒l���e�L�X�g�ɕ\��

			// Vector�͑O���珇�Ԃ�x,y,z�����ǁA���̂܂܃Z�b�g�����
			// Unity��̉�]�̌����ڂ��ςɂȂ�̂ŁAy,z�̒l�����ւ��Ă���B
			Vector3 angle = new Vector3(float.Parse(angles[0]), float.Parse(angles[2]), float.Parse(angles[1]));
			cube.transform.rotation = Quaternion.Euler(angle);
		}
		catch (System.Exception e)
		{
			Debug.LogWarning(e.Message);
		}
	}
}