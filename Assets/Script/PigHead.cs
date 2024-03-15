using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*public class PigHead : Enemy
/*
{
    [SerializeField]
    float knockUp;
    [SerializeField]
    float knockBack;
    [SerializeField]
    float lookRange;
    [SerializeField]
    private LayerMask whatIsGround;
    public Transform stumpPos;
    public float checkRadius;
    public LayerMask bodyLayer;
    Transform bodyPosition;
    bool isGrounded = false;
    bool upRight = false;
    bool impact = false;
    public Transform orientation;
    bool setUp;
    Quaternion truePos;

    // Start is called before the first frame update
    protected override void Start()
    {
        truePos = transform.rotation;
        GetBody();
        GetKnocked();
        base.Start();
        SetUp();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (setUp)
        {
            if (isGrounded == true)
            {
                Invoke("UpRight", 3);
                setUp = false;
            }
        }

        if (upRight)
        {
            transform.rotation = truePos;
        }
        if (upRight)
        {
            isGrounded = Physics2D.OverlapCircle(stumpPos.position, checkRadius, whatIsGround);
        }
        else
        {
            isGrounded = impact;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            impact = true;
        }
    }
    void SetUp()
    {
        setUp = true;
    }
    void UpRight()
    {
        upRight = true;
        transform.rotation = truePos;
    }
    void GetBody()
    {
        Collider2D[] Bodys = Physics2D.OverlapCircleAll(transform.position, lookRange, bodyLayer);
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector2 currentPos = transform.position;
        foreach (Collider2D t in Bodys)
        {
            float dist = Vector2.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t.transform;
                minDist = dist;
            }
        }
        bodyPosition = tMin;
    }
    void GetKnocked()
    {
        if (transform.position.x < bodyPosition.position.x)
        {
            rigidbody2d.velocity = new Vector2(knockBack, knockUp);
        }
        else if (transform.position.x > bodyPosition.position.x)
        {
            rigidbody2d.velocity = new Vector2(-knockBack, knockUp);
        }
    }
}*/
