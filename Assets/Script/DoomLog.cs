using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoomLog : MonoBehaviour
{
    public Transform hooktransform;
    public float leftPushRange;
    public float rightPushRange;
    public float velocityThreshold;
    float MaxAngleDeflection = 60.0f;
    float SpeedOfPendulum = 1.0f;
    float time = -5f;
    public Transform standinTR;
    public ParticleSystem creakFetti1;
    public ParticleSystem creakFetti2;
    public GameObject camCam;
    public Transform goDownCamLocal;
    public Transform loweringPillar;
    public Transform loweringPillarPos;
    public lever levrr;
    bool stopDrop;
    bool retimed;
    bool canGetHooked = true;
    public bool doPush = true;
    public GameObject hookSpot;
    bool firstPush = true;
    public GameObject pauseMenu;
    Menu mmenu;
    public GameObject mainCam;
    CameraBrain cam;
    public Transform endPos;
    public float timeOffSet;

    public float smooth;
    public AudioSource creek;
    //public AudioSource woosh;
    //public AudioSource crack;

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(hooktransform.position, transform.position);
        mmenu = pauseMenu.GetComponent<Menu>();
        cam = mainCam.GetComponent<CameraBrain>();
    }

    private void Update()
    {
        if (firstPush)
        {
            Push();
            //creek.Play();
            //woosh.Play();
            firstPush = false;
        }

        if (levrr.onned & !stopDrop)
        {
            Drop();
            if (!retimed)
            {
                time = -4;
                MaxAngleDeflection = 40;
                retimed = true;
            }
        }

        transform.position = standinTR.position;
        transform.rotation = standinTR.rotation;
        
        if (doPush)
        {
            Push();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "LogStop")
        {
            creek.Stop();
           // woosh.Stop();
            creek.Play();
            //woosh.Play();
            //crack.Play();
            creakFetti1.Play(true);
            creakFetti2.Play(true);
        }
        if (collision.tag == "HookZone" & canGetHooked)
        {
            //crack.Play();
            doPush = false;
            Destroy(hookSpot);
        }
    }

    public void Push()
    {
        time += Time.deltaTime;
        float angle = MaxAngleDeflection * Mathf.Sin(time * SpeedOfPendulum);
        hooktransform.localRotation = Quaternion.Euler(0, 0, angle);
    }

    public void Drop()
    {
        //camCam.transform.position = goDownCamLocal.position;
        loweringPillar.position = Vector2.Lerp(loweringPillar.position, loweringPillarPos.position, Time.deltaTime * smooth);

        if (loweringPillarPos.position.y - loweringPillar.position.y >= -0.05)
        {
            loweringPillar.position = loweringPillarPos.position;
            stopDrop = true;
        }
    }
}
