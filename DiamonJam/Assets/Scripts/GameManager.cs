using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    Discussion,
    SelectCharacter,
    Interrogation,

}


public class GameManager : MonoBehaviour
{
    [Header("Parameter")]
    [SerializeField]
    private int turn = 20;
    [SerializeField]
    private int discussionNumber = 2;
    [SerializeField]
    private int interogationNumber = 3;

    [Header("Data")]
    [SerializeField]
    List<SO_CharacterData> guestsList;

    [Header("Managers")]
    [SerializeField]
    DialogueManager dialogueManager;
    [SerializeField]
    Swipe swipeManager;
    [SerializeField]
    DialogueSelectorManager dialogueSelectorManager;


    private bool murderPreviousTurn = false;

    private int currentDiscussionNumber = 2;
    private int currentInterogationNumber = 3;


    SO_CharacterData currentMask;
    SO_CharacterData currentInterlocutor;
    SO_CharacterData characterToDiscuss;

    private void Start()
    {
        StartNewTurn();
    }


    private void StartNewTurn()
    {
        turn -= 1;
        if(murderPreviousTurn == true)
        {
            // Go To Interrogation
            StartQuestion();
        }
        else
        {
            // Go To Character Selection
            
        }
        murderPreviousTurn = false;
    }





    //   INTERROGATION   =================================================================================================================================================
    public void StartQuestion()
    {
        string dialog = dialogueSelectorManager.SelectQuestion(currentMask);
        dialogueManager.StartDialogue(dialog);
    }


    public void EndQuestion()
    {
        currentInterogationNumber -= 1;
        if (currentInterogationNumber <= 0) // Plus de question, on passe à la sélection
        {

        }
        else // Encore des questions 
        {
            StartQuestion();
        }
    }

    // ======================================================================================================================================================================








    //   DISCUSSION   =================================================================================================================================================
    private void StartDiscussionSelection()
    {

    }

    /*public void StartDiscussion()
    {
        string dialog = dialogueSelectorManager.SelectDiscussion(currentInterlocutor);
        dialogueManager.StartDialogue(dialog);
    }*/
    public void StartDiscussionCharacter()
    {
        string dialog = dialogueSelectorManager.SelectDiscussionCharacter(currentInterlocutor, characterToDiscuss);
        dialogueManager.StartDialogue(dialog);
    }
    public void CancelDiscussion()
    {
        
    }

    public void EndDiscussion()
    {
        currentDiscussionNumber -= 1;
        if(currentDiscussionNumber <= 0) // Plus de discussion, go to choix du meurte
        {

        }
        else // Encore des discussion à faire, on retourne au menu précédent
        {
            StartDiscussionSelection();
        }
    }
    // ======================================================================================================================================================================










    //   CHOIX DU MEURTRE   =================================================================================================================================================
    public void StartMurderSelection()
    {
        string dialog = dialogueSelectorManager.SelectDiscussionCharacter(currentInterlocutor, characterToDiscuss);
        dialogueManager.StartDialogue(dialog);
    }
    public void Kill()
    {
        currentMask = currentInterlocutor;
        murderPreviousTurn = true;
        StartNewTurn();
    }
    public void Spare()
    {
        StartNewTurn();
    }
    // ======================================================================================================================================================================
}
