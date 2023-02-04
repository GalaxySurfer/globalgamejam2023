using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable, CreateAssetMenu]
public class CuttyReactionTable : ScriptableObject
{
	public List<Reaction> ReactionList;

	[Serializable]
	public class Reaction
	{
		public int WorldState;
		public int DialogueChainId;
	}
}
