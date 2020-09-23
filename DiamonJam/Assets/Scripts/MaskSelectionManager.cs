using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MaskSelectionManager : MonoBehaviour
{
    [SerializeField]
    GameObject menuMaskSelection;


    [SerializeField]
    Transform buttonListParent;
    [SerializeField]
    MaskSelectionButton buttonPrefab;

    SO_CharacterData maskSelected;
    public SO_CharacterData MaskSelected
    {
        get { return maskSelected; }
    }

    List<SO_CharacterData> masks = new List<SO_CharacterData>();
    List<MaskSelectionButton> buttonList = new List<MaskSelectionButton>();



    [Header("Events")]
    [SerializeField]
    UnityEvent eventStartSelection;
    [SerializeField]
    UnityEvent eventEndSelection;

    public void StartSelection(List<SO_CharacterData> characters)
    {
        menuMaskSelection.SetActive(true);
        eventStartSelection.Invoke();
        masks = characters;
        for (int i = 0; i < characters.Count; i++)
        {
            if (buttonList.Count <= i)
                buttonList.Add(Instantiate(buttonPrefab, buttonListParent));
            buttonList[i].gameObject.SetActive(true);
            buttonList[i].DrawButton(characters[i], i);
        }
        for (int i = characters.Count; i < buttonList.Count; i++)
        {
            buttonList[i].gameObject.SetActive(false);
        }
    }

    // Call by MaskSelectionButton
    public void SelectMask(int index)
    {
        maskSelected = masks[index];
    }

    // Call by MaskSelectionButton ou GameManager
    public void EndSelection()
    {
        eventEndSelection.Invoke();
    }
}
