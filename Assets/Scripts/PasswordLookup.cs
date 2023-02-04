using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using static View;

[Serializable, CreateAssetMenu]
public class PasswordLookup : ScriptableObject
{
	public List<PasswordPair> PasswordList;

	[Serializable]
	public class PasswordPair
	{
		public int Id;
		public string Password;
		public string Hint;
		public ElementType Type;
	}
}
