using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SystemElement : MonoBehaviour
{
	public Button ButtonElement;
	public Image Icon;
	public TMP_Text ElementText;

	public Sprite FolderSprite;
	public Sprite TextSprite;
	public Sprite ImageSprite;

	private int _id;

	public void InitFolderButton(string folderName, int viewId)
	{
		Icon.sprite = FolderSprite;
		ElementText.text = folderName;
		_id = viewId;
		ButtonElement.onClick.RemoveAllListeners();
		ButtonElement.onClick.AddListener(() =>
		{
			GameManager.Instance.OpenFolder(_id);
		});
	}

	public void InitTextFileButton(string fileName, int fileId)
	{
		Icon.sprite = TextSprite;
		ElementText.text = fileName;
		_id = fileId;
		ButtonElement.onClick.RemoveAllListeners();
		ButtonElement.onClick.AddListener(() =>
		{
			GameManager.Instance.OpenTextFile(_id);
		});
	}

	public void InitImageFileButton(string fileName, int fileId)
	{
		Icon.sprite = ImageSprite;
		ElementText.text = fileName;
		_id = fileId;
		ButtonElement.onClick.RemoveAllListeners();
		ButtonElement.onClick.AddListener(() =>
		{
			GameManager.Instance.OpenImageFile(_id);
		});
	}
}
