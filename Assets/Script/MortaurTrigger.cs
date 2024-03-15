using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortaurTrigger : MonoBehaviour
{
    public bool activated;
    public bool turnedOn;
    public GameObject boulder;
    public GameObject targert;
    public GameObject reticule;
    public GameObject explosion;
    //public float targetTimer;
    //public float betweenShotTimer;
    public GameObject playerControll;
    public Transform playerTf;
    public Vector2 playerPos;
    public Vector2 targetPos;
    [SerializeField] Player player;
    public float explodeRadius;
    float distToPlayer;
    bool notDeltDamage = true;
    public int attackDamage;
    GameObject boulderObj;
    GameObject targetObj;
    GameObject reticuleObj;
    TrackReticuleProgress trackRet;
    public float rockHight;
    public float inacuracy;

    ScreenShakeController screenShake;

    private void Awake()
    {
        screenShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ScreenShakeController>();
        playerControll = GameObject.FindGameObjectWithTag("Player");
        player = playerControll.GetComponent<Player>();
        playerTf = playerControll.transform;
    }

    private void Update()
    {
        playerPos = playerTf.position;
        if (targetPos != null)
        {
            distToPlayer = Vector3.Distance(playerPos, targetPos);
        }
        if (reticuleObj != null)
        {
            if (trackRet.boom)
            {
                Landed();
            }
        }
        if (activated & !turnedOn)
        {
            notDeltDamage = true;
            turnedOn = true;
            targetPos = new Vector2(Random.Range(playerPos.x - inacuracy, playerPos.x + inacuracy), playerPos.y);
            targetObj = Instantiate(targert, targetPos, Quaternion.identity);
            reticuleObj = Instantiate(reticule, targetPos, Quaternion.identity);
            trackRet = reticuleObj.GetComponent<TrackReticuleProgress>();
            Vector2 boulderSpawn = new Vector2(targetPos.x, targetPos.y + rockHight);
            boulderObj = Instantiate(boulder, boulderSpawn, Quaternion.identity);
        }
    }

    public void Landed()
    {
        Instantiate(explosion, targetPos, Quaternion.identity);
        Vector3 myV3 = targetPos;
        screenShake.StartShake(0.3f, 0.1f);
        if (distToPlayer < explodeRadius & notDeltDamage)
        {
            notDeltDamage = false;
            player.TakeDamage(attackDamage, myV3);
        }
        Destroy(targetObj);
        Destroy(reticuleObj);
        Destroy(boulderObj);
        turnedOn = false;
    }
}
