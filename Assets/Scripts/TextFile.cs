using System;
using UnityEngine;

[Serializable, CreateAssetMenu]
public class TextFile : ScriptableObject
{
	public int Id;
	[TextArea]
	public string Text;
}
