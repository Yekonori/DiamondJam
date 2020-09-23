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

    bool active = false;
    int index = 0;

    public void AssignID(int id)
    {
        index = id;
    }

    public void DrawButton(string choiceText, float delayAppear = 0f)
    {
        active = true;
        textChoice.text = choiceText;
        StartCoroutine(ButtonAppearCoroutine(delayAppear));
    }

    private IEnumerator ButtonAppearCoroutine(float delayAppear)
    {
        yield return new WaitForSeconds(delayAppear);
        animatorChoice.ResetTrigger("Disappear");
        animatorChoice.SetTrigger("Appear");
    }


    public void HideButton()
    {
        active = false;
        animatorChoice.SetTrigger("Disappear");
    }





    public void OnPointerDown(PointerEventData eventData)
    {
        if (active == true)
        {
            animatorChoice.SetTrigger("PointerDown");
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (active == true)
        {
            animatorChoice.SetTrigger("PointerUp");
            OnClickEvent.Invoke(index);
        }
    }


}
