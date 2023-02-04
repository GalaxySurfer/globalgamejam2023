using System;
using UnityEngine;

[Serializable, CreateAssetMenu]
public class TextFile : ScriptableObject
{
	public int Id;
	public string FileName;
	[TextArea]
	public string Text;
}
