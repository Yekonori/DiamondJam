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
    MaskSelectionManager maskSelectionManager;
    [SerializeField]
    DialogueSelectorManager dialogueSelectorManager;

    [Header("Debug")]
    [SerializeField]
    SO_CharacterData debugInterlocutor;

    private bool murderPreviousTurn = false;

    private int currentDiscussionNumber = 2;
    private int currentInterogationNumber = 3;


    SO_CharacterData currentMask;
    SO_CharacterData currentInterlocutor;

    private void Start()
    {

        dialogueManager.OnDialogueEnd += EndDiscussion;

        currentInterlocutor = debugInterlocutor;
        StartDiscussionSelection();
        //StartNewTurn();
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
        maskSelectionManager.StartSelection(guestsList);
    }

    // Call by bouton sur une scene
    public void StartDiscussionCharacter()
    {
        maskSelectionManager.EndSelection();
        string dialog = dialogueSelectorManager.SelectDiscussionCharacter(currentInterlocutor, maskSelectionManager.MaskSelected);
        dialogueManager.StartDialogue(dialog);
    }

    // Call by bouton sur une scene
    public void CancelDiscussion()
    {
        maskSelectionManager.EndSelection();
        StartMurderSelection();
    }

    public void EndDiscussion()
    {
        currentDiscussionNumber -= 1;
        if(currentDiscussionNumber <= 0) // Plus de discussion, go to choix du meurte
        {
            CancelDiscussion();
        }
        else // Encore des discussion à faire, on retourne au menu précédent
        {
            StartDiscussionSelection();
        }
    }
    // ======================================================================================================================================================================






    [Header("Murder")]
    [SerializeField]
    GameObject murderPanel;


    //   CHOIX DU MEURTRE   =================================================================================================================================================
    private void StartMurderSelection()
    {
        murderPanel.gameObject.SetActive(true);
    }
    public void Kill()
    {
        guestsList.Remove(currentInterlocutor);
        currentMask = currentInterlocutor;
        murderPreviousTurn = true;
        StartNewTurn();
    }
    public void Spare()
    {
        StartCoroutine(SpareCoroutine());
    }


    private IEnumerator SpareCoroutine()
    {
        yield return new WaitForSeconds(2f);
        StartNewTurn();
    }

    // ======================================================================================================================================================================
}
