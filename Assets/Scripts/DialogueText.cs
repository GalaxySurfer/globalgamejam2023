using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable, CreateAssetMenu]
public class DialogueText : ScriptableObject
{
	[TextArea]
	public string Text;
	public Sprite CuttyImage;
	public List<Choice> Choices;

	[Serializable]
	public class Choice
	{
		public string ChoiceName;
		public UnityEvent OnChoice;
	}
}
