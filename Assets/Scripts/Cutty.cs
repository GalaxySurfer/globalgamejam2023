using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static DialogueText;

public class Cutty : MonoBehaviour
{
	public Action<int> OnChoice;

	public Image CuttyBackground;
	public GameObject CuttyParent;
	public Image CuttyImage;
	public GameObject SpeechBubble;
	public TMP_Text SpeechBubbleText;
	public GameObject ChoiceParent;
	public GameObject ChoicePrefab;

	public void ToggleCutty(bool state) => CuttyParent.gameObject.SetActive(state);
	public void ToggleSpeechBubble(bool state) => SpeechBubble.gameObject.SetActive(state);
	public void ToggleBlockScreen(bool state) => CuttyBackground.gameObject.SetActive(state);

	public void ShowCutty(bool blockScreen = false)
	{
		ToggleCutty(true);
		ToggleSpeechBubble(false);
		if (blockScreen) ToggleBlockScreen(true);
	}

	public void HideCutty()
	{
		ToggleSpeechBubble(false);
		ToggleCutty(false);
		ToggleBlockScreen(false);
	}

	public void SetSpeechBubbleText(string text) => SpeechBubbleText.text = text;
	public void SetCuttyImage(Sprite sprite) => CuttyImage.sprite = sprite;
	public void SetChoices(IEnumerable<Choice> choices)
	{
		// Clear parent first
		for (int index = ChoiceParent.transform.childCount - 1; index >= 0; index--)
		{
			Destroy(ChoiceParent.transform.GetChild(index).gameObject);
		}

		foreach (Choice choice in choices)
		{
			GameObject button = Instantiate(ChoicePrefab, ChoiceParent.transform);
			int dereferencedId = choice.ChoiceId;
			button.GetComponent<Button>().onClick.AddListener(() =>
			{
				OnChoice?.Invoke(dereferencedId);
			});
			button.GetComponentInChildren<TMP_Text>().text = choice.ChoiceName;
		}
	}
}
