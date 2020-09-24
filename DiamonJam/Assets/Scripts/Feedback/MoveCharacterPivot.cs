using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacterPivot : MonoBehaviour
{

    [SerializeField]
    float speedCoroutine;

    private IEnumerator moveCharacterCoroutine;

    public void MoveToNewParent(Transform newPivot)
    {
        this.transform.SetParent(newPivot);
        if (this.gameObject.activeInHierarchy == false)
            return;
        if (moveCharacterCoroutine != null)
            StopCoroutine(moveCharacterCoroutine);
        moveCharacterCoroutine = MoveCharacterCoroutine(speedCoroutine);
        StartCoroutine(moveCharacterCoroutine);
    }

    private IEnumerator MoveCharacterCoroutine(float speed)
    {
        Vector3 originPosition = Vector3.zero;
        Quaternion zero = Quaternion.Euler(0, 0, 0);
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / speed;
            this.transform.localRotation = Quaternion.Slerp(this.transform.localRotation, zero, t);
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, originPosition, t);
            yield return null;
        }
    }



    public void MoveToNewParent(Transform newPivot, float speed)
    {
        this.transform.SetParent(newPivot);
        if (this.gameObject.activeInHierarchy == false)
            return;
        if (moveCharacterCoroutine != null)
            StopCoroutine(moveCharacterCoroutine);
        moveCharacterCoroutine = MoveCharacterCoroutine(speed);
        StartCoroutine(moveCharacterCoroutine);
    }

}
