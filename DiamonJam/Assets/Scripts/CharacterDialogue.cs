using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CharacterDialogue : MonoBehaviour
{
    [SerializeField]
    SO_CharacterData characterData;

    SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /*public void SetCharacterData(SO_CharacterData data, int spriteIndex = 0)
    {
        characterData = data;
        spriteRenderer.sprite = characterData.CharacterSprites[spriteIndex];
    }*/

    /*public void SetSpriteIndex(int spriteIndex)
    {
        spriteRenderer.sprite = characterData.CharacterSprites[spriteIndex];
    }*/

}
