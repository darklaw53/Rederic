using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleCombatMaybe : MonoBehaviour
{
    public EnemyHealth enemyHealth;
    public Player player;
    
    public Transform mostRecentAttacker;
    public bool gotHit;
    void MostRecentAttacker(Transform attacker)
    {
        mostRecentAttacker = attacker;
        gotHit = true;
    }
    void FixedUpdate()
    {
        if (gotHit == true)
        {
            //knockback specifics
            gotHit = false;
        }
    }
    //death

    //agression

    //player health hooked up to big healthbar
}
