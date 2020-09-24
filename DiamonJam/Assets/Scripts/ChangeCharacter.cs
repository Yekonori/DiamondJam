using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
    public Transform whereToInstanciate;

    private float swipeDistanceThreshold = 100;

    private float startPosition;
    private float endPosition;

    [SerializeField] bool isChoosingNPC = true;
    private List<SO_CharacterData> players;
    private int currentPlayerId;
    private GameObject currentPlayer;

    [SerializeField]
    private Vector3 xOffSet = new Vector3(0.15f, 0f, 0f);

    public float timeToMove = 0.5f;
    #endregion


    #region Unity Methods

    private void Start()
    {
        currentPlayerId = 0;
    }

    private void Update()
    {
        if (!isChoosingNPC) return;
#if UNITY_EDITOR || UNITY_STANDALONE
        //swipe with mouse
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition.x;
        }

        if (Input.GetMouseButtonUp(0))
        {
            endPosition = Input.mousePosition.x;
            //Debug.Log(Mathf.Abs(endPosition - startPosition));
            AnalyzeGesture(startPosition, endPosition);
        }
#endif
#if UNITY_ANDROID || UNITY_IOS
        //swipe with finger
        if (Input.touchCount == 1)
        {
            var touch = Input.touches[0];
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Stockage du point de départ
                    startPosition = touch.position.x;
                    break;
                case TouchPhase.Ended:
                    // Stockage du point de fin
                    endPosition = touch.position.x;
                    AnalyzeGesture(startPosition, endPosition);
                    break;
            }
        }
#endif
    }

    #endregion

    #region Swipe

    private void AnalyzeGesture(float start, float end)
    {
        // Distance
        //if (Vector2.Distance(start, end) > swipeDistanceThreshold)
        if (Mathf.Abs(end - start) > swipeDistanceThreshold)
        {
            // Le mouvement est suffisamment ample
            // Debug.Log("StartPosition : " + startPosition + " --- EndPosition : " + endPosition);
            SwipePlayer(start < end ? true : false);
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

        if (currentPlayer != null) StartCoroutine(DestroyCurrentPlayer(toRight, currentPlayer)); //Destroy(currentPlayer);

        StartCoroutine(InstantiateNextPlayer(toRight, currentPlayerId));
        //currentPlayer = Instantiate(players[currentPlayerId].CharacterModel, whereToInstanciate);
        //players[currentPlayerId].gameObject.SetActive(true);
        //currentPlayer = players[currentPlayerId].gameObject;
    }


    public void ChangeChoosingMode(bool b)
    {
        isChoosingNPC = b;
        if (isChoosingNPC)
        {
            chooseNPCPanel.SetActive(true);
        }
        else
        {
            chooseNPCPanel.SetActive(false);
        }
    }

    public void InitListGuests(List<SO_CharacterData> list)
    {
        players = list;
        currentPlayerId = 0;
        if (currentPlayer != null) Destroy(currentPlayer);
        currentPlayer = Instantiate(players[currentPlayerId].CharacterModel, whereToInstanciate);

    }

    public SO_CharacterData GetCurrentNP_SO()
    {
        return players[currentPlayerId];
    }

    IEnumerator DestroyCurrentPlayer(bool ToRight, GameObject go)
    {
        float timer = 0f;
        Vector3 startPosition = go.transform.position;
        Vector3 endPosition = go.transform.position;
        if (ToRight)
        {
            endPosition += xOffSet;
        }
        else
        {
            endPosition -= xOffSet;
        }

        while (timer < timeToMove)
        {
            timer += Time.deltaTime;
            go.transform.localEulerAngles = Vector3.Lerp(startPosition, endPosition, timer / timeToMove);
            yield return null;
        }
        Destroy(go);
    }

    IEnumerator InstantiateNextPlayer(bool Toright, int currentID)
    {
        float timer = 0f;
        Transform startPosition = whereToInstanciate;

        if (Toright)
        {
            currentPlayer = Instantiate(players[currentID].CharacterModel, whereToInstanciate);
            currentPlayer.transform.localEulerAngles -= xOffSet;
            startPosition = currentPlayer.transform;

        }
        else
        {
            currentPlayer = Instantiate(players[currentID].CharacterModel, whereToInstanciate);
            currentPlayer.transform.localEulerAngles += xOffSet;
            startPosition = currentPlayer.transform;
        }

        while (timer < timeToMove)
        {
            timer += Time.deltaTime;
            currentPlayer.transform.localEulerAngles = Vector3.Lerp(startPosition.position, whereToInstanciate.localEulerAngles, timer / timeToMove);
            yield return null;
        }
    }



    public void InstantiateCharacter(SO_CharacterData character)
    {
        if(character == null)
        {
            return;
        }
        if (currentPlayer != null) Destroy(currentPlayer);
        currentPlayer = Instantiate(character.CharacterModel, whereToInstanciate);
    }

    public void InstantiateDeadModel(SO_CharacterData character)
    {
        if (currentPlayer != null) Destroy(currentPlayer);
        currentPlayer = Instantiate(character.CharacterModelDead, whereToInstanciate);
    }

    #endregion
}
