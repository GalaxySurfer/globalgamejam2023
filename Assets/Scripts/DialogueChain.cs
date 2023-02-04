using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable, CreateAssetMenu]
public class DialogueChain : ScriptableObject
{
	public List<DialogueText> Chain;
}