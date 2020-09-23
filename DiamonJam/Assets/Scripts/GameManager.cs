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
    private int turn = 40;
    [SerializeField]
    private int playerHealth = 2;
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

    [SerializeField]
    private bool murderPreviousTurn = false;

    private int currentDiscussionNumber = 2;
    private int currentInterogationNumber = 3;

    private int currentPlayerHealth;


    SO_CharacterData currentMask;
    SO_CharacterData currentInterlocutor;

    private void Start()
    {
        currentMask = debugInterlocutor;
        currentInterlocutor = debugInterlocutor;
        StartNewTurn();
    }


    private void StartNewTurn()
    {
        turn -= 2;
        if(murderPreviousTurn == true)
        {
            // Go To Interrogation
            StartCoroutine(StartQuestionCoroutine());
            //StartQuestion();
        }
        else
        {
            // Go To Character Selection
            StartDiscussionPhase();
        }
        murderPreviousTurn = false;
    }





    // ================================================================================================================================== //
    //   INTERROGATION  
    // ================================================================================================================================== //
    private IEnumerator StartQuestionCoroutine()
    {
        yield return new WaitForSeconds(2f);
        dialogueManager.OnDialogueEnd += EndQuestion;
        currentPlayerHealth = playerHealth;
        StartQuestion();
    }

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
            dialogueManager.OnDialogueEnd -= EndQuestion;
        }
        else // Encore des questions 
        {
            StartQuestion();
        }
    }
    // ================================================================================================================================== //








    // ================================================================================================================================== //
    //   SELECTION PERSO 
    // ================================================================================================================================== //
    public void StartSwipe()
    {
        // StartSwipe
    }


    public void EndSwipe()
    {
        //currentInterlocutor = swipeManager.a     
        StartDiscussionPhase();
    }

    // ================================================================================================================================== //











    // ================================================================================================================================== //
    //   DISCUSSION  
    // ================================================================================================================================== //
    private void StartDiscussionPhase()
    {
        dialogueManager.OnDialogueEnd += EndDiscussion;
        StartDiscussionSelection();
    }

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

    public void EndDiscussion()
    {
        turn -= 1;
        StartDiscussionSelection();
    }

    // Call by bouton sur une scene
    public void CancelDiscussion()
    {
        dialogueManager.OnDialogueEnd -= EndDiscussion;
        maskSelectionManager.EndSelection();
        StartMurderSelection();
    }


    // ======================================================================================================================================================================






    [Header("Murder")]
    [SerializeField]
    GameObject murderPanel;


    // ================================================================================================================================== //
    //   CHOIX DU MEURTRE 
    // ================================================================================================================================== //
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
        yield return new WaitForSeconds(1f);
        murderPanel.gameObject.SetActive(false);
    }

    // ======================================================================================================================================================================




    [Header("GameOver")]
    [SerializeField]
    string gameOverDialog;
    [SerializeField]
    Shake shakeScreen;
    [SerializeField]
    Flash flashRedBackground;
    [SerializeField]
    Animator zawa;
    [SerializeField]
    Animator zawa2;
    [SerializeField]
    GameObject gameOverPanel;

    public void LoseHealth()
    {
        flashRedBackground.StartFlash();
        shakeScreen.ShakeEffect();

        currentPlayerHealth -= 1;
        if (currentPlayerHealth <= 0) 
        {
            dialogueManager.OnDialogueEnd -= EndQuestion;
            dialogueManager.InterruptDialog();
            GameOver();
        }
        else
        {
            zawa.SetTrigger("Feedback");
            zawa2.SetTrigger("Feedback");
        }
    }

    public void GameOver()
    {
        dialogueManager.OnDialogueEnd += GameOverAnimation;
        dialogueManager.StartDialogue(gameOverDialog);

    }

    public void GameOverAnimation()
    {
        gameOverPanel.SetActive(true);
        StartCoroutine(GameOverCoroutine());
    }

    private IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSeconds(8f);
        // Load scene
    }





}
