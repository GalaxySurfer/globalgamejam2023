using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static PasswordLookup;
using static View;

public class PasswordWindow : MonoBehaviour
{
	public TMP_Text HintText;
	public TMP_InputField InputField;
	public Image ErrorImage;
	public Button OkButton;

	private int _id;

	public void InitPasswordFolder(string hint, int id)
	{
		HintText.text = GameManager.Instance.WorldState > 1 ? hint : "";
		_id = id;
		ErrorImage.gameObject.SetActive(false);
		InputField.Select();
		InputField.ActivateInputField();
		OkButton.onClick.AddListener(() =>
		{
			bool passed = GameManager.Instance.CheckPassword(_id, InputField.text, ElementType.Folder);
			if (passed == false)
			{
				StopAllCoroutines();
				StartCoroutine(DoFlashErrorIcon());
			}
			else
			{
				GameManager.Instance.OpenFolder(_id, false);
				PasswordPair pair = GameManager.Instance.GetPasswordPair(_id, ElementType.Folder);
				if(pair != null && pair.NewWorldState > -1) GameManager.Instance.SetWorldState(pair.NewWorldState);
				Destroy(gameObject);
			}
		});
	}

	public void InitPasswordText(string hint, int id)
	{
		HintText.text = GameManager.Instance.WorldState > 1 ? hint : "";
		_id = id;
		ErrorImage.gameObject.SetActive(false);
		InputField.Select();
		InputField.ActivateInputField();
		OkButton.onClick.AddListener(() =>
		{
			bool passed = GameManager.Instance.CheckPassword(_id, InputField.text, ElementType.Text);
			if (passed == false)
			{
				StopAllCoroutines();
				StartCoroutine(DoFlashErrorIcon());
			}
			else
			{
				GameManager.Instance.OpenTextFile(_id, false);
				PasswordPair pair = GameManager.Instance.GetPasswordPair(_id, ElementType.Text);
				if (pair != null && pair.NewWorldState > -1) GameManager.Instance.SetWorldState(pair.NewWorldState);
				Destroy(gameObject);
			}
		});
	}

	public void InitPasswordImage(string hint, int id)
	{
		HintText.text = GameManager.Instance.WorldState > 1 ? hint : "";
		_id = id;
		ErrorImage.gameObject.SetActive(false);
		InputField.Select();
		InputField.ActivateInputField();
		OkButton.onClick.AddListener(() =>
		{
			bool passed = GameManager.Instance.CheckPassword(_id, InputField.text, ElementType.Image);
			if (passed == false)
			{
				StopAllCoroutines();
				StartCoroutine(DoFlashErrorIcon());
			}
			else
			{
				GameManager.Instance.OpenImageFile(_id, false);
				PasswordPair pair = GameManager.Instance.GetPasswordPair(_id, ElementType.Image);
				if (pair != null && pair.NewWorldState > -1) GameManager.Instance.SetWorldState(pair.NewWorldState);
				Destroy(gameObject);
			}
		});
	}

	public void InitPasswordBin(string hint, int id)
	{
		HintText.text = GameManager.Instance.WorldState > 1 ? hint : "";
		_id = id;
		ErrorImage.gameObject.SetActive(false);
		InputField.Select();
		InputField.ActivateInputField();
		OkButton.onClick.AddListener(() =>
		{
			bool passed = GameManager.Instance.CheckPassword(_id, InputField.text, ElementType.Bin);
			if (passed == false)
			{
				StopAllCoroutines();
				StartCoroutine(DoFlashErrorIcon());
			}
			else
			{
				PasswordPair pair = GameManager.Instance.GetPasswordPair(_id, ElementType.Bin);
				if (pair != null && pair.NewWorldState > -1) GameManager.Instance.SetWorldState(pair.NewWorldState);
				Destroy(gameObject);
			}
		});
	}

	public void Cancel() => Destroy(gameObject);

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && GameManager.Instance.CuttyScreen.CuttyBackground.isActiveAndEnabled == false)
		{
			Cancel();
		}
		else if (Input.GetKeyUp(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return) && GameManager.Instance.CuttyScreen.CuttyBackground.isActiveAndEnabled == false)
		{
			OkButton.onClick.Invoke();
		}
	}

	private IEnumerator DoFlashErrorIcon()
	{
		float repeatDelay = .15f;
		ErrorImage.gameObject.SetActive(true);
		yield return new WaitForSeconds(repeatDelay);
		ErrorImage.gameObject.SetActive(false);
		yield return new WaitForSeconds(repeatDelay);
		ErrorImage.gameObject.SetActive(true);
		yield return new WaitForSeconds(repeatDelay);
		ErrorImage.gameObject.SetActive(false);
		yield return new WaitForSeconds(repeatDelay);
		ErrorImage.gameObject.SetActive(true);
		yield return new WaitForSeconds(repeatDelay);
		ErrorImage.gameObject.SetActive(false);
	}
}
