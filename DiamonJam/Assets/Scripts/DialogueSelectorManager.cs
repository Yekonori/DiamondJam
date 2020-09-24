using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DialogueSelectorManager : MonoBehaviour
{

    [SerializeField]
    private int mediumDifficultyThreshold;
    [SerializeField]
    private int hardDifficultyThreshold;

    [SerializeField]
    private AudioClip easyMusic;
    [SerializeField]
    private AudioClip mediumMusic;
    [SerializeField]
    private AudioClip hardMusic;

    private int questionCount;


    private int levelDifficulty;
    public int LevelDifficulty
    {
        get { return levelDifficulty; }
        set { levelDifficulty = value; }
    }

    List<string> playerKnowledge = new List<string>();

    [Header("Debug")]
    [SerializeField]
    List<QuestionData> questionsPool = new List<QuestionData>();


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


    // A appelé avant Select question pour ne pas retomber sur les mêmes questions 2 fois
    public void CreateQuestions(SO_CharacterData maskWorn)
    {
        if(levelDifficulty < mediumDifficultyThreshold)
            SoundManager.Instance.PlayMusic(easyMusic);
        else if (levelDifficulty < hardDifficultyThreshold)
            SoundManager.Instance.PlayMusic(mediumMusic);
        else
            SoundManager.Instance.PlayMusic(hardMusic);
        questionsPool = maskWorn.QuestionDatas.ToList();
    }

    public string SelectQuestion(SO_CharacterData maskWorn)
    {
        QuestionData res;
        if (questionsPool.Count == 0)
            CreateQuestions(maskWorn);

        AddQuestionCount();
        for (int i = 0; i < questionsPool.Count; i++)
        {
            for (int j = 0; j < playerKnowledge.Count; j++)
            {
                if (questionsPool[i].QuestionTag == playerKnowledge[j])
                {
                    res = questionsPool[i];
                    questionsPool.RemoveAt(i);
                    return res.QuestionID;
                }
            }
        }
        // Si le joueur n'a pas le bon tag, on prend un truc au pif avec la bonne difficulté
        List<QuestionData> questions = new List<QuestionData>();
        for (int i = 0; i < questionsPool.Count; i++)
        {
            if (questionsPool[i].QuestionDifficulty == levelDifficulty)
            {
                questions.Add(questionsPool[i]);
            }
        }

        int rand = 0;
        if (questions.Count == 0)
        {
            rand = Random.Range(0, questionsPool.Count - 1);
            res = questionsPool[rand];
            questionsPool.RemoveAt(rand);
        }
        else
        {
            rand = Random.Range(0, questions.Count - 1);
            res = questions[rand];
            questionsPool.RemoveAt(rand);
        }
        return res.QuestionID;
    }

    private void AddQuestionCount()
    {
        questionCount += 1;
        if (questionCount == mediumDifficultyThreshold)
            levelDifficulty = 2;
        if (questionCount == hardDifficultyThreshold)
            levelDifficulty = 3;
    }

}
