using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCharacter : MonoBehaviour
{
    #region Script Parameters

    //public GameObject playersField;
    
    public GameObject dialoguePanel;
    public GameObject chooseNPCPanel;
    public GameObject dialogueManager;

    #endregion

    #region Fields

    private float swipeDistanceThreshold = 50;

    private Vector2 startPosition;
    private Vector2 endPosition;

    [SerializeField] bool isChoosingNPC = true;
    private List<ScriptableObject> players;
    private int currentPlayerId;
    private GameObject currentPlayer;

    #endregion


    #region Unity Methods

    private void Start()
    {
        //players = new List<Transform>();

        //foreach(Transform transform in playersField.transform)
        //{
        //    players.Add(transform);
        //}

        currentPlayerId = 0;
        //currentPlayer = players[currentPlayerId].gameObject;
    }

    private void Update()
    {
        if (!isChoosingNPC) return;
        if (Input.touchCount == 1)
        {
            var touch = Input.touches[0];
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Stockage du point de départ
                    startPosition = touch.position;
                    break;
                case TouchPhase.Ended:
                    // Stockage du point de fin
                    endPosition = touch.position;
                    AnalyzeGesture(startPosition, endPosition);
                    break;
            }
        }
    }

    #endregion

    #region Swipe

    private void AnalyzeGesture(Vector2 start, Vector2 end)
    {
        // Distance
        if (Vector2.Distance(start, end) > swipeDistanceThreshold)
        {
            // Le mouvement est suffisamment ample
            // Debug.Log("StartPosition : " + startPosition + " --- EndPosition : " + endPosition);
            SwipePlayer(startPosition.x < endPosition.x ? true : false);
        }
    }

    private void SwipePlayer(bool toRight)
    {
        //players[currentPlayerId].gameObject.SetActive(false);

        /**
         * IF true : 
         * Swipe Right : 
         *  - Va en arrière
         *  - Si index = 0 -> revient au dernier
         * 
         * ELSE : 
         * Swipe Left : 
         *  - Va en avant
         *  - Si index = players.Count - 1 -> revient au premier
         * 
         */
        if (toRight)
        {
            if(currentPlayerId == 0)
            {
                currentPlayerId = players.Count - 1;
            }
            else
            {
                currentPlayerId--;
            }
        }
        else
        {
            if (currentPlayerId == players.Count - 1)
            {
                currentPlayerId = 0;
            }
            else
            {
                currentPlayerId++;
            }
        }


        //players[currentPlayerId].gameObject.SetActive(true);
        //currentPlayer = players[currentPlayerId].gameObject;
    }


    public void ChangeChoosingMode(bool b)
    {
        //Debug.Log("Button push");
        isChoosingNPC = b;
        //if (isChoosingNPC)
        //{
        //    chooseNPCPanel.SetActive(true);
        //    dialoguePanel.SetActive(false);
        //}
        //else
        //{
        //    chooseNPCPanel.SetActive(false);
        //    dialoguePanel.SetActive(true);
        //    dialogueManager.GetComponent<DialogueManager>().StartDialogue("TestNam");
        //}
    }

    public void InitListGuests(List<ScriptableObject> list)
    {
        players = list;
    }

    #endregion
}
