using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DialogueSelectorManager : MonoBehaviour
{

    private int levelDifficulty;
    public int LevelDifficulty
    {
        get { return levelDifficulty; }
        set { levelDifficulty = value; }
    }

    List<string> playerKnowledge = new List<string>();




    /*public string SelectDiscussion(SO_CharacterData interlocutor)
    {
        /*if (interlocutor.DiscussionDatas.Length == 0)
            return null;

        int rand = Random.Range(0, interlocutor.DiscussionDatas.Length); // On choisi une info au pif
        playerKnowledge.Add(interlocutor.DiscussionDatas[rand].DiscussionID); // On enregistre que le joueur a cet info
        return interlocutor.DiscussionDatas[rand].DiscussionID; 
        return null;
    }*/



    public string SelectDiscussionCharacter(SO_CharacterData interlocutor, SO_CharacterData character)
    {
        if (interlocutor.DiscussionCharacterDatas.Length == 0)
            return null;
        DiscussionCharacterData discussion = null;
        for (int i = 0; i < interlocutor.DiscussionCharacterDatas.Length; i++)
        {
            if(interlocutor.DiscussionCharacterDatas[i].CharacterData.name == character.name) 
            {
                discussion = interlocutor.DiscussionCharacterDatas[i];
                break;
            }
        }
        if (discussion == null)
            return null;

        // On ajoute les tags à la connaissance du joueur
        string tag = discussion.DiscussionTag;
        var tags = tag.Split('-');
        for (int i = 0; i < tags.Length; i++)
        {
            playerKnowledge.Add(tags[i]);
        }
        return discussion.DiscussionID;

    }




    public string SelectQuestion(SO_CharacterData maskWorn)
    {
        for (int i = 0; i < maskWorn.QuestionDatas.Length; i++)
        {
            for (int j = 0; j < playerKnowledge.Count; j++)
            {
                if (maskWorn.QuestionDatas[i].QuestionTag == playerKnowledge[j])
                {
                    return maskWorn.QuestionDatas[i].QuestionID;
                }
            }
        }
        // Si le joueur n'a pas le bon tag, on prend un truc au pif
        return maskWorn.QuestionDatas[Random.Range(0, maskWorn.QuestionDatas.Length-1)].QuestionID;
    }

}
