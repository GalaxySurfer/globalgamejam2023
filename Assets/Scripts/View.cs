using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable, CreateAssetMenu]
public class View : ScriptableObject
{
	public int ViewId;
	public int PreviousViewId;
	public string Path;
	public List<ViewElement> Elements;

	[Serializable]
	public class ViewElement
	{
		public int Id;
		public string Name;
		public bool HasPassword;
		public ElementType Type;
		public UnityEvent OnOpen;
	}
	
	public enum ElementType
	{
		Folder,
		Text,
		Image,
		Bin
	}
}