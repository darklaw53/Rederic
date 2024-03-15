using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noticed : MonoBehaviour
{
    private void Update()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = transform.position + Vector3.up * 1f;
        transform.position = Vector2.Lerp(startPos, endPos, 1 * Time.deltaTime);
        Invoke("KYS", 0.5f);
    }
    void KYS()
    {
        Destroy(gameObject);
    }
}
