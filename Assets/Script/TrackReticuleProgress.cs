using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackReticuleProgress : MonoBehaviour
{
    //Animator anim;
    public bool boom;

    void Awake()
    {
        //anim = transform.GetComponent<Animator>();
        boom = false;
    }

    public void Explode()
    {
        boom = true;
    }
}
