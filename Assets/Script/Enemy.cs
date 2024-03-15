using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    #region establishing names and stuff
    #region Serialized field stuff
    [Space(10)]
    [SerializeField] protected Collider2D collider2d;
    [SerializeField] protected EnemyHealth enemyHealth = default;
    [SerializeField] protected GameObject playerControll = default;
    [SerializeField] protected Player player;
    [SerializeField] protected Transform trPlayer;
    [SerializeField] protected Transform debugTransform;
    [SerializeField] protected float distToTarget = 0;
    [SerializeField] protected float distToTargetY = 0;
    [SerializeField] protected float distToTargetX = 0;
    [SerializeField] protected float distToPlayerX;
    [SerializeField] protected float distToPlayerY;
    [SerializeField] protected float distToPlayer;
    [SerializeField] protected bool onGround;
    [SerializeField] protected bool onWall = false;
    #endregion
    #region public shit
    [Space(10)]
    [SerializeField] protected Rigidbody2D rigidbody2d;
    public Animator Animator { get; private set; }
    public Vector3 myV3;

    [Space(10)]
    public GameObject Noticed;
    public Transform groundCheck;
    [SerializeField] protected Transform wallCheck;
    public Transform attackPoint;
    public Transform noticedSpot;
    public Transform center;
    protected Transform parent;
    public LayerMask playerLayer;
    public float moveSpeed;
    public float attackRange;
    public float agroRange;
    public float stun;
    public Transform edgeDetectorVal;
    public ParticleSystem dust;
    protected Transform edgeDetector;
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected float knockBack;
    [SerializeField] protected float knockUp;
    [SerializeField] protected float checkRadius;
    [SerializeField] protected float waitTime;
    [SerializeField] protected float hearingRange;
    [SerializeField] protected float noticeRadius;
    [SerializeField] protected float antiShlorpRadius;
    public float attackingdistdebug;
    public float MaxSpeed;
    public float Acceleration;
    public float Deceleration;
    public float runSpeed;
    public int sightRange;
    public int attackDamage;
    #endregion
    #region bools
    [Space(10)]
    protected bool facingRight = false;
    protected bool lookForGround = false;
    protected bool canPatrol = true;
    protected bool canChasePlayer = false;
    protected bool canSeePlayer = false;
    protected bool lookToGoThroughGround = false;
    protected bool givingUpOnChase = false;
    protected bool purpleTeleportGo = false;
    protected bool tpTargetGot = false;
    bool hitEdge;
    protected bool alert = false;
    bool rememberYourJob = false;
    bool canHearPlayer = false;
    protected bool haveTarget = false;
    protected bool eyesOpen = true;
    protected bool careAboutGround = true;
    #endregion
    #region floats
    protected float verticalSpeed;
    protected float horizontalSpeed;
    protected float codeedSpeed = 0;
    #endregion
    Coroutine ca;
    protected Coroutine co;
    Coroutine pa;
    protected Transform target;
    #endregion

    public int[] table;
    public int total;
    public int randomNumber;
    public List<GameObject> drop1;
    public List<GameObject> drop2;
    public List<GameObject> drop3;
    public List<List<GameObject>> dropList = new List<List<GameObject>>();
    public bool canAttack = true;
    public bool notOnLog = true;
    float remeberGravidy;
    bool halt;

    PhysicsMaterial2D m_Material;

    PhysicsMaterial2D noFric, maxFric;

    public float juggleNumber = 0;
    CameraBrain camB;

    protected virtual void Awake()
    {
        noFric = (PhysicsMaterial2D)Resources.Load("No Friction", typeof(PhysicsMaterial2D));
        maxFric = (PhysicsMaterial2D)Resources.Load("Full Friction", typeof(PhysicsMaterial2D));

        playerControll = GameObject.FindGameObjectWithTag("Player");
        player = playerControll.GetComponent<Player>();
        trPlayer = playerControll.GetComponent<Transform>();

        collider2d = GetComponent<CapsuleCollider2D>();
        camB = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraBrain>();

        enemyHealth = gameObject.GetComponent<EnemyHealth>();
        enemyHealth.agroMemory = agroRange;
        parent = transform.parent;
        if (parent.GetChild(0) != gameObject)
        {
            debugTransform = parent.GetChild(0);
        }

        remeberGravidy = rigidbody2d.gravityScale;

        dropList.Add(drop1);
        dropList.Add(drop2);
        dropList.Add(drop3);
    }

    protected Coroutine ho;

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (ho != null)
        {
            StopCoroutine(ho);
        }
        if (collision.gameObject.tag == ("Destroyer"))
        {
            transform.parent.parent = collision.transform;
            notOnLog = false;
            rigidbody2d.gravityScale = 0;
            if (!halt)
            {
                halt = true;
                rigidbody2d.velocity = new Vector2(0, 0);
            }
            transform.rotation = collision.transform.rotation;
        }
        else
        {
            transform.parent.parent = null;
            transform.rotation = Quaternion.identity;
        }
    }

    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Destroyer"))
        {
            ho = StartCoroutine(HopOff());
            rigidbody2d.gravityScale = remeberGravidy;
            if (halt)
            {
                halt = false;
            }
        }
    }

    protected IEnumerator HopOff()
    {
        yield return new WaitForSeconds(0.1f);
        notOnLog = true;
        
        this.transform.parent.parent = null;
        transform.rotation = Quaternion.identity;
    }

    protected virtual void Start()
    {
        Animator = GetComponent<Animator>();
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
    }

    public float timeRemaining = 0.5f;

    private void Update()
    {
        if (juggleNumber == 5)
        {
            camB.Chivo2Get();
        }
    }

    protected virtual void FixedUpdate()
    {
        onWall = Physics2D.OverlapCircle(wallCheck.position, checkRadius, groundLayer);

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }

        if (!notOnLog)
        {
            //rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, -10);
        }
    }

    protected bool circleRed = false;

    protected virtual void OnDrawGizmosSelected()
    {
        if (circleRed)
        {
        }
        Gizmos.DrawWireSphere(center.position, antiShlorpRadius);
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireSphere(center.position, hearingRange);
        Gizmos.DrawWireSphere(center.position, noticeRadius);
        if (debugTransform != null)
        {
            Gizmos.DrawWireSphere(debugTransform.position, 1);
            if (circleRed)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(debugTransform.position, 1);
            }
        }
    }

    protected virtual void CheckDrop()
    {
        total = 0;
        foreach(var item in table)
        {
            total += item;
        }
        randomNumber = UnityEngine.Random.Range(0, total);
        for(int i = 0; i < table.Length; i++)
        {
            if (randomNumber <= table[i])
            {
                DropLoot(dropList[i]);
                return;
            }
            else
            {
                randomNumber -= table[i];
            }
        }
    }

    void DropLoot(List<GameObject> glitteringPrizes)
    {
        foreach(var item in glitteringPrizes)
        {
            Instantiate(item,center.position,center.rotation);
        }
    }

    protected virtual void DamageEffect()
    {
        eyesOpen = false;
        if (canChasePlayer)
        {
            rememberYourJob = true;
        }
        else
        {
            rememberYourJob = false;
        }

        StopChasingPlayer();
        Animator.SetBool("damage 0", true);
        canPatrol = false;
        canChasePlayer = false;
        tpTargetGot = false;

        rigidbody2d.velocity = new Vector2(0, knockUp);

        juggleNumber = juggleNumber+1;

        if (transform.position.x < trPlayer.position.x)
        {
            rigidbody2d.velocity = new Vector2(-knockBack, rigidbody2d.velocity.y);
            transform.localScale = new Vector2(-1, 1);
            facingRight = true;
            StartCoroutine(StopEnemyAfterDamage());
            enemyHealth.SetIsDamage();
        }
        else if (transform.position.x > trPlayer.position.x)
        {
            rigidbody2d.velocity = new Vector2(knockBack, rigidbody2d.velocity.y);
            transform.localScale = new Vector2(1, 1);
            facingRight = false;
            StartCoroutine(StopEnemyAfterDamage());
            enemyHealth.SetIsDamage();
        }
    }

    protected virtual void Patrol()
    {
        if ((canHearPlayer | canSeePlayer) & !alert)
        {
            canPatrol = false;
            StopChasingPlayer();
            GameObject a = Instantiate(Noticed) as GameObject;
            a.transform.position = noticedSpot.position;
            alert = true;
            StartCoroutine(SpotPlayer());
        }

        if (canPatrol)
        {
            DetectEdgeOrWall();
            if (facingRight)
            {
                MoveRight();
            }
            else
            {
                MoveLeft();
            }

            if (hitEdge | onWall)
            {
                canPatrol = false;
                StopChasingPlayer();
                pa = StartCoroutine(PatrolStuff());
            }
        }
    }

    protected virtual void AntiPlatformShlorp()
    {
        bool wouldShlorp = Physics2D.OverlapCircle(center.position, antiShlorpRadius, groundLayer);
        if (wouldShlorp)
        {
            collider2d.enabled = false;
        }
        else
        {
            collider2d.enabled = true;
        }
    }

    public Transform eyePos;

    protected virtual void EnemyEyes()
    {
        Vector2 endPos;
        if (facingRight)
        {
            endPos = eyePos.position + Vector3.right * sightRange;
        }
        else
        {
            endPos = eyePos.position + Vector3.left * sightRange;
        }
        RaycastHit2D eyeInfo = Physics2D.Linecast(eyePos.position, endPos, 1 << LayerMask.NameToLayer("Action"));
        //canSeePlayer = false;
        if (eyeInfo.collider != null)
        {
            if (eyeInfo.collider.CompareTag("Player"))
            {
                canSeePlayer = true;
                Debug.DrawLine(eyePos.position, eyeInfo.point, Color.red);
            }
            else
            {
                canSeePlayer = false;
                Debug.DrawLine(eyePos.position, eyeInfo.point, Color.blue);
            }
        }
        else
        {
            canSeePlayer = false;
            Debug.DrawLine(eyePos.position, endPos, Color.blue);
        }
        if (distToPlayer < hearingRange & timeRemaining <= 0)
        {
            canHearPlayer = true;
        }
        else if (distToPlayer >= hearingRange)
        {
            canHearPlayer = false;
        }
    }

    protected virtual void LookForGround()
    {
        if (lookForGround)
        {
            careAboutGround = true;
            if (onGround)
            {
                juggleNumber = 0;

                lookForGround = false;
                Animator.SetBool("damage 0", false);
                Animator.enabled = true;
                lookToGoThroughGround = false;
                StopChasingPlayer();
                rigidbody2d.sharedMaterial.friction = 0;
                careAboutGround = true;

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

                Animator.enabled = true;
                if (rememberYourJob)
                {
                    canChasePlayer = true;
                }

                CheckForDead();

                if (facingRight)
                {
                    codeedSpeed = 1;
                }
                else
                {
                    codeedSpeed = -1;
                }

                StartCoroutine(SpotPlayer());
                eyesOpen = true;
                Invoke("NoMoreTarget", 1);

                purpleTeleportGo = true;
            }
        }
    }

    protected virtual void NoMoreTarget()
    {
        haveTarget = false;
    }

    bool droped;

    protected virtual void CheckIfDamage()
    {
        if (enemyHealth.GetIsDamage())
        {
            if (enemyHealth.GetCurrentHealth() <= 0 & !droped)
            {
                CheckDrop();
                droped = true;
            }
            DamageEffect();
        }
    }

    protected virtual void DistanceToPlayer()
    {
        distToPlayer = Vector3.Distance(trPlayer.position, center.position);
        distToPlayerY = (trPlayer.position.y - center.position.y);
        distToPlayerX = (trPlayer.position.x - center.position.x);
    }

    protected virtual void GroundDetector()
    {
        if (careAboutGround)
        {
            onGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        }       
    }

    protected virtual void CreateDust()
    {
        dust.Play();
    }

    protected virtual void SpeedDetector()
    {
        verticalSpeed = rigidbody2d.velocity.y;
        horizontalSpeed = rigidbody2d.velocity.x;

        Animator.SetFloat("VerticalSpeed", Mathf.Abs(verticalSpeed));
        Animator.SetFloat("HorizontalSpeed", Mathf.Abs(horizontalSpeed));
    }

    bool miniLookingForGround = false;

    protected virtual void MiniLookForGround()
    {
        if (miniLookingForGround)
        {
            if (onGround)
            {
                StopChasingPlayer();
                miniLookingForGround = false;
                Invoke("NoMoreTarget", 1);
            }
        }
    }

    protected virtual void AgroManager()
    {
        if (!canSeePlayer & !canHearPlayer & alert & !givingUpOnChase)
        {
            co = StartCoroutine(GiveUpOnChase());
            givingUpOnChase = true;
        }
        else if ((canSeePlayer | canHearPlayer) & givingUpOnChase)
        {
            StopCoroutine(co);
            givingUpOnChase = false;
        }
    }

    protected bool dead = false;

    protected virtual void CheckForDead()
    {
        if (enemyHealth.GetCurrentHealth() <= 0)
        {
            canAttack = false;
            enemyHealth.canBeHit = false;
            canPatrol = false;
            canChasePlayer = false;
            eyesOpen = false;
            StopChasingPlayer();
            Animator.SetBool("Dead", true);
            dead = true;
            StartCoroutine(DeathEnemy());
        }
    }

    protected virtual void StopChasingPlayer()
    {
        rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
    }

    protected virtual void HitPlayer()
    {
        attackingdistdebug = Vector3.Distance(player.hitPoint.position, attackPoint.position);
        if (Vector3.Distance(player.hitPoint.position, attackPoint.position) < attackRange)
        {
            DamagePlayer();
            StartCoroutine(SpotPlayer());
        }
    }

    protected virtual void DamagePlayer()
    {
        if (player.currentHealth > 0 & canAttack)
        {
            player.TakeDamage(attackDamage, myV3);
        }
    }

    protected virtual void FreezeAnim()
    {
        Animator.enabled = false;
    }

    void StopPatrol()
    {
        StopCoroutine(pa);
        patroling = false;
    }

    protected virtual void MinimalistPatrol()
    {
        if (canPatrol)
        {
            canPatrol = false;
            float patrTm = UnityEngine.Random.Range(waitTime / 2, waitTime);
            ca = StartCoroutine(MinPatrolStuff(patrTm));
        }

        if ((canHearPlayer | canSeePlayer) & !alert)
        {
            canPatrol = false;
            StopCoroutine(ca);
            StopChasingPlayer();
            GameObject a = Instantiate(Noticed) as GameObject;
            a.transform.position = noticedSpot.position;
            alert = true;
            StartCoroutine(SpotPlayer());
        }
    }

    protected virtual IEnumerator MinLookForGroundStuff()
    {
        yield return new WaitForSeconds(0.1f);
        miniLookingForGround = true;
    }

    IEnumerator MinPatrolStuff(float waitungTime)
    {
        yield return new WaitForSeconds(waitungTime);
        if (facingRight)
        {
            transform.localScale = new Vector2(1, 1);
            facingRight = false;
        }
        else
        {
            transform.localScale = new Vector2(-1, 1);
            facingRight = true;
        }
        yield return new WaitForSeconds(0.01f);
        canPatrol = true;
    }

    protected virtual IEnumerator SpotPlayer()
    {
        yield return new WaitForSeconds(0.01f);
        if (patroling)
        {
            StopPatrol();
        }
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
        yield return new WaitForSeconds(0.5f);
        if (rememberYourJob)
        {
            rememberYourJob = false;
            yield break;
        }
        canChasePlayer = true;
    }

    bool patroling;

    protected virtual IEnumerator PatrolStuff()
    {
        yield return new WaitForSeconds(0.01f);
        patroling = true;
        yield return new WaitForSeconds(waitTime);
        if (facingRight)
        {
            MoveLeft();
        }
        else
        {
            MoveRight();
        }
        hitEdge = false;
        onWall = false;
        yield return new WaitForSeconds(0.01f);
        canPatrol = true;
        patroling = false;
    }

    protected virtual IEnumerator GiveUpOnChase()
    {
        yield return new WaitForSeconds(10f);
        canPatrol = false;
        canChasePlayer = false;
        StopChasingPlayer();
        yield return new WaitForSeconds(waitTime);
        canPatrol = true;
        alert = false;
        givingUpOnChase = false;
    }

    protected virtual IEnumerator StopEnemyAfterDamage()
    {
        yield return new WaitForSeconds(0.2f);
        lookForGround = true;
    }

    protected virtual IEnumerator DeathEnemy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    //meh?
    protected virtual void MoveLeft()
    {
        rigidbody2d.velocity = new Vector2(-moveSpeed, rigidbody2d.velocity.y);
        transform.localScale = new Vector2(1, 1);
        facingRight = false;
    }

    protected virtual void MoveRight()
    {
        rigidbody2d.velocity = new Vector2(moveSpeed, rigidbody2d.velocity.y);
        transform.localScale = new Vector2(-1, 1);
        facingRight = true;
    }

    protected virtual void DetectEdgeOrWall()
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(edgeDetector.position, Vector2.down, 1f);
        if (!groundInfo.collider)
        {
            hitEdge = true;
        }
    }

    protected virtual void ChasePlayer()
    {
        if (canChasePlayer & enemyHealth.GetCurrentHealth() > 0)
        {
            if (transform.position.x < trPlayer.position.x)
            {
                if (!facingRight)
                {
                    CreateDust();
                }
                facingRight = true;
                RunRight();
            }
            else if (transform.position.x > trPlayer.position.x)
            {
                if (facingRight)
                {
                    CreateDust();
                }
                facingRight = false;
                RunLeft();
            }
            else if ((transform.position.x == trPlayer.position.x + 0.1 | transform.position.x == trPlayer.position.x - 0.1) & (horizontalSpeed <= 0.1 | player.currentHealth == 0f))
            {
                StopChasingPlayer();
            }
        }
    }

    protected virtual void RunRight()
    {
        if (horizontalSpeed == 0)
        {
            codeedSpeed = 0;
        }
        codeedSpeed += runSpeed;
        if (codeedSpeed >= MaxSpeed)
        {
            codeedSpeed = MaxSpeed;
        }
        rigidbody2d.velocity = new Vector2(codeedSpeed, rigidbody2d.velocity.y);
        transform.localScale = new Vector2(-1, 1);
        facingRight = true;
    }

    protected virtual void RunLeft()
    {
        if (horizontalSpeed == 0)
        {
            codeedSpeed = 0;
        }
        codeedSpeed -= runSpeed;
        if (codeedSpeed <= -MaxSpeed)
        {
            codeedSpeed = -MaxSpeed;
        }
        rigidbody2d.velocity = new Vector2(codeedSpeed, rigidbody2d.velocity.y);
        transform.localScale = new Vector2(1, 1);
        facingRight = false;
    }
}
