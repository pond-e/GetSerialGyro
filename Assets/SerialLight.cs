using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SerialLight : MonoBehaviour
{

	public SerialHandler serialHandler;
	public Text text;

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
			text.text = message; // �V���A���̒l���e�L�X�g�ɕ\��
		}
		catch (System.Exception e)
		{
			Debug.LogWarning(e.Message);
		}
	}
}