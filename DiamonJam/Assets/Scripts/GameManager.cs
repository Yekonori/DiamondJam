using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
    ChangeCharacter swipeManager;
    [SerializeField]
    MaskSelectionManager maskSelectionManager;
    [SerializeField]
    DialogueSelectorManager dialogueSelectorManager;

    [Header("HUD")]
    [SerializeField]
    Transform guestTransform;
    [SerializeField]
    Image imageGuestPrefab;
    [SerializeField]
    Image imageCurrentMask;
    [SerializeField]
    TMPro.TextMeshProUGUI textTurn;
    Animator animatorTextTurn;

    List<Image> imageGuests = new List<Image>();

    [Header("Debug")]
    [SerializeField]
    SO_CharacterData debugInterlocutor;

    [SerializeField]
    private bool murderPreviousTurn = false;

    //private int currentDiscussionNumber = 2;
    private int currentInterogationNumber = 3;

    private int currentPlayerHealth;


    SO_CharacterData currentMask;
    SO_CharacterData currentInterlocutor;

    private void Start()
    {
        animatorTextTurn = textTurn.GetComponent<Animator>();
        currentMask = debugInterlocutor;
        currentInterlocutor = debugInterlocutor;
        StartNewTurn();
    }


    private void StartNewTurn()
    {
        turn -= 2;
        DrawHUD();
        if (murderPreviousTurn == true)
        {
            // Go To Interrogation
            StartCoroutine(StartQuestionCoroutine());
            //StartQuestion();
        }
        else
        {
            StartSwipe();
            //StartDiscussionPhase();
        }
        murderPreviousTurn = false;
    }

    private void DrawHUD()
    {
        imageCurrentMask.sprite = currentMask.CharacterMask;
        for (int i = 0; i < guestsList.Count; i++)
        {
            if(imageGuests.Count <= i)
            {
                imageGuests.Add(Instantiate(imageGuestPrefab, guestTransform));
            }
            imageGuests[i].gameObject.SetActive(true);
            imageGuests[i].sprite = guestsList[i].CharacterMask;
        }
        for (int i = guestsList.Count; i < imageGuests.Count; i++)
        {
            imageGuests[i].gameObject.SetActive(false);
        }
        DrawNewTurn();

    }

    private void DrawNewTurn()
    {
        string hour = (turn / 2) + "h";
        if (turn % 2 != 0)
        {
            hour = hour + "30";
        }
        textTurn.text = hour;
        animatorTextTurn.SetTrigger("Feedback");
    }



    // ================================================================================================================================== //
    //   INTERROGATION  
    // ================================================================================================================================== //
    private IEnumerator StartQuestionCoroutine()
    {
        yield return new WaitForSeconds(2f);
        dialogueManager.OnDialogueEnd += EndQuestion;
        currentPlayerHealth = playerHealth;
        currentInterogationNumber = interogationNumber;
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
            StartDiscussionPhase();
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
        swipeManager.InitListGuests(guestsList);
    }


    public void EndSwipe()
    {
        currentInterlocutor = swipeManager.GetCurrentNP_SO();
        swipeManager.ChangeChoosingMode(false);
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
        DrawNewTurn();
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
        moveCharacterPivot.MoveToNewParent(characterPositionDead, 500);
        StartCoroutine(KillCoroutine());
    }
    private IEnumerator KillCoroutine()
    {
        yield return new WaitForSeconds(6.4f);
        moveCharacterPivot.MoveToNewParent(characterPositionDefault, 1);
        StartNewTurn();
        yield return new WaitForSeconds(1.1f);
        murderPanel.gameObject.SetActive(false);
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
    [SerializeField]
    MoveCharacterPivot moveCharacterPivot;
    [SerializeField]
    Transform characterPositionDefault;
    [SerializeField]
    Transform characterPositionGameOver;
    [SerializeField]
    Transform characterPositionDead;

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
        moveCharacterPivot.MoveToNewParent(characterPositionGameOver);
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
