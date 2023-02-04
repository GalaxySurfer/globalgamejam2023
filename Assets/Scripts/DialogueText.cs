using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable, CreateAssetMenu]
public class DialogueText : ScriptableObject
{
	[TextArea]
	public string Text;
	public List<Choice> Choices;

	[Serializable]
	public class Choice
	{
		public int ChoiceId;
		public string ChoiceName;
	}
}
