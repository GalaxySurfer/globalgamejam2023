using System;
using UnityEngine;

[Serializable, CreateAssetMenu]
public class ImageFile : ScriptableObject
{
	public int Id;
	public string FileName;
	public Sprite Image;
}
