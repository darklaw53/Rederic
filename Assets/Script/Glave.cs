using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glave : MonoBehaviour
{
    public Transform aim;
    public bool facingRight;
    public float speed;
    public float attackRange;
    public float distance;
    public float time;

    public int attackDamage;

    bool timeUp;
    bool landed;

    RaycastHit2D hit;

    GameObject playerControll = default;
    Player player;
    Transform trPlayer;
    private Rigidbody2D rgBdy;
    Vector3 myV3;

    Animator anim;
    public GameObject Smoke;

    void Start()
    {
        playerControll = GameObject.FindGameObjectWithTag("Player");
        player = playerControll.GetComponent<Player>();
        trPlayer = playerControll.GetComponent<Transform>();
        anim = gameObject.transform.GetChild(0).GetComponent<Animator>();
        rgBdy = GetComponent<Rigidbody2D>();
        StartCoroutine(ExampleCoroutine());
    }

    void FixedUpdate()
    {
        myV3 = transform.position;

        if (!landed)
        {
            if (facingRight)
            {
                transform.position += transform.right * Time.deltaTime * speed;
                hit = Physics2D.Raycast(transform.position, transform.right, distance);
            }
            else
            {
                transform.position += (transform.right * -1) * Time.deltaTime * speed;
                hit = Physics2D.Raycast(transform.position, transform.right * -1, distance);
            }
            
            if (hit)
            {
                if (hit.collider.gameObject.tag == "Boss" & timeUp)
                {
                    hit.collider.gameObject.GetComponent<BlackGnome>().CatchGlave();
                    transform.position = hit.point;
                    Destroy(gameObject);
                }
                else if (hit.collider.gameObject.layer == 17 & timeUp)
                {
                    //Debug.Log("hit");
                    landed = true;
                    transform.position = hit.point;
                    HitWall();
                    rgBdy.constraints = RigidbodyConstraints2D.FreezePosition;
                }
            }
        }
        Damage();
    }

    void HitWall()
    {
        anim.SetTrigger("HitWall");
        anim.speed = 0;
        StartCoroutine(PoofAway());
    }

    IEnumerator PoofAway()
    {
        yield return new WaitForSeconds(0);
        GameObject smok = Instantiate(Smoke, new Vector3(transform.position.x, transform.position.y - 0.4f, transform.position.z), transform.rotation);
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(0.5f);
        Destroy(smok);
        Destroy(gameObject);
    }

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(time);

        timeUp = true;
        //Debug.Log("timeup");
    }

    public void Damage()
    {
        if ((Vector3.Distance(player.hitPoint.position, transform.position) < attackRange) & !landed)
        {
            player.TakeDamage(attackDamage, myV3);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
