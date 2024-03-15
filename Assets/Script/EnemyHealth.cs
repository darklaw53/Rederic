using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private bool isDamage = false;
    [SerializeField] private int currentHealth;

    #region vars
    public float agroRange;
    public float knockBack;
    public float knockUp;
    public float agroMemory;
    public int maxHealth;
    
    public bool isAgro = false;
    public bool dead;
    public bool rangeChange;
    public bool canBeHit = true;
    public int lastDam;
    #endregion

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void SetDamage(int damage)
    {
        //Debug.Log("bingus");
        if (canBeHit)
        {
            lastDam = damage;
            isDamage = true;

            if (currentHealth > damage)
            {
                currentHealth -= damage;
            }
            else
            {
                currentHealth = 0;
            }
        }
    }

    #region Get and setters
    public void SetIsDamage()
    {
        isDamage = false;
    }

    public bool GetIsDamage()
    {
        return isDamage;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
    #endregion
}
