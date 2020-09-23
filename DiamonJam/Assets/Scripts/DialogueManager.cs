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
    DialogueChoiceManager dialogueChoiceManager;
    [SerializeField]
    DialogueTextManager dialogueTextManager;

    // Start is called before the first frame update
    void Start()
    {
        // vIDE.assignedDialogue =
        //Subscribe to some events and Begin the Dialogue

       // VD.OnNodeChange += UpdateNode;
        //VD.OnEnd += EndDialog;
        //VD.BeginDialogue(GetComponent<VIDE_Assign>()); //This is the first most important method when using VIDE
    }


    public void StartDialogue(string dialogue)
    {
        vIDE.assignedDialogue = dialogue;

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
        Debug.Log("Allo");
        VD.OnNodeChange -= UpdateNode;
        VD.OnEnd -= EndDialog;
        VD.EndDialogue(); //Third most important method when using VIDE     

        // Clean des trucs
        //textDialogName.text = "";
        //textDialog.text = "";
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
}
