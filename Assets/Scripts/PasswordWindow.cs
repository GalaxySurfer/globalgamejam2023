using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PasswordWindow : MonoBehaviour
{
    public TMP_Text HintText;
    public TMP_InputField InputField;
    public Image ErrorImage;
    public Button OkButton;

    private int _id;

    public void InitPasswordFolder(string hint, int id)
    {
        HintText.text = hint;
        _id = id;
        ErrorImage.gameObject.SetActive(false);
        OkButton.onClick.AddListener(() =>
        {
            bool passed = GameManager.Instance.CheckPassword(InputField.text, _id);
            if (passed == false)
            {
                //TODO: Show Error symbol next to password field
            }
            else
            {
                GameManager.Instance.OpenFolder(_id, false);
                Destroy(gameObject);
            }
        });
    }

    public void InitPasswordText(string hint, int id)
    {
        HintText.text = hint;
        _id = id;
        ErrorImage.gameObject.SetActive(false);
        OkButton.onClick.AddListener(() =>
        {
            bool passed = GameManager.Instance.CheckPassword(InputField.text, _id);
            if (passed == false)
            {
                //TODO: Show Error symbol next to password field
            }
            else
            {
                GameManager.Instance.OpenTextFile(_id, false);
                Destroy(gameObject);
            }
        });
    }

    public void InitPasswordImage(string hint, int id)
    {
        HintText.text = hint;
        _id = id;
        ErrorImage.gameObject.SetActive(false);
        OkButton.onClick.AddListener(() =>
        {
            bool passed = GameManager.Instance.CheckPassword(InputField.text, _id);
            if (passed == false)
            {
                //TODO: Show Error symbol next to password field
            }
            else
            {
                GameManager.Instance.OpenImageFile(_id, false);
                Destroy(gameObject);
            }
        });
    }

    public void Cancel() => Destroy(gameObject);
}
