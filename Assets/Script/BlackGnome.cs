using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BlackGnome : MonoBehaviour
{
    [SerializeField] protected EnemyHealth enemyHealth = default;

    public Quaternion degrees;
    public float startAngle, endAngle;
    //247, 180
    //180, 113
    public int bulletsAmount;
    public LayerMask Platform;
    public LayerMask whatIsSolidWall;
    public bool facingRight;
    bool gone;

    public float knockUp, knockBack;

    public Rigidbody2D rigidbod2D;

    public Transform center;
    public Transform side;
    public Transform knifePointR;
    public Transform knifePointL;

    public Transform LaserHit;

    public GameObject Bullet;
    public GameObject Smoke;
    public GameObject Glave;
    public GameObject transformDebug;

    public float projectileSpeed;
    public float jumpHeight;
    float distToTargetX;
    float distToTargetY;

    Vector3 targetPos;

    public Animator anim;

    public LayerMask whatIsGround;
    public Transform groundCheck, wallCheck;
    public bool onGround;
    public float checkRadius;
    public float jumpNumber;
    float jumpFoth;
    float arenaWidth;
    bool inAir;
    bool readyGlaveThrow;
    bool lookingToLand;
    private bool caughtGlave;
    bool startFrontFlip;
    public bool onRealWall;

    GameObject playerControll = default;
    public GameObject arenaLimitR, arenaLimitL;
    Player player;
    Transform trPlayer;

    GameObject glave;

    RaycastHit2D hit;
    RaycastHit2D dong;

    Vector2 highPoint2;
    private bool hitWall;
    private bool perched;
    private bool waitingForNextAttack;
    private bool waitingAftPain;
    private bool threwGlave;
    private bool canBeHit = true;

    bool start;

    public TilemapCollider2D platformColider;
    private bool hop;

    public Transform one, two, tree, four, five, six;
    public bool _one, _two, _tree, _four, _five, _six;
    private bool inPain;
    private bool pong;
    private bool _1;
    private bool _2;
    private bool _3;
    private bool dead;

    public CameraBrain camB;
    public ScreenShakeController screenShake, selfShake;
    public Transform deadPo;
    private bool startShake;

    public float attackRange;
    public float attackDamage;
    Vector3 myV3;

    Vector3 healthScale;
    public Transform healthBar;
    public int currentHealth;

    public bool rage;

    public GameObject hpBar;
    Vector3 hpBarPos;

    Vector3 topPos;
    Vector3 botPos;

    public Animator levelScreen;
    GameObject levelChanger;
    NextLevel nexLev;

    public PhysicsMaterial2D rough;
    public PhysicsMaterial2D slip;

    private void Awake()
    {
        enemyHealth = gameObject.GetComponent<EnemyHealth>();
        levelChanger = GameObject.FindGameObjectWithTag("Level");
        levelScreen = levelChanger.GetComponent<Animator>();
        nexLev = levelChanger.GetComponent<NextLevel>();
    }

    private void Start()
    {
        playerControll = GameObject.FindGameObjectWithTag("Player");
        player = playerControll.GetComponent<Player>();
        trPlayer = playerControll.GetComponent<Transform>();
        arenaWidth = arenaLimitR.transform.position.x - arenaLimitL.transform.position.x;
        //waitingForNextAttack = true;
        healthScale = healthBar.localScale;
        healthScale.x = enemyHealth.GetCurrentHealth();
        StartCoroutine(FightStart());
    }

    private void Update()
    {
        if (startFrontFlip)
        {
            rigidbod2D.sharedMaterial = rough;
        }
        else
        {
            rigidbod2D.sharedMaterial = slip;
        }

        if (enemyHealth.GetCurrentHealth() < 4)
        {
            rage = true;
        }

        healthScale.x = enemyHealth.GetCurrentHealth();
        healthBar.localScale = healthScale;

        myV3 = transform.position;
        transform.rotation = new Quaternion(0, 0, 0, 0);

        if (waitingForNextAttack)
        {
            waitingForNextAttack = false;
            caughtGlave = false;
            StartCoroutine(AttackTrack());
            /*if (perched)
            {
                StartCoroutine(AttackTrack());
                waitingForNextAttack = false;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    _FlipThrough();
                    waitingForNextAttack = false;
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    _GlaveThrowAttk();
                    waitingForNextAttack = false;
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    StartCoroutine(_KnifeThrow());
                    waitingForNextAttack = false;
                }
            }*/
        }

        if (!onGround)
        {
            inAir = true;
        }
        else
        {
            if (inAir)
            {
                inAir = false;
                if (!startFrontFlip)
                {
                    Landed();
                }
            }
        }

        onGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (center.position.x < side.position.x)
        {
            facingRight = true;
        }
        else
        {
            facingRight = false;
        }

        if (Vector3.Distance(player.hitPoint.position, transform.position) < attackRange)
        {
            player.TakeDamage(attackDamage, myV3);
        }

        //topPos = new Vector3(hpBar.transform.position.x, -4.4f, 16f);
        //botPos = new Vector3(hpBar.transform.position.x, -5.91f, 16f);


    }

    IEnumerator _a;

    private void FixedUpdate()
    {
        if (startFrontFlip & onGround & hop)
        {
            hop = false; //def triggered
            _a = FrontFip();
            StartCoroutine(_a);
        }

        if (!startFrontFlip)
        {
            _one = false;
            _two = false;
            _tree = false;
            _four = false;
            _five = false;
            _six = false;
        }

        if (enemyHealth.GetIsDamage())
        {
            if (_a != null)
            {
                StopCoroutine(_a);
            }
            DamageEffect();
        }

        if (readyGlaveThrow)
        {
            if (center.position.y - trPlayer.position.y >= 0.1)
            {
                readyGlaveThrow = false;
                StartCoroutine(ThrowGlave());
            }
        }

        onRealWall = Physics2D.OverlapCircle(wallCheck.position, checkRadius, whatIsSolidWall);

        if (onRealWall)
        {
            rigidbod2D.velocity = new Vector2(0f, rigidbod2D.velocity.y);
        }
    }

    IEnumerator FrontFip()
    {
        yield return new WaitForSeconds(0.05f);
        FrontFlip();
    }

    IEnumerator FightStart()
    {
        player.canMove = false;
        camB.camFollow = false;
        camB.aimPos = transform.position;
        yield return new WaitForSeconds(0.3f);
        Vector3 botPos = hpBar.transform.position;
        Vector3 topPos = new Vector3(hpBar.transform.position.x, hpBar.transform.position.y + 1.6f, hpBar.transform.position.z);
        //hpBarPos = hpBar.transform.position;
        hpBar.transform.position = botPos;
        var i = 0.0f;
        var rate = 1.0f / 3;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            hpBar.transform.position = Vector3.Lerp(botPos, topPos, i);
            yield return null;
        }

        screenShake.StartShake(1f, 0.02f);
        yield return new WaitForSeconds(2f);
        StartCoroutine(_KnifeThrow());
        yield return new WaitForSeconds(0.3f);
        camB.camFollow = true;
        player.canMove = true;
    }

    void Landed()
    {
        if (startFrontFlip)
        {
            hop = true;
        }
        if (enemyHealth.GetCurrentHealth() <= 0)
        {
            CheckForDead();
        }
        if (!dead)
        {
            //Debug.Log("land");
            rigidbod2D.velocity = new Vector2(0f, 0f);
            platformColider.enabled = true;
            StartAnim();
            BackToIdle();

            if (inPain)
            {
                enemyHealth.SetIsDamage();
                inPain = false;
                StartCoroutine(_KnifeThrow());
            }

            if (caughtGlave)
            {
                rigidbod2D.constraints = RigidbodyConstraints2D.None;
                transform.rotation = new Quaternion(0, 0, 0, 0);
                rigidbod2D.constraints = RigidbodyConstraints2D.FreezeRotation;
                caughtGlave = false;
                BackToIdle();
                waitingForNextAttack = true;
            }

            if (readyGlaveThrow)
            {
                readyGlaveThrow = false;
                BackToIdle();
                waitingForNextAttack = true;
            }
        }
    }

    public void StopAnim()
    {
        anim.speed = 0;
        onGround = false;
    }

    public void StartAnim()
    {
        anim.speed = 1;
    }

    public void BackToIdle()
    {
        anim.SetTrigger("Idle");
    }

    IEnumerator ThrowGlave()
    {
        yield return new WaitForSeconds(0);
        anim.speed = 1;
        rigidbod2D.constraints = RigidbodyConstraints2D.FreezePosition;
        StartAnim();
        anim.SetTrigger("Glavetoss");

        if (rage)
        {
            yield return new WaitForSeconds(0.1f);
            platformColider.enabled = false;
            GameObject glame = Instantiate(Glave, center.position, transform.rotation);
            if (facingRight)
            {
                glame.GetComponent<Glave>().facingRight = true;
            }
            else
            {
                glame.GetComponent<Glave>().facingRight = false;
            }
            StartCoroutine(SmokeBomb());

            if (facingRight)
            {
                hit = Physics2D.Raycast(transform.position, transform.right, 100000, 1 << LayerMask.NameToLayer("WallLimit"));
            }
            else
            {
                hit = Physics2D.Raycast(transform.position, transform.right * -1, 100000, 1 << LayerMask.NameToLayer("WallLimit"));
            }

            if (hit)
            {
                if (facingRight)
                {
                    transform.position = new Vector2(knifePointR.position.x, hit.point.y);
                }
                else
                {
                    transform.position = new Vector2(knifePointL.position.x, hit.point.y);
                }
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
                StartCoroutine(SmokeBomb());
            }

            for (int i = 0; i < 5; i++)
            {
                transform.position = new Vector2(transform.position.x, Random.Range(0f, 6f));

                anim.speed = 1;
                rigidbod2D.constraints = RigidbodyConstraints2D.FreezePosition;
                StartAnim();
                anim.SetTrigger("Glavetoss");
                yield return new WaitForSeconds(0.1f);
                GameObject glabe = Instantiate(Glave, center.position, transform.rotation);
                if (facingRight)
                {
                    glabe.GetComponent<Glave>().facingRight = true;
                }
                else
                {
                    glabe.GetComponent<Glave>().facingRight = false;
                }
                StartCoroutine(SmokeBomb());

                if (facingRight)
                {
                    hit = Physics2D.Raycast(transform.position, transform.right, 100000, 1 << LayerMask.NameToLayer("WallLimit"));
                }
                else
                {
                    hit = Physics2D.Raycast(transform.position, transform.right * -1, 100000, 1 << LayerMask.NameToLayer("WallLimit"));
                }

                if (hit)
                {
                    if (facingRight)
                    {
                        transform.position = new Vector2(knifePointR.position.x, hit.point.y);
                    }
                    else
                    {
                        transform.position = new Vector2(knifePointL.position.x, hit.point.y);
                    }
                    Vector3 theScale = transform.localScale;
                    theScale.x *= -1;
                    transform.localScale = theScale;
                    StartCoroutine(SmokeBomb());
                }

                if (i == 5)
                {
                    threwGlave = true;
                }
            }
        }
        else
        {
            yield return new WaitForSeconds(0.1f);
            GameObject glame = Instantiate(Glave, center.position, transform.rotation);
            threwGlave = true;
            if (facingRight)
            {
                glame.GetComponent<Glave>().facingRight = true;
            }
            else
            {
                glame.GetComponent<Glave>().facingRight = false;
            }
            StartCoroutine(SmokeBomb());
            yield return new WaitForSeconds(0.5f);
            if (facingRight)
            {
                hit = Physics2D.Raycast(transform.position, transform.right, 100000, 1 << LayerMask.NameToLayer("WallLimit"));
            }
            else
            {
                hit = Physics2D.Raycast(transform.position, transform.right * -1, 100000, 1 << LayerMask.NameToLayer("WallLimit"));
            }

            if (hit)
            {
                if (facingRight)
                {
                    transform.position = new Vector2(knifePointR.position.x, hit.point.y);
                }
                else
                {
                    transform.position = new Vector2(knifePointL.position.x, hit.point.y);
                }
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
                //Debug.Log("killme");
                StartCoroutine(SmokeBomb());
                //pong = false;
            }
        }
    }

    public void CatchGlave()
    {
        caughtGlave = true;
        //StopAllCoroutines();
        threwGlave = false;
        rigidbod2D.constraints = RigidbodyConstraints2D.None;
        transform.rotation = new Quaternion(0, 0, 0, 0);
        rigidbod2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        anim.SetTrigger("GlaveCatch");
        StartAnim();

        //vv bug fixing vv
        rigidbod2D.isKinematic = true;
        rigidbod2D.isKinematic = false;
    }

    IEnumerator SmokeBomb()
    {
        yield return new WaitForSeconds(0);
        GameObject smok = Instantiate(Smoke, new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z), transform.rotation);
        yield return new WaitForSeconds(0.5f);
        gone = true;
        yield return new WaitForSeconds(0.5f);
        Destroy(smok);
    }

    IEnumerator _KnifeThrow()
    {
        yield return new WaitForSeconds(0);
        platformColider.enabled = true;
        anim.SetTrigger("Jutzu");
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(SmokeBomb());
        yield return new WaitForSeconds(0.5f);
        enemyHealth.canBeHit = true;
        if (Random.Range(0, 2) == 0)
        {
            transform.position = knifePointR.position;
            startAngle = 114;
            endAngle = 180;
            if (facingRight)
            {
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
            }
        }
        else
        {
            transform.position = knifePointL.position;
            startAngle = 180;
            endAngle = 246;
            if (!facingRight)
            {
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
            }
        }
        StartCoroutine(SmokeBomb());

        anim.SetTrigger("Knifetoss");
        StartAnim();

        if (rage)
        {
            for (int i = 0; i < bulletsAmount * 2; i++)
            {
                yield return new WaitForSeconds(0.2f);
                Vector3 direction = (player.center.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, direction);
                //lookRotation = Quaternion.Euler(transform.position + direction);
                GameObject dart = Instantiate(Bullet, center.position, lookRotation);
                dart.GetComponent<BoomKnife>().boomTime = 0.5f;
                StartAnim();
                anim.SetTrigger("Knifetoss");
            }
        }
        else
        {
            float angleStep = (endAngle - startAngle) / bulletsAmount;
            float angle = startAngle;
            for (int i = 0; 1 < bulletsAmount + 1; i++)
            {
                Instantiate(Bullet, center.position, Quaternion.Euler(new Vector3(0, 0, angle)));

                angle += angleStep;
                if (angle > endAngle)
                {
                    break;
                }
            }
        }

        perched = true;
        waitingForNextAttack = true;
    }

    void _GlaveThrowAttk()
    {
        rigidbod2D.AddForce(new Vector2(0, jumpHeight + 1), ForceMode2D.Impulse);
        anim.SetTrigger("Backflip");
        readyGlaveThrow = transform;
    }

    void _FlipThrough()
    {
        hop = true; //not this
        startFrontFlip = true;
    }

    void FrontFlip()
    {
        platformColider.enabled = false;

        if (facingRight)
        {
            if (_two)
            {
                if (_tree)
                {
                    if (_four)
                    {
                        if (_five)
                        {
                            if (_six)
                            {
                                startFrontFlip = false;
                                Vector3 theScale = transform.localScale;
                                theScale.x *= -1;
                                transform.localScale = theScale;
                                BackToIdle();
                                hop = false;
                                StartCoroutine(RecoverFromFlip());
                            }
                            else
                            {
                                _six = true;
                                targetPos = six.position;
                            }
                        }
                        else
                        {
                            _five = true;
                            targetPos = five.position;
                        }
                    }
                    else
                    {
                        _four = true;
                        targetPos = four.position;
                    }
                }
                else
                {
                    _tree = true;
                    targetPos = tree.position;
                }
            }
            else
            {
                _two = true;
                targetPos = two.position;
            }
        }
        else
        {
            if (_five)
            {
                if (_four)
                {
                    if (_tree)
                    {
                        if (_two)
                        {
                            if (_one)
                            {
                                startFrontFlip = false;
                                Vector3 theScale = transform.localScale;
                                theScale.x *= -1;
                                transform.localScale = theScale;
                                BackToIdle();
                                hop = false;
                                StartCoroutine(RecoverFromFlip());
                            }
                            else
                            {
                                _one = true;
                                targetPos = one.position;
                            }
                        }
                        else
                        {
                            _two = true;
                            targetPos = two.position;
                        }
                    }
                    else
                    {
                        _tree = true;
                        targetPos = tree.position;
                    }
                }
                else
                {
                    _four = true;
                    targetPos = four.position;
                }
            }
            else
            {
                _five = true;
                targetPos = five.position;
            }
        }
        if (startFrontFlip)
        {
            anim.SetTrigger("Frontflip");
            transformDebug.transform.position = targetPos;
            distToTargetY = (targetPos.y - transform.position.y);
            distToTargetX = (targetPos.x - transform.position.x);
            rigidbod2D.AddForce(new Vector2(distToTargetX, jumpHeight + (distToTargetY)), ForceMode2D.Impulse);
        }
    }

    public void JumpDart()
    {
        hop = true; //maybe this
        float angleStep = (220 - 140) / 2;
        float angle = 140;
        for (int i = 0; 1 < 2 + 1; i++)
        {
            GameObject dart = Instantiate(Bullet, center.position, Quaternion.Euler(new Vector3(0, 0, angle)));

            angle += angleStep;
            if (angle > endAngle)
            {
                break;
            }

            if (rage)
            {
                dart.GetComponent<BoomKnife>().boomTime = 0.5f;
            }
        }
        if (rage)
        {
            GameObject glame1 = Instantiate(Glave, center.position, transform.rotation);
            GameObject glame2 = Instantiate(Glave, center.position, transform.rotation);

            glame1.GetComponent<Glave>().facingRight = true;

            glame2.GetComponent<Glave>().facingRight = false;
        }
    }

    IEnumerator RecoverFromFlip()
    {
        yield return new WaitForSeconds(0.5f);
        waitingForNextAttack = true;
    }

    IEnumerator GlavePoof()
    {
        yield return new WaitForSeconds(0);
        glave.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        GameObject smok = Instantiate(Smoke, new Vector3(glave.transform.position.x, glave.transform.position.y - 0.4f, glave.transform.position.z), transform.rotation);
        yield return new WaitForSeconds(0.5f);
        Destroy(glave);
        threwGlave = false;
        yield return new WaitForSeconds(0.5f);
        Destroy(smok);
    }

    IEnumerator AttackTrack()
    {
        yield return new WaitForSeconds(0);

        if (perched)
        {
            if (rage)
            {
                yield return new WaitForSeconds(0.3f);
                StartCoroutine(SmokeBomb());
                yield return new WaitForSeconds(0.5f);
                hit = Physics2D.Raycast(transform.position, transform.up * -1, 100000, whatIsGround);
                transform.position = hit.point;
                perched = false;
                platformColider.enabled = false;
                StartCoroutine(SmokeBomb());
                waitingForNextAttack = true;
            }
            else
            {
                yield return new WaitForSeconds(3);
                perched = false;
                platformColider.enabled = false;
                anim.SetTrigger("Backflip");
                //Debug.Log("perch");
                yield return new WaitForSeconds(2);
                waitingForNextAttack = true;
            }
        }
        else
        {
            int x = Random.Range(0, 3);
            switch (x)
            {
                case 0:
                    yield return new WaitForSeconds(2);
                    _FlipThrough();
                    break;
                case 1:
                    _GlaveThrowAttk();
                    break;
                case 2:
                    StartCoroutine(_KnifeThrow());
                    break;
            }
        }
    }

    void CheckForDead()
    {
        if (!dead)
        {
            //die
            dead = true;
            waitingForNextAttack = false;
            enemyHealth.canBeHit = false;
            rigidbod2D.velocity = new Vector2(0f, 0f);
            StartCoroutine(DeathEnemy());
        }
    }

    public AudioSource music;
    public AudioSource scream;
    public AudioClip victoryMusic;

    IEnumerator DeathEnemy()
    {
        yield return new WaitForSeconds(0);
        music.enabled = false;
        scream.enabled = true;
        player.invincible = true;
        player.canMove = false;
        player.rigidbody2d.velocity = new Vector2(0, 0);
        player.RealAnimator.SetFloat("Speed", 0);
        screenShake.StartShake(7, 0.1f);
        platformColider.enabled = true;
        anim.SetTrigger("Jutzu");
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(SmokeBomb());
        yield return new WaitForSeconds(0.5f);
        transform.position = deadPo.position;
        StartCoroutine(SmokeBomb());
        anim.SetTrigger("Dead");
        camB.DeadCam();
        //startShake = true;
        //selfShake.StartShake(3, 0.05f);
        yield return new WaitForSeconds(3);
        //Debug.Log("postMort");
        StartAnim();
        anim.SetTrigger("PostMortem");
        yield return new WaitForSeconds(7);
        camB.camFollow = true;
        camB.deadCam = false;
        yield return new WaitForSeconds(4);
        scream.clip = victoryMusic;
        scream.Play();
        player.SwordFlourish();
        camB.Chivo1Get();
        yield return new WaitForSeconds(2);
        //if (HoldmyShit.hasMadePurchase == false & HoldmyShit.lvl5R == true)
        //{
        //    camB.Chivo4Get();
        //}
        SaveManager.instance.Save();
        yield return new WaitForSeconds(2);
        nexLev.FadeOut(true);
    }

    void DamageEffect()
    {
        rigidbod2D.constraints = RigidbodyConstraints2D.None;
        transform.rotation = new Quaternion(0, 0, 0, 0);
        rigidbod2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        if (enemyHealth.canBeHit)
        {
            StartAnim();
            enemyHealth.canBeHit = false;
            StopAllCoroutines();

            if (!startFrontFlip)
            {
                anim.SetTrigger("Pain");
            }
            if (startFrontFlip)
            {
                startFrontFlip = false;
                anim.SetTrigger("Airpain");
            }
            if (threwGlave)
            {
                anim.SetTrigger("Glavepain");
                glave = GameObject.FindGameObjectWithTag("Glave");
                StartCoroutine(GlavePoof());
            }

            rigidbod2D.velocity = new Vector2(0f, 0f);
            rigidbod2D.velocity = new Vector2(0, rigidbod2D.velocity.y);
            rigidbod2D.velocity = new Vector2(0, knockUp);

            if (transform.position.x < trPlayer.position.x)
            {
                rigidbod2D.velocity = new Vector2(-knockBack, rigidbod2D.velocity.y);
                transform.localScale = new Vector2(-1, 1);
                facingRight = true;
            }
            else if (transform.position.x > trPlayer.position.x)
            {
                rigidbod2D.velocity = new Vector2(knockBack, rigidbod2D.velocity.y);
                transform.localScale = new Vector2(1, 1);
                facingRight = false;
            }
            //enemyHealth.SetIsDamage();
            inPain = true;
            //waitingAftPain = true;
        }
    }
}