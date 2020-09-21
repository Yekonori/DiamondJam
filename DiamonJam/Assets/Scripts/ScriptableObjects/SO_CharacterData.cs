using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "CharacterData", menuName = "Dialogue/CharacterData", order = 1)]
public class SO_CharacterData : ScriptableObject
{
    [SerializeField]
	private string characterName;
	public string CharacterName
	{
		get { return characterName; }
	}

	[SerializeField]
	private Sprite[] characterSprites;
	public Sprite[] CharacterSprites
	{
		get { return characterSprites; }
	}
}
