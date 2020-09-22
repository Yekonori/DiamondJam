using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VIDE_Data; //à utiliser pour que le node action détecte le script



public class Flash : MonoBehaviour
{
    [SerializeField]
    float time = 0.5f;
    [SerializeField]
    Image flashImage;

    private IEnumerator coroutine;

    public void StartFlash()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = FlashCoroutine();
        StartCoroutine(coroutine);
    }

    private IEnumerator FlashCoroutine()
    {
        Debug.Log("Hey");
        Color finalColor = new Color(1, 1, 1, 0);
        float t = 0f;
        while(t < 1f)
        {
            t += Time.deltaTime / time; // division donc pas optimisé
            flashImage.color = Color.Lerp(Color.white, finalColor, t);
            yield return null;
        }
    }
}
