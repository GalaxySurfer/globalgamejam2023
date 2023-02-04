using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static CuttyReactionTable;
using static DialogueText;

public class Cutty : MonoBehaviour
{
	public Image CuttyBackground;
	public GameObject CuttyParent;
	public Image CuttyImage;
	public GameObject SpeechBubble;
	public TMP_Text SpeechBubbleText;
	public GameObject ChoiceParent;
	public GameObject ChoicePrefab;

	private CuttyReactionTable _reactionTable;

	private void Start()
	{
		_reactionTable = Resources.LoadAll<CuttyReactionTable>("")[0];
	}

	public void EvaluateWorldState()
	{
		int worldState = GameManager.Instance.WorldState;
		Reaction reaction = _reactionTable.ReactionList.FirstOrDefault(x => x.WorldState == worldState);
		if (reaction != null)
		{
			ToggleSpeechBubble(true);
			ToggleBlockScreen(true);
			GameManager.Instance.StartNewDialogueChain(reaction.DialogueChainId);
			GameManager.Instance.NextDialogueText();
		}
	}

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
			UnityEvent dereferencedEvent = choice.OnChoice;
			button.GetComponentInChildren<Button>().onClick.AddListener(() =>
			{
				dereferencedEvent?.Invoke();
			});
			button.GetComponentInChildren<TMP_Text>().text = choice.ChoiceName;
		}
	}
}
