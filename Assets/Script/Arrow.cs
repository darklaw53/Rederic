using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    [SerializeField] protected GameObject playerControll = default;
    [SerializeField] protected Player player;
    [SerializeField] protected Transform trPlayer;
    public bool facingRight;
    public int attackDamage;
    //public Transform attackPoint;
    //public float attackRange;
    public LayerMask enemyLayers;
    public bool hitEnemy;
    public bool halted;
    public bool gotMoving;
    public float speed;
    GameObject daddy = null;
    Collider2D collider2d;

    public GameObject arrowThunkSound;

    void Awake()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        collider2d = transform.GetComponent<Collider2D>();
        playerControll = GameObject.FindGameObjectWithTag("Player");
        player = playerControll.GetComponent<Player>();
        trPlayer = playerControll.GetComponent<Transform>();
        if (!player.facingRight)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            facingRight = false;
        }
        else
        {
            facingRight = true;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.layer == 19 | collision.gameObject.layer == 10)  & !hitEnemy)
        {
            Instantiate(arrowThunkSound);

            hitEnemy = true;
            collision.gameObject.GetComponent<EnemyHealth>().SetDamage(attackDamage);
            if (collision.gameObject.GetComponent<EnemyHealth>().GetCurrentHealth() <= 0)
            {
                HoldmyShit.hasDoneAKill = true;
                Debug.Log("HASDONEKILL");
                HoldmyShit.totalArrowKills++;
                if (HoldmyShit.totalArrowKills >= 20)
                {
                    player.camB.Chivo5Get();
                }
            }
            transform.parent = collision.gameObject.transform;
            daddy = collision.gameObject;
            halted = true;
            Destroy(rigidbody2d);
            collider2d.enabled = false;
            //Arrow script = transform.GetComponent<Arrow>();
            //script.enabled = false;
        }
    }
    void Update()
    {
        if (rigidbody2d != null)
        {
            speed = rigidbody2d.velocity.x;
        }

        if (daddy != null)
        {
            //what to do if enemy with arrow in them dies
        }
    }
    void FixedUpdate()
    {
        if (!halted)
        {
            if (facingRight)
            {
                rigidbody2d.velocity = new Vector3(30, rigidbody2d.velocity.y, 0);
            }
            else
            {
                rigidbody2d.velocity = new Vector3(-30, rigidbody2d.velocity.y, 0);
            }
        }

        if (hitEnemy)
        {
            halted = true;
        }
    }
}
