using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VIDE_Data; //<--- Import to use VD class
using TMPro;


public delegate void Action();

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    VIDE_Assign vIDE;

    [SerializeField]
    DialogueChoiceManager dialogueChoiceManager;
    [SerializeField]
    DialogueTextManager dialogueTextManager;


    [SerializeField]
    GameObject dialogueMenu;


    public event Action OnDialogueEnd;

    // Start is called before the first frame update
    void Start()
    {

    }


    public void StartDialogue(string dialogue)
    {
        dialogueMenu.gameObject.SetActive(true);

        vIDE.AssignNew(dialogue);

        VD.OnNodeChange += UpdateNode;
        VD.OnEnd += EndDialog;
        VD.BeginDialogue(vIDE);
    }




    //Every time VD.nodeData is updated, this method will be called. (Because we subscribed it to OnNodeChange event)
    void UpdateNode(VD.NodeData data)
    {
        if(data.isPlayer) // Le node est un choix on fait appel à ChoiceManager
        {
            dialogueChoiceManager.UpdateNode(data);
        }
        else
        {
            dialogueTextManager.UpdateNode(data);
        }
    }

    void EndDialog(VD.NodeData data)
    {
        VD.OnNodeChange -= UpdateNode;
        VD.OnEnd -= EndDialog;
        VD.EndDialogue(); //Third most important method when using VIDE     

        dialogueMenu.gameObject.SetActive(false);


        if (OnDialogueEnd != null) OnDialogueEnd.Invoke();
    }



    public void Wait(float a)
    {
        StartCoroutine(WaitCoroutine(a));

    }
    private IEnumerator WaitCoroutine(float t)
    {
        yield return new WaitForSeconds(t);
        VD.Next();
    }



    public void InterruptDialog()
    {
        VD.OnNodeChange -= UpdateNode;
        VD.OnEnd -= EndDialog;
        VD.EndDialogue();
    }

}
