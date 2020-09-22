using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpeak : MonoBehaviour
{

    [SerializeField]
    Animator animator;


    public void Speak(bool b)
    {
        animator.SetBool("Speak", b);
    }
}
