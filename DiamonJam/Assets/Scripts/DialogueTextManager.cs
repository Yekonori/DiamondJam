using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VIDE_Data;
using TMPro;


public class DialogueTextManager : MonoBehaviour
{
    [Header("Parameter")]
    [SerializeField]
    float intervalLetter = 0.01f;
    [SerializeField]
    float intervalLong = 0.4f;

    [SerializeField]
    TextMeshProUGUI textDialog;

    [Header("Feedback")]
    [SerializeField]
    GameObject nextButton;
    [SerializeField]
    Animator dialogBoxFeedback;
    [SerializeField]
    CharacterSpeak characterSpeak;

    string actualText;
    int actualLenght = 0;
    float actualTime = 0f;

    Touch touch;



    public void UpdateNode(VD.NodeData text)
    {
        StartCoroutine(UpdateTextCoroutine(text));
    }

    private IEnumerator UpdateTextCoroutine(VD.NodeData text)
    {
        // Initialization ==========================================================
        dialogBoxFeedback.SetTrigger("Feedback");
        nextButton.gameObject.SetActive(false);
        actualText = text.comments[0];
        textDialog.text = text.comments[0];
        textDialog.maxVisibleCharacters = 0;

        yield return null; // On attend une frame pour que text mesh pro update bien comme il faut


        actualLenght = textDialog.textInfo.characterCount;
        textDialog.maxVisibleCharacters += 1;
        while (textDialog.maxVisibleCharacters < actualLenght)
        {
            // Set time
            actualTime = intervalLetter;
            if (actualText[textDialog.maxVisibleCharacters - 1] == ',' && actualText[textDialog.maxVisibleCharacters] == ' ' ||
                actualText[textDialog.maxVisibleCharacters - 1] == '.' && actualText[textDialog.maxVisibleCharacters] == ' ' ||
                actualText[textDialog.maxVisibleCharacters - 1] == '?' && actualText[textDialog.maxVisibleCharacters] == ' ' ||
                actualText[textDialog.maxVisibleCharacters - 1] == '!' && actualText[textDialog.maxVisibleCharacters] == ' ')
            {
                characterSpeak.Speak(false);
                actualTime = intervalLong;
            }
            else
            {
                characterSpeak.Speak(true);
            }


            while (actualTime >= 0)
            {
                actualTime -= Time.deltaTime;
                if (UpdateInput() == true)
                {
                    break;
                }
                yield return null;
            }

            if (UpdateInput() == true) // C'est pas ouf
            {
                yield return null;
                break;
            }
            // Print new Letter
            textDialog.maxVisibleCharacters += 1;
        }
        EndTextUpdate();
        while (UpdateInput() == false)
        {
            yield return null;
        }
        VD.Next();
    }



    private void EndTextUpdate()
    {
        textDialog.maxVisibleCharacters = actualLenght;
        nextButton.gameObject.SetActive(true);
        characterSpeak.Speak(false);
    }



    public bool UpdateInput()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                return true;
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            return true;
        }
        return false;
    }

    private void EndNode()
    {
        
    }
}
