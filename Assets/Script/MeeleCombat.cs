using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleCombat : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private Player player;
    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayers;
    public int attackDamage;
    public float attackRate;
    float nextAttackTime = 0f;

    public GameObject swordSwingSound;

    public bool canhit = true;

    private void Start()
    {
        canhit = true;
    }

    void Update()
    {
        //canhit = player.canMove;
        if(Time.time >= nextAttackTime & player.canMove & canhit)
        {
            if ((Input.GetKeyDown(KeyCode.Space) | Input.GetKeyDown(KeyCode.Z)) && animator.GetBool("isAttacking") == false && animator.GetBool("shooting") == false)
            {
                Instantiate(swordSwingSound);
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack()
    {
        //play attack animation
        animator.SetBool("isAttacking", true);

        //Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //damage them
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().SetDamage(attackDamage);
            if (enemy.gameObject.GetComponent<EnemyHealth>().GetCurrentHealth() <= 0)
            {
                HoldmyShit.hasDoneAKill = true;
            }
        }

        /*if (hitEnemies.Length > 0)
        {
            CheckForCrime(hitEnemies);
        }
        */
        hitEnemies = null;
    }

    /*IEnumerator CheckForCrime(Collider2D[] victims)
    {
        yield return new WaitForSeconds(3f);
        foreach (Collider2D enemy in victims)
        {
            if (enemy == null)
            {
                //HoldmyShit.hasDoneAKill = true;
            }
        }
    }*/

    //this is being referenced from the animation
    void EndAttack()
    {
        animator.SetBool("isAttacking", false);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
