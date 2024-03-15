using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pigman : Enemy
{/*
    [SerializeField]
    Transform castPoint;
    [SerializeField]
    Transform headSpot;
    [SerializeField]
    float agroRange;
    [SerializeField]
    float sightRange;
    [SerializeField]
    float attackRange;
    [SerializeField]
    float moveSpeed;
    public int maxHealth;
    [SerializeField]
    float knockUp;
    [SerializeField]
    float knockBack;
    [SerializeField]
    LayerMask whatIsGround;
    [SerializeField]
    GameObject headRef;
    [SerializeField]
    float jumpHeight;
    [SerializeField]
    float thrustDist;
    [SerializeField]
    float thrustTime;
    [SerializeField]
    float coolDown;
    public float checkRadius;
    public float stun;
    int currentHealth;
    public Transform hoofPos;
    private bool isAgro = false;
    private bool isSearching;
    bool OnGround = true;
    bool stunned = false;
    bool StartWalking;
    bool Panicking = false;
    bool Headless;
    bool ChaseePlayer;
    bool StopChasePlayer;
    bool atPlayer = false;
    bool attacking;
    public Transform attackPos;
    bool isJumping;
    bool smash;
    bool canAttack = true;

    // Start is called before the first frame update
    protected override void Start()
    {
        Headless = false;
        base.Start();
        //facingRight = false;
        currentHealth = maxHealth;
    }

    void FixedUpdate()
    {
        verticalSpeed = rigidbody2d.velocity.y;

        if (!stunned)
        {
            OnGround = Physics2D.OverlapCircle(hoofPos.position, checkRadius, whatIsGround);
        }
        if (!Panicking)
        {
            //distance to player
            float distToPlayerX = Vector2.Distance(transform.position, player.position);
            if (distToPlayerX < agroRange)
            {
                isAgro = true;
                ChasePlayer();
            }
            else
            {
                if (isAgro)
                {
                    if (!isSearching)
                    {
                        isSearching = true;
                        Invoke("StopChasingPlayer", 5);
                    }
                }
            }
        }
        else
        {
            StopChasingPlayer();
        }
        if (StopChasePlayer)
        {
            rigidbody2d.velocity = Vector2.zero;
            if (currentHealth > 0)
            {
                StartWalking = false;
                RealAnimator.SetBool("Walking", false);
                isAgro = false;
                isSearching = false;
            }
        }
        else if (ChaseePlayer)
        {
            if (OnGround && !attacking)
            {                
                StartWalking = true;
                if (transform.position.x < //player.position.x)
                {
                    rigidbody2d.velocity = new Vector2(moveSpeed, rigidbody2d.velocity.y);
                    transform.localScale = new Vector2(-1, 1);
                    //facingRight = true;
                    atPlayer = false;
                }
                if (transform.position.x > //player.position.x)
                {
                    rigidbody2d.velocity = new Vector2(-moveSpeed, rigidbody2d.velocity.y);
                    transform.localScale = new Vector2(1, 1);
                    //facingRight = false;
                    atPlayer = false;
                }
            }
            if (StartWalking)
            {
                RealAnimator.SetBool("Walking", true);
            }
        }
    }
    void ChasePlayer()
    {
        ChaseePlayer = true;
        StopChasePlayer = false;
    }
    void StopChasingPlayer()
    {
        ChaseePlayer = false;
        StopChasePlayer = true;
    }

    // Update is called once per frame
    void Update()
    {
        RealAnimator.SetFloat("VerticalSpeed", verticalSpeed);

        if (verticalSpeed < 13 && isJumping == true)
        {
            RealAnimator.enabled = true;
        }

        if (isJumping == true)
        {
            rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
        }

        if (CanAttackPlayer(attackRange) && !attacking && canAttack == true)
        {
            attacking = true;
            RealAnimator.SetBool("Walking", false);
            JumpUpToSmash();
        }
        if (CanSeePlayer(sightRange))
        {
            isAgro = true;
            ChasePlayer();
        }
        else
        {
            if(isAgro)
            {
                if(!isSearching)
                {
                    isSearching = true;
                    Invoke("StopChasingPlayer", 5);
                }
            }
        }

        if(isAgro)
        {
            ChasePlayer();
        }      
    }

    void BigStun()
    {
        stunned = false;
    }

    bool CanSeePlayer(float distance)
    {
            bool val = false;
            var castDist = distance;

            if (facingRight)
            {
                castDist = -distance;
            }

            Vector2 endPos = castPoint.position + Vector3.left * castDist;
            RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Action"));
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Player") && atPlayer == false)
                {
                    val = true;
                }
                else
                {
                    val = false;
                }
                Debug.DrawLine(castPoint.position, hit.point, Color.blue);
            }
            else
            {
                Debug.DrawLine(castPoint.position, endPos, Color.red);
            }
            return val;        
    }

    bool CanAttackPlayer(float distance)
    {
        bool val = false;
        var castDist = distance;

        if (facingRight)
        {
            castDist = -distance;
        }

        Vector2 endPos = castPoint.position + Vector3.left * castDist;
        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Action"));
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player") && atPlayer == false)
            {
                val = true;
            }
            else
            {
                val = false;
            }
            Debug.DrawLine(castPoint.position, hit.point, Color.yellow);
        }
        else
        {
            Debug.DrawLine(castPoint.position, endPos, Color.green);
        }
        return val;
    }

    void StopPanicking()
    {
        Panicking = false;
        RealAnimator.SetBool("HeadlessIdle", true);
        RealAnimator.Play("HeadlessIdle");
        OnGround = true;
    }

    void BetterNow()
    {
        RealAnimator.SetBool("Pain", false);
        Invoke("StopPanicking", 3);
    }

    public void TakeDamage(int damage)
    {
        RealAnimator.SetBool("Pain", true);
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            if (Headless == false)
            {
                RealAnimator.Play("Just the ouch");
                Panicking = true;
                RealAnimator.SetBool("Headless", true);
                OnGround = false;
                Headless = true;
                GameObject head = (GameObject)Instantiate(headRef);
                head.transform.position = headSpot.transform.position;
            }
        }
        OnGround = false;
        if (transform.position.x < player.position.x)
        {
            rigidbody2d.velocity = new Vector2(-knockBack, knockUp);
        }
        else if (transform.position.x > player.position.x)
        {
            rigidbody2d.velocity = new Vector2(knockBack, knockUp);
        }
        stunned = true;
        Invoke("BigStun", stun);
    }

    void JumpUpToSmash()
    {
        isJumping = true;
        rigidbody2d.velocity = new Vector2(0, jumpHeight);
        RealAnimator.SetBool("isJumping", true);
        //smash = true;
        //RealAnimator.SetBool("Smashing", true);
    }

    void Hold()
    {
        RealAnimator.enabled = false;
    }

    void Strike()
    {
        isJumping = false;
        RealAnimator.enabled = false;
        if (facingRight == true)
        {
            rigidbody2d.velocity = new Vector2(thrustDist, rigidbody2d.velocity.y);
        }
        if (facingRight == false)
        {
            rigidbody2d.velocity = new Vector2(-thrustDist, rigidbody2d.velocity.y);
        }
        Invoke("Go", thrustTime);
    }
    
    void Go()
    {
        RealAnimator.enabled = true;
        attacking = false;
        canAttack = false;
        OnGround = true;
        Invoke("CanAttack", coolDown);
    }

    void CanAttack()
    {
        canAttack = true;
    }

    void Die()
    {
        Debug.Log("Enemy died");
        // Die aniation
        // Disable enemy
    }*/
}
