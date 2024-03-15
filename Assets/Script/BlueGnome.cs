using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueGnome : Enemy
{
    public float jumpHeight;
    Collider2D colider;
    public Collider2D platformColider;
    public Collider2D floorColider;
    public bool touchdown = false;
    bool canGoThroughGround;
    [SerializeField] LayerMask whatIsPlatform;
    [SerializeField] LayerMask whatIsSolidWall;
    public PhysicMaterial noFriction;
    public PhysicMaterial fullFriction;

    public AudioSource meAudio;

    protected override void Awake()
    {
        base.Awake();
        colider = transform.GetComponent<CapsuleCollider2D>();
        GameObject platform = GameObject.Find("CanJumpThrough");
        GameObject floor = GameObject.Find("Tilemap");
        platformColider = platform.GetComponent<Collider2D>();
        floorColider = floor.GetComponent<Collider2D>();
    }

    protected override void Start()
    {
        meAudio = GetComponent<AudioSource>();

        base.Start();
        rigidbody2d.sharedMaterial.friction = 0;
        edgeDetector = null;
        target = default;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (dead)
        {
            Animator.Play("BlueDead");
        }
        myV3 = transform.position;
        if (eyesOpen)
        {
            EnemyEyes();
        }
        SpeedDetector();
        GroundDetector();
        DistanceToPlayer();
        LookForGround();
        CheckIfDamage();
        HitPlayer();
        LeapAfterPlayer();
        MinimalistPatrol();
        DontSlipPastGround();
        AgroManager();
        WallEject();
        MiniLookForGround();
        //AntiPlatformShlorp();
    }

    void WallEject()
    {
        bool onRealWall = Physics2D.OverlapCircle(wallCheck.position, checkRadius, whatIsSolidWall);
        if (onRealWall & onGround)
        {
            if (facingRight)
            {
                rigidbody2d.velocity = new Vector2(-3, 3);
                StartCoroutine(MinLookForGroundStuff());
            }
            else
            {
                rigidbody2d.velocity = new Vector2(3, 3);
                StartCoroutine(MinLookForGroundStuff());
            }
        }
    }

    void DontSlipPastGround()
    {
        if (onGround)
        {
            colider.sharedMaterial.bounciness = 0;
        }

        if (distToTarget < 1 | canGoThroughGround)
        {
            Physics2D.IgnoreCollision(platformColider, colider, false);
            Physics2D.IgnoreCollision(floorColider, colider, false);
            careAboutGround = true;
        }

        if (distToTarget < 1)
        {
            circleRed = true;
        }
        else
        {
            circleRed = false;
        }
    }

    public Transform myTrsf;
    bool growling;

    void LeapAfterPlayer()
    {
        canGoThroughGround = Physics2D.OverlapCircle(center.position, noticeRadius, whatIsSolidWall);
        distToTarget = Vector3.Distance(debugTransform.position, myTrsf.position);
        distToTargetY = (debugTransform.position.y - myTrsf.position.y);
        distToTargetX = (debugTransform.position.x - myTrsf.position.x);

        if (canChasePlayer & enemyHealth.GetCurrentHealth() > 0)
        {
            if (!haveTarget & onGround)
            {
                colider.sharedMaterial.bounciness = 1;
                //line up the current player position with the temp position
                debugTransform.position = trPlayer.position;
                distToTarget = Vector3.Distance(debugTransform.position, myTrsf.position);
                distToTargetY = (debugTransform.position.y - myTrsf.position.y);
                distToTargetX = (debugTransform.position.x - myTrsf.position.x);

                haveTarget = true;

                if (transform.position.x < trPlayer.position.x)
                {
                    transform.localScale = new Vector2(-1, 1);
                    facingRight = true;
                }
                else if (transform.position.x > trPlayer.position.x)
                {
                    transform.localScale = new Vector2(1, 1);
                    facingRight = false;
                }

                if (distToTarget > 8f)
                {
                    haveTarget = false;
                    return;
                }

                if (!growling)
                {
                    growling = true;
                    meAudio.enabled = false;
                    // meAudio.pitch = ((Random.Range(100, 301)) / 100);
                    meAudio.enabled = true;
                    StartCoroutine(Growl());
                }

                rigidbody2d.AddForce(new Vector2(distToTargetX, jumpHeight + (distToTargetY)), ForceMode2D.Impulse);
                CreateDust();
                StartCoroutine(OffTheGround());
                StartCoroutine(MinLookForGroundStuff());
            }
        }
    }

    IEnumerator Growl()
    {
        yield return new WaitForSeconds(2f);
        growling = false;
    }

    IEnumerator OffTheGround()
    {
        yield return new WaitForSeconds(0.1f);
        Physics2D.IgnoreCollision(platformColider, colider, true);
        Physics2D.IgnoreCollision(floorColider, colider, true);
        careAboutGround = false;
    }
}
