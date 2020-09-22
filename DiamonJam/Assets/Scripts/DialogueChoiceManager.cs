using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VIDE_Data;

public class DialogueChoiceManager : MonoBehaviour
{

    [SerializeField]
    Transform buttonListParent;
    [SerializeField]
    DialogueChoiceButton buttonPrefab;

    [Header("Parameter")]
    [SerializeField]
    float choiceInterval = 0.1f;

    [Header("Feedback")]
    [SerializeField]
    MoveCharacterPivot moveCharacter;
    [SerializeField]
    Transform transformStart;
    [SerializeField]
    Transform transformEnd;
    [SerializeField]
    Animator[] feedbacks;

    List<DialogueChoiceButton> buttonList = new List<DialogueChoiceButton>();

    //[Header("Feedback")]
    //[SerializeField]



    public void UpdateNode(VD.NodeData choice)
    {
        // Feedback
        moveCharacter.MoveToNewParent(transformStart);
        for (int i = 0; i < feedbacks.Length; i++)
        {
            feedbacks[i].SetBool("Appear", true);
        }
        // ============


        for (int i = 0; i < choice.comments.Length; i++)
        {
            if (buttonList.Count <= i)
                buttonList.Add(Instantiate(buttonPrefab, buttonListParent));
            buttonList[i].gameObject.SetActive(true);
            buttonList[i].DrawButton(choice.comments[i], choiceInterval * i);
            buttonList[i].AssignID(i);
        }
        for (int i = choice.comments.Length; i < buttonList.Count; i++)
        {
            buttonList[i].gameObject.SetActive(false);
        }
    }


    private void EndNode()
    {
        moveCharacter.MoveToNewParent(transformEnd);
        for (int i = 0; i < feedbacks.Length; i++)
        {
            feedbacks[i].SetBool("Appear", false);
        }
        for (int i = 0; i < buttonList.Count; i++)
        {
            buttonList[i].gameObject.SetActive(false);
        }
    }

    public void SelectChoiceAndGoToNext(int playerChoice)
    {
        EndNode();
        VD.nodeData.commentIndex = playerChoice; //Setting this when on a player node will decide which node we go next
        VD.Next();
    }

}
