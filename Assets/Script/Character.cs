using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{

    public Animator RealAnimator { get; private set; }
    public Rigidbody2D rigidbody2d;
    public bool facingRight;
    protected float verticalSpeed;
    protected float velocity;
    protected float horizontalInput;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //RealAnimator.enabled = true;
        RealAnimator = GetComponent<Animator>();
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
    }
}
