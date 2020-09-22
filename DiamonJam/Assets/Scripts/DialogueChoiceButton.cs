using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

[System.Serializable]
public class UnityEventInt : UnityEvent<int>
{

}

public class DialogueChoiceButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    TextMeshProUGUI textChoice;
    [SerializeField]
    Animator animatorChoice;

    [SerializeField]
    UnityEventInt OnClickEvent; 

    int index = 0;

    public void DrawButton(string choiceText, float delayAppear = 0f)
    {
        textChoice.text = choiceText;
        StartCoroutine(ButtonAppearCoroutine(delayAppear));
    }
    private IEnumerator ButtonAppearCoroutine(float delayAppear)
    {
        yield return new WaitForSeconds(delayAppear);
        animatorChoice.SetBool("Appear", true);
    }




    public void AssignID(int id)
    {
        index = id;
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Mouse Down: " + eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnClickEvent.Invoke(index);
    }


}
