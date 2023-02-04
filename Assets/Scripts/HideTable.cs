using System;
using System.Collections.Generic;
using UnityEngine;
using static View;

[Serializable, CreateAssetMenu]
public class HideTable : ScriptableObject
{
	public List<HidePair> HideList;

	[Serializable]
	public class HidePair
	{
		public int WorldState;
		public int SystemElementId;
		public ElementType Type;
	}
}
