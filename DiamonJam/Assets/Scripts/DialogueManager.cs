using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VIDE_Data; //<--- Import to use VD class
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    VIDE_Assign vIDE;

    [SerializeField]
    DialogueTextManager dialogueTextManager;








    [Header("Debug")]
    [SerializeField]
    TextMeshProUGUI textDialogName;
    [SerializeField]
    TextMeshProUGUI textDialog;

    Touch touch;
    bool pause = false;


    // Start is called before the first frame update
    void Start()
    {
        // vIDE.assignedDialogue =
        //Subscribe to some events and Begin the Dialogue

        VD.OnNodeChange += UpdateNode;
        VD.OnEnd += EndDialog;
        VD.BeginDialogue(GetComponent<VIDE_Assign>()); //This is the first most important method when using VIDE
    }


    void Update()
    {
        if (VD.isActive && pause == false)
        {
            if (Input.touchCount > 0) // Pas définitif si on met des boutons sur l'interface
            {
                touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    VD.Next();
                }
            }
            else if (Input.GetMouseButtonDown(0)) // Mettre un #if Editor à l'ocaz
            {
                VD.Next();
            }

        }
    }

    //Every time VD.nodeData is updated, this method will be called. (Because we subscribed it to OnNodeChange event)
    void UpdateNode(VD.NodeData data)
    {
        textDialogName.text = data.characterData.CharacterName;
        textDialog.text = data.comments[0];
        
        /*WipeAll(); //Turn stuff off first

        if (!data.isPlayer) //For NPC. Activate text gameobject and set its text
        {
            NPC_text.gameObject.SetActive(true);
            NPC_text.text = data.comments[data.commentIndex];
        }
        else //For Player. It will activate the required Buttons and set their text
        {
            for (int i = 0; i < PLAYER_text.Length; i++)
            {
                if (i < data.comments.Length)
                {
                    PLAYER_text[i].transform.parent.gameObject.SetActive(true);
                    PLAYER_text[i].text = data.comments[i];
                }
                else
                {
                    PLAYER_text[i].transform.parent.gameObject.SetActive(false);
                }

                PLAYER_text[0].transform.parent.GetComponent<Button>().Select();
            }
        }*/
    }

    void EndDialog(VD.NodeData data)
    {
        VD.OnNodeChange -= UpdateNode;
        VD.OnEnd -= EndDialog;
        VD.EndDialogue(); //Third most important method when using VIDE     

        // Clean des trucs
        textDialogName.text = "";
        textDialog.text = "";
    }











    public void Wait(float a)
    {
        pause = true;
        StartCoroutine(WaitCoroutine(a));

    }
    private IEnumerator WaitCoroutine(float t)
    {
        yield return new WaitForSeconds(t);
        VD.Next();
        pause = false;
    }
}
