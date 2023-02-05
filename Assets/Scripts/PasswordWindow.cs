using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static PasswordLookup;
using static View;

public class PasswordWindow : MonoBehaviour
{
	public TMP_Text HintText;
	public TMP_InputField InputField;
	public Image ErrorImage;
	public Button OkButton;
	public AudioSource ErrorSoundSource;
	public AudioSource KeyboardClickSource;

	private int _id;

    private void Start()
    {
		InputField.onValueChanged.AddListener((newString) =>
		{
			KeyboardClickSource.pitch = Random.Range(0.875f, 1f);
			KeyboardClickSource.Play();
		});
    }

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
				UnityEvent onOpenEvents = GameManager.Instance.GetViewWithSystemElementId(_id, ElementType.Folder);
				onOpenEvents?.Invoke();
				if(_id == 0) GameManager.Instance.OpenFolder(1, false);
				else GameManager.Instance.OpenFolder(_id, false);
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
				UnityEvent onOpenEvents = GameManager.Instance.GetViewWithSystemElementId(_id, ElementType.Text);
				onOpenEvents?.Invoke();
				GameManager.Instance.OpenTextFile(_id, false);
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
				UnityEvent onOpenEvents = GameManager.Instance.GetViewWithSystemElementId(_id, ElementType.Image);
				onOpenEvents?.Invoke();
				GameManager.Instance.OpenImageFile(_id, false);
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
				UnityEvent onOpenEvents = GameManager.Instance.GetViewWithSystemElementId(_id, ElementType.Bin);
				onOpenEvents?.Invoke();
				Destroy(gameObject);
			}
		});
	}

	public void Cancel()
	{
		GameManager.Instance.MouseClickSource.Play();
		Destroy(gameObject);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && GameManager.Instance.CuttyScreen.CuttyBackground.isActiveAndEnabled == false)
		{
			Cancel();
		}
		else if (Input.GetKeyUp(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return) && GameManager.Instance.CuttyScreen.CuttyBackground.isActiveAndEnabled == false)
		{
			GameManager.Instance.MouseClickSource.Play();
			OkButton.onClick.Invoke();
		}
	}

	private IEnumerator DoFlashErrorIcon()
	{
		ErrorSoundSource.Play();
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
