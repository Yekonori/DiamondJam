using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class QuestionData
{
	[SerializeField]
	private string questionID;
	public string QuestionID
	{
		get { return questionID; }
	}

	[SerializeField]
	private string questionTag;
	public string QuestionTag
	{
		get { return questionTag; }
	}

	[SerializeField]
	private int questionDifficulty;
	public int QuestionDifficulty
	{
		get { return questionDifficulty; }
	}
}




[System.Serializable]
public class DiscussionData
{
	[SerializeField]
	private string discussionID;
	public string DiscussionID
	{
		get { return discussionID; }
	}

	[SerializeField]
	private string discussionTag;
	public string DiscussionTag
	{
		get { return discussionTag; }
	}
}

[System.Serializable]
public class DiscussionCharacterData
{
	[SerializeField]
	private SO_CharacterData characterData;
	public SO_CharacterData CharacterData
	{
		get { return characterData; }
	}

	[SerializeField]
	private string discussionID;
	public string DiscussionID
	{
		get { return discussionID; }
	}

	[SerializeField]
	private string discussionTag;
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

	[TextArea()]
	[SerializeField]
	private string characterDescription;
	public string CharacterDescription
	{
		get { return characterDescription; }
	}


	//[Space]
	//[Space]
	//[Space]
	[Header("Discussion du personnage quand le joueur lui demande de parler de lui-même")]
	[SerializeField]
	private DiscussionData[] discussionDatas;
	public DiscussionData[] DiscussionDatas { get; set; }



	[Header("Discussion du personnage quand le joueur lui demande de parler d'un autre perso")]
	[SerializeField]
	private DiscussionCharacterData[] discussionCharacterDatas;
	public DiscussionCharacterData[] DiscussionCharacterDatas { get; set; }


	[Space]
	[Space]
	[Space]

	[Header("Questions posé si le joueur porte le masque de ce personnage\n" +
			" - Questions ID est l'identifiant du dialogue\n" +
			" - Questions Tag est le nom de la variable pour sélectionner en priorité cette question")]
	[SerializeField]
	private QuestionData[] questionDatas;
	public QuestionData[] QuestionDatas { get; set; }
}
