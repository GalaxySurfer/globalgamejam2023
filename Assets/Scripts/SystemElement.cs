using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SystemElement : MonoBehaviour
{
    public Button ButtonElement;
    public Image Icon;
    public TMP_Text ElementText;

    public Sprite FolderSprite;
    public Sprite LockedFolderSprite;
    public Sprite TextSprite;
    public Sprite LockedTextSprite;
    public Sprite ImageSprite;
    public Sprite LockedImageSprite;
	public Sprite LockedBinSprite;

    private int _id;

    public void InitFolderButton(string folderName, int viewId, bool hasPassword)
    {
        Icon.sprite = hasPassword ? LockedFolderSprite : FolderSprite;
        ElementText.text = folderName;
        _id = viewId;
        ButtonElement.onClick.RemoveAllListeners();
        ButtonElement.onClick.AddListener(() =>
        {
            GameManager.Instance.OpenFolder(_id, hasPassword);
        });
    }

    public void InitTextFileButton(string fileName, int fileId, bool hasPassword)
    {
        Icon.sprite = hasPassword ? LockedTextSprite : TextSprite;
        ElementText.text = fileName;
        _id = fileId;
        ButtonElement.onClick.RemoveAllListeners();
        ButtonElement.onClick.AddListener(() =>
        {
            GameManager.Instance.OpenTextFile(_id, hasPassword);
        });
    }

    public void InitImageFileButton(string fileName, int fileId, bool hasPassword)
    {
        Icon.sprite = hasPassword ? LockedImageSprite : ImageSprite;
        ElementText.text = fileName;
        _id = fileId;
        ButtonElement.onClick.RemoveAllListeners();
        ButtonElement.onClick.AddListener(() =>
        {
            GameManager.Instance.OpenImageFile(_id, hasPassword);
        });
    }

	public void InitBinFileButton(string fileName, int fileId)
	{
		// Always has password
		Icon.sprite = LockedBinSprite;
		ElementText.text = fileName;
		_id = fileId;
		ButtonElement.onClick.RemoveAllListeners();
		ButtonElement.onClick.AddListener(() =>
		{
			GameManager.Instance.OpenBinFile(_id);
		});
	}
}
