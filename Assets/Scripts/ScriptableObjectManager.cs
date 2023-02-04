using System;
using UnityEngine;

[Serializable, CreateAssetMenu]
public class ScriptableObjectManager : ScriptableObject
{
	public void NextDialogueText() => GameManager.Instance.NextDialogueText();
	public void SetNewDialogueChain(int chainId) => GameManager.Instance.StartNewDialogueChain(chainId);
	
	public void SetNewWorldState(int newState) => GameManager.Instance.SetWorldState(newState);
	public void RefreshView() => GameManager.Instance.RefreshView();

	public void ToggleCutty(bool state) => GameManager.Instance.CuttyScreen.ToggleCutty(state);
	public void ToggleSpeechBubble(bool state) => GameManager.Instance.CuttyScreen.ToggleSpeechBubble(state);
	public void ToggleBlockScreen(bool state) => GameManager.Instance.CuttyScreen.ToggleBlockScreen(state);
}