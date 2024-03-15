using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingGnome : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] EnemyHealth enemyHealth = default;
    public float attackRange;
    public float attackingdistdebug;
    public int attackDamage;
    public Vector3 myV3;
    public GameObject redGnome;
    public Transform spawnLocation;
    public Transform doomLog;
    DoomLog dLog;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); ;
        dLog = doomLog.GetComponent<DoomLog>();
    }

    private void Update()
    {
        CheckIfDamage();
    }

    void CheckIfDamage()
    {
        if (enemyHealth.GetIsDamage())
        {
            enemyHealth.SetIsDamage();
            DamageEffect();
        }
    }

    void DamageEffect()
    {
        GameObject reGno = Instantiate(redGnome, spawnLocation.position, spawnLocation.rotation);
        reGno.GetComponentInChildren<EnemyHealth>().SetDamage(enemyHealth.lastDam);
        dLog.doPush = true;
        Destroy(gameObject);
    }
}
