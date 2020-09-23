using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskSelectionButton : MonoBehaviour
{
    [SerializeField]
    Image imageMask;
    int index;

    [SerializeField]
    UnityEventInt OnButtonPress;

    public void DrawButton(SO_CharacterData character, int i)
    {
        index = i;
        imageMask.sprite = character.CharacterMask;
    }

    public void ButtonCall()
    {
        OnButtonPress.Invoke(index);
    }
}
