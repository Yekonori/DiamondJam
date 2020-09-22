using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data; // à utiliser pour que le node action détecte le script

public class Shake : MonoBehaviour
{

    [SerializeField]
    int power;

    [SerializeField]
    int time = 40;


    [SerializeField]
    RectTransform[] rectTransform;

    List<Vector3> starts = new List<Vector3>();

    [ContextMenu("Shake")]
    public void ShakeEffect()
    {
        StartCoroutine(ShakeCoroutineRectTransform(power, time));
    }

    private IEnumerator ShakeCoroutineRectTransform(float power, int time)
    {
        starts.Clear();
        for (int i = 0; i < rectTransform.Length; i++)
        {
            starts.Add(rectTransform[i].anchoredPosition);
        }
        float speed = power / time;
        float t = time / 60f;
        while (t > 0f)
        {
            t -= Time.deltaTime;
            power -= speed;
            for (int i = 0; i < rectTransform.Length; i++)
            {
                rectTransform[i].anchoredPosition = new Vector3(starts[i].x + Random.Range(-power, power), starts[i].y + Random.Range(-power, power), starts[i].z + Random.Range(-power, power));
            }

            yield return null;
        }
        for (int i = 0; i < rectTransform.Length; i++)
        {
            rectTransform[i].anchoredPosition = starts[i];
        }
    }

}
