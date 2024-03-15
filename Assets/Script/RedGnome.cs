using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedGnome : Enemy
{

    public AudioSource audioSause;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        edgeDetector = edgeDetectorVal;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (dead)
        {
            Animator.Play("RedGnomeDead");
        }
        myV3 = transform.position;
        if (eyesOpen)
        {
            EnemyEyes();
        }
        SpeedDetector();
        GroundDetector();
        DistanceToPlayer();
        ChasePlayer();
        LookForGround();
        CheckIfDamage();
        HitPlayer();
        Patrol();
        AgroManager();

        if(rigidbody2d.velocity.x != 0 & onGround)
        {
            audioSause.enabled = true;
        }
        else
        {
            audioSause.enabled = false;
        }

        if(alert)
        {
            audioSause.pitch = 2.5f;
        }
        else
        {
            audioSause.pitch = 1f;
        }
    }
}