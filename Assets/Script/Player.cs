using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{
    [SerializeField] private Transform feetPos;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private LayerMask jumpPlatformLayer;
    [SerializeField] private float slopeCheckDistance;
    [SerializeField] protected float runSpeed;

    public PlatformEffector2D platformness;
    public Collider2D collider2d;
    public Transform healthBar;
    public Transform hitPoint;
    public Transform center;
    public Transform treasureBag;
    public Transform arrowLaunchPoint1;
    public Transform arrowLaunchPoint2;
    public TreasureBag trBag;
    public ParticleSystem dust;
    public ArrowUI arrowUi;
    public GameObject arrow;

    public GameObject textBox;
    public TextRoller txRoller;

    Vector3 healthScale;

    public float checkRadius;
    public float jumpTime;
    public float jumpVelocity;
    public float knockBack;
    public float knockUp;
    public float iframeTime;
    public float antiShlorpRadius;
    public bool canMove = true;
    public bool hasPurchasedArrows;
    public bool hasPurchasedHealth;

    public int arrowNumber;
    public int maxHealth = 100;
    public int currentHealth;

    bool jumpKeyWasPressed;
    bool isGrounded;
    bool isJumping;
    bool grace;
    public bool invincible = false;
    public bool lookForGround;
    bool forceFlip;
    bool firstTap;
    public bool notOnLog = true;
    float remeberGravidy;

    float jumpTimeCounter;

    bool halt;

    GameObject lastSafeTransform;
    GameObject secondSafeTransform;
    GameObject thirdSafeTransform;

    PhysicsMaterial2D noFric, maxFric;
        public Quaternion rot;
    int currentLevelNUmb;

    NextLevel nexLev;

    public CameraBrain camB;

    Coroutine ho;
    Coroutine kys;
    public Transform log;

    Vector2 lastSafeGround;
    Vector2 secondToLastSafeGround;
    Vector2 thirdToLastSafeGround;
    public Vector2 astualSafeGround;
    public LayerMask enemyLayer;
    public LayerMask cumLayer;

    public Transform slopeCheckPos;
    Vector2 vec2;
    public Vector2 slopNormalPerpendicular;
    float slopeDownAngle;
    float slopeDownAngleOld;
    //bool isOnSloap;

    List<GameObject> arrowsInLeve;
    List<GameObject> arrowsEndLeve;

    public GameObject stepSound;
    public GameObject counterstepSound;
    public GameObject jumpSound;
    public GameObject landingSound;
    public GameObject arrowShotSound;
    public GameObject arrowPickUpSound;
    public GameObject treasurePickUpSound;
    public GameObject painSound;

    public AudioSource soudioSause;

    void Awake()
    {
        arrowsInLeve = new List<GameObject>(GameObject.FindGameObjectsWithTag("Arrow"));

        camB = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraBrain>();

        invincible = false;

        nexLev = GameObject.FindGameObjectWithTag("Level").GetComponent<NextLevel>();

        lastSafeTransform = new GameObject("lastSafeTransform");
        secondSafeTransform = new GameObject("secondSafeTransform");
        thirdSafeTransform = new GameObject("thirdSafeTransform");

        MakeSafeSpotObjects();

        InvokeRepeating("UpdateSafeGroundTrackers", 0f, 3f);

        rot = transform.rotation;
        remeberGravidy = rigidbody2d.gravityScale;

        noFric = (PhysicsMaterial2D)Resources.Load("No Friction", typeof(PhysicsMaterial2D));
        maxFric = (PhysicsMaterial2D)Resources.Load("Full Friction", typeof(PhysicsMaterial2D));

        base.Start();
        healthScale = healthBar.localScale;

        RealAnimator.SetBool("isJumping", false);
        facingRight = true;

        trBag = treasureBag.GetComponent<TreasureBag>();
    }

    protected override void Start()
    {
        currentLevelNUmb = SceneManager.GetActiveScene().buildIndex;
        if (currentLevelNUmb == 1 | currentLevelNUmb == 3 | currentLevelNUmb == 5 | currentLevelNUmb == 7 | currentLevelNUmb == 8 | currentLevelNUmb == 9)
        {
            if (currentLevelNUmb == 1)
            {
                HoldmyShit.hasDoneAKill = false;
                HoldmyShit.hasMadePurchase = false;
                //if (HoldmyShit.lvl1R == false)
                //{
                HoldmyShit.lvl1R = true;
                HoldmyShit.lvl2R = false;
                HoldmyShit.lvl3R = false;
                HoldmyShit.lvl4R = false;
                HoldmyShit.lvl5R = false;
                //}
                currentHealth = maxHealth;
                arrowNumber = 0;
                HoldmyShit.hasMadePurchase = false;
                trBag.goldScore = 0;
            }
            if (currentLevelNUmb == 3)
            {
                if (HoldmyShit.lvl2R == false & HoldmyShit.lvl1R == true)
                {
                    //HoldmyShit.lvl1R = false;
                    HoldmyShit.lvl2R = true;
                    HoldmyShit.lvl3R = false;
                    HoldmyShit.lvl4R = false;
                    HoldmyShit.lvl5R = false;
                    HoldmyShit.keepHealth2 = HoldmyShit.keepHealth;
                    HoldmyShit.arrows2 = HoldmyShit.arrows;
                    HoldmyShit.keepScore2 = HoldmyShit.keepScore;
                }
                currentHealth = HoldmyShit.keepHealth2;
                arrowNumber = HoldmyShit.arrows2;
                trBag.goldScore = HoldmyShit.keepScore2;
            }
            if (currentLevelNUmb == 5)
            {
                if (HoldmyShit.lvl3R == false & HoldmyShit.lvl2R == true)
                {
                    //HoldmyShit.lvl2R = false;
                    HoldmyShit.lvl3R = true;
                    HoldmyShit.lvl4R = false;
                    HoldmyShit.lvl5R = false;
                    HoldmyShit.keepHealth3 = HoldmyShit.keepHealth;
                    HoldmyShit.arrows3 = HoldmyShit.arrows;
                    HoldmyShit.keepScore3 = HoldmyShit.keepScore;
                }
                currentHealth = HoldmyShit.keepHealth3;
                arrowNumber = HoldmyShit.arrows3;
                trBag.goldScore = HoldmyShit.keepScore3;
            }
            if (currentLevelNUmb == 7)
            {
                if (HoldmyShit.lvl4R == false & HoldmyShit.lvl3R == true)
                {
                    //HoldmyShit.lvl2R = false;
                    //HoldmyShit.lvl3R = false;
                    HoldmyShit.lvl4R = true;
                    HoldmyShit.lvl5R = false;
                    HoldmyShit.keepHealth4 = HoldmyShit.keepHealth;
                    HoldmyShit.arrows4 = HoldmyShit.arrows;
                    HoldmyShit.keepScore4 = HoldmyShit.keepScore;
                }
                currentHealth = HoldmyShit.keepHealth4;
                arrowNumber = HoldmyShit.arrows4;
                trBag.goldScore = HoldmyShit.keepScore4;
            }

            if (currentLevelNUmb == 8)
            {
                if (!HoldmyShit.hasDoneAKill & HoldmyShit.lvl4R)
                {
                    camB.Chivo6Get();
                }
                if (HoldmyShit.hasMadePurchase == false & HoldmyShit.lvl4R == true)
                {
                    camB.Chivo4Get();
                }
            }

            if (currentLevelNUmb == 9)
            {
                if (HoldmyShit.lvl5R == false & HoldmyShit.lvl4R == true)
                {
                    //HoldmyShit.lvl2R = false;
                    //HoldmyShit.lvl3R = false;
                    //HoldmyShit.lvl4R = false;
                    HoldmyShit.lvl5R = true;
                    HoldmyShit.keepHealth5 = HoldmyShit.keepHealth;
                    HoldmyShit.arrows5 = HoldmyShit.arrows;
                    HoldmyShit.keepScore5 = HoldmyShit.keepScore;
                }
                currentHealth = HoldmyShit.keepHealth5;
                arrowNumber = HoldmyShit.arrows5;
                trBag.goldScore = HoldmyShit.keepScore5;
            }
        }
        else
        {
            currentHealth = HoldmyShit.keepHealth;
            arrowNumber = HoldmyShit.arrows;
            trBag.goldScore = HoldmyShit.keepScore;
        }
        healthScale.x = currentHealth;
        StartCoroutine(LateStart());
    }

    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.01f);
        float t = arrowNumber;
        while (t != 0)
        {
            //Debug.Log("arowgottr");
            arrowUi.GotArrow();
            t--;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ho != null)
        {
            StopCoroutine(ho);
        }
        if (collision.gameObject.tag == ("Destroyer"))
        {
            transform.parent = collision.transform;

            notOnLog = false;
            rigidbody2d.gravityScale = 0;
            if (!halt)
            {
                halt = true;
                rigidbody2d.velocity = new Vector2(0, 0);
                //kys = StartCoroutine(Stoooooop());
            }
            transform.rotation = collision.transform.rotation;
        }
        else
        {
            transform.parent = null;
            notOnLog = true;
            transform.rotation = Quaternion.identity;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Destroyer"))
        {
            //log = collision.transform;
            if (kys != null)
            {
                StopCoroutine(kys);
            }
            ho = StartCoroutine(HopOff());
            rigidbody2d.gravityScale = remeberGravidy;
            if (halt)
            {
                halt = false;
            }
        }
    }

    public void StepSound()
    {
        Instantiate(stepSound);
    }

    public void CounterStepSound()
    {
        Instantiate(counterstepSound);
    }

    internal void TakeDamage(object attackDamage, object myV3)
    {
        //throw new NotImplementedException();
    }

    IEnumerator HopOff()
    {
        yield return new WaitForSeconds(0.1f);
        notOnLog = true;

        this.transform.parent = null;
        transform.rotation = Quaternion.identity;
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(center.position, antiShlorpRadius);
    }

    void MakeSafeSpotObjects()
    {
        Instantiate(lastSafeTransform, transform.position, Quaternion.identity);
        Instantiate(secondSafeTransform, transform.position, Quaternion.identity);
        Instantiate(thirdSafeTransform, transform.position, Quaternion.identity);
    }

    void Update()
    {
        if (trBag.goldScore >= 2500)
        {
            camB.Chivo3Get();
        }

        //healthScale.x = currentHealth;
        if (txRoller != null)
        {
            if (txRoller.texting == false)
            {
                canMove = true;
            }
        }

        Collider2D[] colliders1 = Physics2D.OverlapCircleAll(transform.position, 1f, enemyLayer);
        Collider2D[] cumliders1 = Physics2D.OverlapCircleAll(transform.position, 1f, cumLayer);
        if (isGrounded)
        {
            if (colliders1.Length == 0 & cumliders1.Length == 0)
            {
                if (!notOnLog)
                {
                    lastSafeTransform.transform.parent = log;
                }
                else
                {
                    lastSafeTransform.transform.parent = null;
                }

                lastSafeTransform.transform.position = transform.position;
            }
        }

        astualSafeGround = lastSafeTransform.transform.position;

        /*if (lastSafeTransform.transform.parent == null)
        {
            lastSafeTransform.transform.position = lastSafeGround;
        }
        if (secondSafeTransform.transform.parent == null)
        {
            secondSafeTransform.transform.position = secondToLastSafeGround;
        }
        if (thirdSafeTransform.transform.parent == null)
        {
            thirdSafeTransform.transform.position = thirdToLastSafeGround;
        }

        if (doTheUpdate)
        {
            thirdToLastSafeGround = secondToLastSafeGround;
            secondToLastSafeGround = lastSafeGround;
            if (secondSafeTransform.transform.parent != null)
            {
                thirdSafeTransform.transform.parent = log;
            }
            else
            {
                thirdSafeTransform.transform.parent = null;
            }
            if (lastSafeTransform.transform.parent != null)
            {
                secondSafeTransform.transform.parent = log;
            }
            else
            {
                secondSafeTransform.transform.parent = null;
            }
        }
        if (colliders1.Length > 0 & cumliders1.Length > 0)
        {
            Collider2D[] colliders2 = Physics2D.OverlapCircleAll(secondToLastSafeGround, 1f, enemyLayer);
            Collider2D[] cumliders2 = Physics2D.OverlapCircleAll(secondToLastSafeGround, 1f, cumLayer);
            if (colliders2.Length > 0 & cumliders2.Length > 0)
            {
                astualSafeGround = secondSafeTransform.transform.position;
            }
            else
            {
                astualSafeGround = thirdSafeTransform.transform.position;
            }
        }
        else
        {
            astualSafeGround = lastSafeTransform.transform.position;
        }*/

        SlopeCheck();

        HoldmyShit.keepHealth = currentHealth;
        HoldmyShit.arrows = arrowNumber;

        if (arrowNumber > 4)
        {
            arrowNumber = 4;
        }
        else if (arrowNumber < 0)
        {
            arrowNumber = 0;
        }

        healthBar.localScale = healthScale;

        LookForGround();

        if (canMove)
        {
            SetAnimationValues();
            GetInput();
        }

        if (arrowUi.gotArrow & (Input.GetKeyDown(KeyCode.LeftShift) | Input.GetKeyDown(KeyCode.X)) & !dead & !RealAnimator.GetCurrentAnimatorStateInfo(0).IsName("Knight Pain"))
        {
            canMove = false;
            rigidbody2d.velocity = new Vector2(0, 0);
            RealAnimator.SetFloat("Speed", 0);
            RealAnimator.SetFloat("VerticalSpeed", verticalSpeed);
            if (isGrounded)
            {
                RealAnimator.Play("idol");
            }
            RealAnimator.SetBool("isAttacking", false);
            RealAnimator.SetBool("isJumping", false);
            RealAnimator.SetBool("shooting", true);
            arrowNumber--;
            arrowUi.Fire();
        }
    }

    bool doTheUpdate;

    void UpdateSafeGroundTrackers()
    {
        doTheUpdate = true;
    }

    public void HaltMidAir()
    {
        rigidbody2d.isKinematic = true;
    }

    public void NotHMA()
    {
        rigidbody2d.isKinematic = false;
    }

    public void Fire()
    {
        Instantiate(arrowShotSound);

        if (isGrounded)
        {
            canMove = true;
        }
        else
        {
            RealAnimator.enabled = false;
            if (!facingRight)
            {
                rigidbody2d.velocity = new Vector2(knockBack, knockUp);
            }
            else
            {
                rigidbody2d.velocity = new Vector2(-knockBack, knockUp);
            }
            StartCoroutine(StopPlayerAfterDamage());
        }
        RealAnimator.SetBool("shooting", false);
        //instantiate arrow
        if (isGrounded)
        {
            GameObject.Instantiate(arrow, arrowLaunchPoint1.position, arrowLaunchPoint1.rotation);
        }
        else
        {
            GameObject.Instantiate(arrow, arrowLaunchPoint2.position, arrowLaunchPoint2.rotation);           
        }
    }

    private void SlopeCheck()
    {
        if (slopeCheckPos != null)
        {
            vec2 = slopeCheckPos.position;
        }
        RaycastHit2D hit = Physics2D.Raycast(vec2, Vector2.down, slopeCheckDistance, whatIsGround);

        if (hit)
        {
            slopNormalPerpendicular = Vector2.Perpendicular(hit.normal).normalized;
            if (facingRight)
            {
                slopNormalPerpendicular *= -1;
            }
            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (slopeDownAngle != slopeDownAngleOld)
            {
                //isOnSloap = true;
            }

            slopeDownAngleOld = slopeDownAngle;

            Debug.DrawRay(hit.point, slopNormalPerpendicular, Color.red);
            Debug.DrawRay(hit.point, hit.normal, Color.green);
        }
        //transform.up = (hit.point - slopNormalPerpendicular).normalized;
        if (log != null)
        {
            if (((log.rotation.z > 0.1 & !facingRight) | (log.rotation.z < -0.1 & facingRight)) & (Input.GetKey(KeyCode.D) | Input.GetKey(KeyCode.A)) & !notOnLog)
            {
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, -10);
                //Debug.Log("killme");
            }
        }   
    }

    void FixedUpdate()
    {
        //SlopeCheck();
        /*(if (!notOnLog)
        {
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, -10);
        }
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (!notOnLog)
            {
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, -10);
            }
        }*/

        verticalSpeed = rigidbody2d.velocity.y;
        
        Flip(horizontalInput);

        if (jumpKeyWasPressed)
        {
            Instantiate(jumpSound);
            Instantiate(stepSound);

            rigidbody2d.velocity = new Vector3(horizontalInput, jumpVelocity, 0);
            jumpKeyWasPressed = false;
            isGrounded = false;
            isJumping = true;
            jumpTimeCounter = jumpTime;
            CreateDust();
        }

        if ((Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.UpArrow)) & isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rigidbody2d.velocity = new Vector3(horizontalInput, jumpVelocity, 0);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        AntiPlatformShlorp();
    }

    public GameObject caChing;
    public GameObject denied;

    public void PlusArrow(int price)
    {
        if (!arrowUi.fullUp & trBag.goldScore >= price)
        {
            Instantiate(caChing);

            float t = 4;
            while (t != 0)
            {
                arrowNumber++;
                arrowUi.GotArrow();
                t--;
            }
            trBag.goldScore -= price;
        }
        else
        {
            Instantiate(denied);
        }
    }

    public void PlusHealth(int price)
    {
        if (maxHealth != currentHealth & trBag.goldScore >= price)
        {
            Instantiate(caChing);

            currentHealth = maxHealth;
            trBag.goldScore -= price;
            healthScale.x = 100;
        }
        else
        {
            Instantiate(denied);
        }
    }

    List<GameObject> arrowDemiPocket = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LogStartLine"))
        {
            log.GetComponent<DoomLog>().doPush = true;
        }
        if (collision.CompareTag("Treasure"))
        {
            Instantiate(treasurePickUpSound);
            var valueGetter = collision.GetComponent<Treasure>();
            trBag.CashLoot(valueGetter.value, valueGetter.me);
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Arrow") & !arrowUi.fullUp)
        {
            Instantiate(arrowPickUpSound);

            //Debug.Log("ping");
            arrowNumber++;
            arrowUi.GotArrow();
            //arrowDemiPocket.Add(collision.gameObject);
            /*
            int x = SceneManager.GetActiveScene().buildIndex;
            switch (x)
            {
                case 1:
                    //arrowDemiPocket.Add(collision.gameObject);
                    ArrowCollected.arrowColectedLvl1.Add(collision.gameObject.name);
                    ArrowCollected.arrowColectedLvl1 = ArrowCollected.arrowColectedLvl1.Distinct().ToList();
                    if (arrowsInLeve.Count == ArrowCollected.arrowColectedLvl1.Count)
                    {
                        HoldmyShit.gotAllArrows1 = true;
                    }
                    break;
                case 3:
                    ArrowCollected.arrowColectedLvl2.Add(collision.gameObject.name);
                    ArrowCollected.arrowColectedLvl2 = ArrowCollected.arrowColectedLvl2.Distinct().ToList();
                    if (arrowsInLeve.Count == ArrowCollected.arrowColectedLvl2.Count)
                    {
                        HoldmyShit.gotAllArrows2 = true;
                    }
                    break;
                case 5:
                    ArrowCollected.arrowColectedLvl3.Add(collision.gameObject.name);
                    ArrowCollected.arrowColectedLvl3 = ArrowCollected.arrowColectedLvl3.Distinct().ToList();
                    if (arrowsInLeve.Count == ArrowCollected.arrowColectedLvl3.Count)
                    {
                        HoldmyShit.gotAllArrows3 = true;
                    }
                    break;
                case 7:
                    ArrowCollected.arrowColectedLvl4.Add(collision.gameObject.name);
                    ArrowCollected.arrowColectedLvl4 = ArrowCollected.arrowColectedLvl4.Distinct().ToList();
                    if (arrowsInLeve.Count == ArrowCollected.arrowColectedLvl4.Count)
                    {
                        HoldmyShit.gotAllArrows4 = true;
                    }
                    break;
            }
            if (HoldmyShit.gotAllArrows1 & HoldmyShit.gotAllArrows2 & HoldmyShit.gotAllArrows3 & HoldmyShit.gotAllArrows4)
            {
                camB.Chivo5Get();
            } */

            Destroy(collision.gameObject.transform.parent.gameObject);
        }
        if (collision.CompareTag("Exit"))
        {
            collision.gameObject.GetComponent<ExitPoint>().ExitProtocol();
            SaveManager.instance.Save();
            canMove = false;
            collider2d.enabled = false;
            rigidbody2d.gravityScale = 0f;
            if (facingRight)
            {
                rigidbody2d.velocity = new Vector3(1 * runSpeed, rigidbody2d.velocity.y, 0);
            }
            else
            {
                rigidbody2d.velocity = new Vector3(-1 * runSpeed, rigidbody2d.velocity.y, 0);
            }
        }
        if (collision.CompareTag("DiologueTrigger") & txRoller != null)
        {
            txRoller.StartText();
            canMove = false;
            rigidbody2d.velocity = new Vector2(0, 0);
            RealAnimator.SetFloat("Speed", 0);
        }
        if (collision.CompareTag("BoulderTrigger"))
        {
            collision.gameObject.GetComponent<MortaurTrigger>().activated = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("BoulderTrigger"))
        {
            collision.gameObject.GetComponent<MortaurTrigger>().activated = false;
        }
    }

    bool isShlorping;

    void AntiPlatformShlorp()
    {
        if (platformness != null)
        {
            bool wouldShlorp = Physics2D.OverlapCircle(center.position, antiShlorpRadius, platformLayer);
            if (wouldShlorp)
            {
                platformness.useColliderMask = false;
                Physics2D.IgnoreLayerCollision(8, 11, true);
                isShlorping = true;
            }
            else if (!wouldShlorp & isShlorping)
            {
                platformness.useColliderMask = true;
                Physics2D.IgnoreLayerCollision(8, 11, false);
                isShlorping = false;
            }
        }
    }

    public void TakeDamage(int damage, Vector3 attacker)
    {
        if (!invincible)
        {
            Instantiate(painSound);

            canMove = false;
            invincible = true;
            RealAnimator.SetBool("inPain", true);
            RealAnimator.Play("Knight Pain");
            if (currentHealth > damage)
            {
                currentHealth -= damage;
                healthScale.x -= damage;
            }
            else
            {
                currentHealth = 0;
                healthScale.x = 0;
            }
            if (transform.position.x > attacker.x)
            {
                if (facingRight)
                {
                    Vector3 theScale = transform.localScale;
                    theScale.x *= -1;
                    forceFlip = true;
                    transform.localScale = theScale;
                }
                rigidbody2d.velocity = new Vector2(knockBack, knockUp);
                StartCoroutine(StopPlayerAfterDamage());
            }
            else if (transform.position.x < attacker.x)
            {
                if (!facingRight)
                {
                    Vector3 theScale = transform.localScale;
                    theScale.x *= -1;
                    forceFlip = true;
                    transform.localScale = theScale;
                }
                rigidbody2d.velocity = new Vector2(-knockBack, knockUp);
                StartCoroutine(StopPlayerAfterDamage());
            }
        }
    }

    public void FreezeAnim()
    {
        RealAnimator.enabled = false;
    }

    void SetAnimationValues()
    {
        RealAnimator.SetFloat("Speed", Mathf.Abs(horizontalInput));
        RealAnimator.SetFloat("VerticalSpeed", verticalSpeed);
        if ((Mathf.Abs(horizontalInput) != 0) & isGrounded)
        {
            soudioSause.enabled = true;
        }
        else
        {
            soudioSause.enabled = false;
        }
    }

    void CreateDust()
    {
        dust.Play();
    }

    bool jumping;

    void LookForGround()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if (isGrounded == true)
        {
            RealAnimator.SetBool("isJumping", false);
            if (jumping)
            {
                jumping = false;
                Instantiate(landingSound);
                Instantiate(stepSound);
            }
        }
        else if (isGrounded == false)
        {
            jumping = true;
            RealAnimator.SetBool("isJumping", true);
        }
        if (lookForGround)
        {
            if (isGrounded)
            {
                //Instantiate(landingSound);
                //Instantiate(stepSound);

                lookForGround = false;
                rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
                RealAnimator.enabled = true;
                if (currentHealth > 0)
                {
                    RealAnimator.SetBool("isAttacking", false);
                    RealAnimator.SetBool("inPain", false);
                    if (forceFlip)
                    {
                        Vector3 theScale = transform.localScale;
                        theScale.x *= -1;
                        transform.localScale = theScale;
                        forceFlip = false;
                    }
                    canMove = true;
                }
                else
                {
                    Die(false);
                }
            }
        }
    }

    public bool dead;

    public void Die(bool poison)
    {
        RealAnimator.Play("Knight ded");
        invincible = true;
        dead = true;

        if (!poison)
        {
            nexLev.nextLevel = currentLevelNUmb;
            nexLev.FadeOut(true);
        }
    }

    IEnumerator StopPlayerAfterDamage()
    {
        yield return new WaitForSeconds(0.2f);
        lookForGround = true;
        yield return new WaitForSeconds(iframeTime);
        invincible = false;
    }

    void GetInput()
    {
        rigidbody2d.velocity = new Vector3(horizontalInput, rigidbody2d.velocity.y, 0);
        horizontalInput = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetKeyUp(KeyCode.W) | Input.GetKeyUp(KeyCode.UpArrow))
        {
            isJumping = false;
        }
        if ((Input.GetKeyDown(KeyCode.W) | Input.GetKeyDown(KeyCode.UpArrow)) & isGrounded & (!Input.GetKey(KeyCode.S) & !Input.GetKey(KeyCode.DownArrow)))
        {
            jumpKeyWasPressed = true;
        }
        if (!Input.GetKey(KeyCode.S) & grace)
        {
            Meterialize();
        }

        if (Input.GetKeyUp(KeyCode.S) | Input.GetKeyUp(KeyCode.DownArrow))
        {
            firstTap = true;
        }

        if (firstTap)
        {
            tl = StartCoroutine(TooLate());
            if (Input.GetKeyDown(KeyCode.S) | Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (Input.GetKey(KeyCode.S) | Input.GetKey(KeyCode.DownArrow))
                {
                    grace = false;
                    platformness.useColliderMask = false;
                    Physics2D.IgnoreLayerCollision(8, 11, true);
                    Invoke("GracePeriod", 0.3f);
                    StopCoroutine(tl);
                    firstTap = false;
                }
            }
        }
    }

    Coroutine tl;

    IEnumerator TooLate()
    {
        yield return new WaitForSeconds(0.1f);
        firstTap = false;
    }

    void GracePeriod()
    {
        grace = true;
    }

    void Meterialize()
    {
        Physics2D.IgnoreLayerCollision(8, 11, false);
        platformness.useColliderMask = true;
    }

    protected void Flip(float horizontalInput)
    {
        if (horizontalInput > 0 & !facingRight | horizontalInput < 0 & facingRight)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            CreateDust();
        }
    }

    public void SwordFlourish()
    {
        RealAnimator.SetBool("Flourish", true);
    }
}