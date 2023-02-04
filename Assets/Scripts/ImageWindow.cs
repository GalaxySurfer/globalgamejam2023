using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImageWindow : MonoBehaviour
{
	public TMP_Text FileNameText;
	public Image FileImage;

	public void Init(string fileName, Sprite image)
	{
		FileNameText.text = fileName;
		FileImage.sprite = image;
	}

	public void Close()
	{
		Destroy(gameObject);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape)) Close();
	}
}
