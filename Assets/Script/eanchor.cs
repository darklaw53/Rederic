using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eanchor : MonoBehaviour
{
    public GameObject eButton;
    public float fadeTime;
    public Animator meAnim;
    public bool pressedE;
    priceTrigger pTrigger;

    private void Update()
    {
        eButton.transform.position = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        pTrigger = collision.GetComponent<priceTrigger>();
        if (pTrigger != null)
        {
            pTrigger.inRange = true;
            pTrigger.TurnOn();
            if (!pressedE)
            {
                meAnim.SetBool("in", true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        pTrigger = collision.GetComponent<priceTrigger>();
        if (pTrigger != null)
        {
            pTrigger.inRange = false;
            pTrigger.TurnOff();
            if (meAnim.GetBool("out") == false)
            {
                meAnim.SetBool("out", true);
            }
        }
    }
}
