using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lever : MonoBehaviour
{
    public priceTrigger pTrigger;
    Animator anim;
    public bool onned;

    private void Awake()
    {
        anim = transform.GetComponent<Animator>();
    }

    private void Update()
    {
        if (pTrigger.pulled)
        {
            anim.SetBool("On", true);
            onned = true;
        }
        else
        {
            anim.SetBool("On", false);
            onned = false;
        }
    }
}
