using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacterPivot : MonoBehaviour
{

    [SerializeField]
    float speedCoroutine;
    [SerializeField]
    Transform newPivot;

    // Start is called before the first frame update
    void Start()
    {
        MoveToNewParent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void MoveToNewParent()
    {
        this.transform.SetParent(newPivot);
        StartCoroutine(MoveCharacterCoroutine());
    }

    private IEnumerator MoveCharacterCoroutine()
    {
        Vector3 originPosition = Vector3.zero;
        Quaternion zero = Quaternion.Euler(0, 0, 0);
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / speedCoroutine;
            this.transform.localRotation = Quaternion.Slerp(this.transform.localRotation, zero, t);
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, originPosition, t);
            yield return null;
        }
    }
}
