using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextWindow : MonoBehaviour
{
	public TMP_Text FileNameText;
	public TMP_Text FileText;

	public void Init(string fileName, string text)
	{
		FileNameText.text = fileName;
		FileText.text = text;
	}

	public void Close()
	{
		GameManager.Instance.MouseClickSource.Play();
		Destroy(gameObject);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape)) Close();
	}
}
