using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtySpike : MonoBehaviour
{
    public Player player;
    public Vector3 myV3;
    public int attackDamage;

    private void Update()
    {
        myV3 = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            player = collision.gameObject.GetComponent<Player>();
            if (player.currentHealth > 0)
            {
                player.TakeDamage(attackDamage, myV3);
            }
        }
        if (collision.gameObject.layer == 10)
        {
            collision.gameObject.GetComponent<EnemyHealth>().SetDamage(attackDamage);
        }
    }
}
