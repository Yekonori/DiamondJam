﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class QuestionData
{
	[SerializeField]
	private string questionID = "";
	public string QuestionID
	{
		get { return questionID; }
	}

	[SerializeField]
	private int questionDifficulty = 1;
	public int QuestionDifficulty
	{
		get { return questionDifficulty; }
	}

	[SerializeField]
	private string questionTag = "";
	public string QuestionTag
	{
		get { return questionTag; }
	}
}




[System.Serializable]
public class DiscussionData
{
	[SerializeField]
	private string discussionID = "";
	public string DiscussionID
	{
		get { return discussionID; }
	}

}

[System.Serializable]
public class DiscussionCharacterData
{
	[SerializeField]
	private SO_CharacterData characterData = null;
	public SO_CharacterData CharacterData
	{
		get { return characterData; }
	}

	[SerializeField]
	private string discussionID = "";
	public string DiscussionID
	{
		get { return discussionID; }
	}

	[SerializeField]
	private string discussionTag = "";
	public string DiscussionTag
	{
		get { return discussionTag; }
	}
}



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
	private GameObject characterModel;
	public GameObject CharacterModel
	{
		get { return characterModel; }
	}

	[SerializeField]
	private GameObject characterModelDead;
	public GameObject CharacterModelDead
	{
		get { return characterModelDead; }
	}

	[SerializeField]
	private Sprite characterMask;
	public Sprite CharacterMask
	{
		get { return characterMask; }
	}

	[SerializeField]
	private Sprite characterBackground;
	public Sprite CharacterBackground
	{
		get { return characterBackground; }
	}

	[TextArea()]
	[SerializeField]
	private string characterDescription;
	public string CharacterDescription
	{
		get { return characterDescription; }
	}


	[Header("(Character)    (Dialogue ID)    (Dialogue Tag)")]
	[Header(" Discussion du perso quand le joueur lui demande de parler d'un autre perso")]
	[Space(50)]
	[SerializeField]
	private DiscussionCharacterData[] discussionCharacterDatas;
	public DiscussionCharacterData[] DiscussionCharacterDatas
	{
		get { return discussionCharacterDatas; }
	}



	[Header("(Difficulté de la question)    (Dialogue ID)     (Dialogue Tag)")]
	[Header(" Questions posé si le joueur porte le masque de ce personnage")]
	[Space(50)]
	[SerializeField]
	private QuestionData[] questionDatas;
	public QuestionData[] QuestionDatas
	{
		get { return questionDatas; }
	}
}
