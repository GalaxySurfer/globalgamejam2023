using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable, CreateAssetMenu]
public class DialogueChain : ScriptableObject
{
	public int Id;
	public List<DialogueText> Chain;
}