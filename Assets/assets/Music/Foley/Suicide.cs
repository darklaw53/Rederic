using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suicide : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Perish());
    }

    IEnumerator Perish()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
