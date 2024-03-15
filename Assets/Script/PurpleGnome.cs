using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleGnome : Enemy
{
    public Transform launchPoint;
    public GameObject magiBolt;
    GameObject tempGameObj = default;
    public float offset;
    bool exploded;
    Vector2 tpTarget;
    bool manabolt = false;
    bool tleport = false;
    public bool reloadMMagic = true;
    float explodedCounter;

    public AudioClip magicSound;
    public AudioClip teleportSound;

    public AudioSource meAudio;

    protected override void Start()
    {
        base.Start();

        meAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (watching)
        {
            watching = false;
            StartCoroutine(reloadMagic());
        }
        Teleporting();
        MagiBolt();
    }

    IEnumerator reloadMagic()
    {
        yield return new WaitForSeconds(3f);
        reloadMMagic = true;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (dead)
        {
            Animator.Play("PurpleDead");
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
        MinimalistPatrol();
        AgroManager();
        MiniLookForGround();
        //MagiBolt();
        //Teleporting();
        TeleportChecker();
    }

    void TeleportChecker()
    {
        if (purpleTeleportGo) //this key comes from look for ground
        {
            Teleport();
            purpleTeleportGo = false;
        }
    }

    bool canDoThis = true;

    void MagiBolt()
    {
        //meAudio.clip = magicSound;
        //meAudio.enabled = true;

        distToTarget = Vector3.Distance(debugTransform.position, transform.position);
        distToTargetY = (debugTransform.position.y - transform.position.y);
        distToTargetX = (debugTransform.position.x - transform.position.x);
        if (tempGameObj != null)
        {
            exploded = tempGameObj.GetComponent<MagiBolt>().explodedetect();
        }
        else
        {
            exploded = false;
        }
        if (exploded & canDoThis)
        {
            canDoThis = false;
            explodedCounter++;
            StartCoroutine(Recharge());
        }
        if (canChasePlayer & enemyHealth.GetCurrentHealth() > 0 & (distToPlayer <= 8f))
        {
            if (!haveTarget & onGround & !tpTargetGot & !exploded & reloadMMagic)
            {
                watching = true;
                reloadMMagic = false;
                manabolt = true;
                Animator.SetBool("Idleing", false);
                Animator.Play("Magignome spell");
                StopChasingPlayer();
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
            }
        }
    }

    IEnumerator Recharge()
    {
        yield return new WaitForSeconds(1f);
        if (explodedCounter >= 3)
        {
            Teleport();
            explodedCounter = 0;
            canDoThis = true;
            haveTarget = false;
            canChasePlayer = true;
        }
        else
        {
            haveTarget = false;
            canDoThis = true;
            canChasePlayer = true;
        }
    }

    void Cast()
    {
        if (!tpTargetGot)
        {
            if (manabolt)
            {
                manabolt = false;
                if (distToPlayer > 8f)
                {
                    haveTarget = false;
                    Animator.SetBool("Idleing", true);
                    return;
                }
                ManaBolt();
                meAudio.enabled = false;
                meAudio.clip = magicSound;
                meAudio.enabled = true;
                meAudio.pitch = 2;
            }
            if (tleport)
            {
                tleport = false;
                Teleport();
            }
            StopChasingPlayer();
        }
    }

    void ManaBolt()
    {
        debugTransform.position = player.center.position;
        distToTarget = Vector3.Distance(debugTransform.position, transform.position);
        distToTargetY = (debugTransform.position.y - transform.position.y);
        distToTargetX = (debugTransform.position.x - transform.position.x);
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
        Animator.SetBool("Idleing", true);
        //meAudio.enabled = false;
        if (distToPlayer > 8f)
        {
            exploded = true;
            haveTarget = false;
            return;
        }
        Vector3 difference = launchPoint.position - debugTransform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        debugTransform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        tempGameObj = Instantiate(magiBolt, launchPoint.position, debugTransform.rotation);
    }

    float doomCounter = 0;
    private bool watching;

    public void Teleport() //this function seems to work fine
    {
        meAudio.enabled = false;
        meAudio.clip = teleportSound;
        meAudio.enabled = true;
        meAudio.pitch = 1;

        Vector2 pointA = center.transform.position; // Position of objectA
        Vector2 pointB = player.center.transform.position; // Position of objectB
        float a = 0.5f; // A value between 0 and 1
        Vector2 pointC = Vector3.Lerp(pointA, pointB, a);
        float radius = 8f;
        tpTargetGot = false;
        while (!tpTargetGot)
        {
            Collider2D colliders;
            Vector2 target = (Vector2)transform.position + (radius * Random.insideUnitCircle);
            if (Physics2D.OverlapCircle(target, 1f))
            {
                colliders = Physics2D.OverlapCircle(target, 3f);
            }
            else
            {
                doomCounter++;
                if (doomCounter == 100)
                {
                    doomCounter = 0;
                    break;
                }
                Vector2 endPos;
                endPos = target + Vector2.down * 1000000000;

                RaycastHit2D teleInfo = Physics2D.Linecast(target, endPos);
                if (teleInfo.collider != null)
                {
                    Vector2 trueTarget = teleInfo.point + Vector2.up * 0.5f;
                    Collider2D[] colliders2 = Physics2D.OverlapCircleAll(trueTarget, 1f, playerLayer);
                    if (colliders2.Length > 0)
                    {
                        continue;
                    }
                    else
                    {
                        float teleportingDIstance = Vector2.Distance(transform.position, trueTarget);
                        float newPositionFromPlayer = Vector2.Distance(trPlayer.position, trueTarget);
                        if (teleportingDIstance < 3 | newPositionFromPlayer < 3)
                        {
                            continue;
                        }
                        else
                        {
                            tpTarget = trueTarget;
                            tpTargetGot = true;
                        }
                    }
                }
                else
                {
                    continue;
                }
            }
        }
    }

    void Teleporting()
    {
        if (tpTargetGot) //this is the key from the Teleport function
        {
            if (enemyHealth.GetCurrentHealth() <= 0)
            {
                return;
            }
            Animator.SetBool("Idleing", false);
            tpTargetGot = false;
            canChasePlayer = false;
            Animator.Play("Magignome teleport");
            StopChasingPlayer();
            enemyHealth.canBeHit = false;
        }
    }

    void Blink()
    {
        transform.position = tpTarget;
        Animator.Play("Magignome telepin");
        StopChasingPlayer();
    }

    void Reactivate()
    {
        meAudio.enabled = false;
        enemyHealth.canBeHit = true;
        canChasePlayer = true;
        Animator.Play("Magignome idle");
        Animator.SetBool("Idleing", true);
        StopChasingPlayer();
    }
}