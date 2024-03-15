using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomKnife : MonoBehaviour
{
    public Animator anim;
    Rigidbody2D rgBdy;

    Vector3 shooDIr;
    Vector3 lastPose;

    bool landed;
    bool timeUp;

    public float speed;
    public float time;
    public float boomTime;
    public float distance;
    public float attackRange;

    public int attackDamage;

    GameObject playerControll = default;
    Player player;
    Transform trPlayer;
    Vector3 myV3;

    public GameObject boom;

    ScreenShakeController screenShake;

    public AudioSource auSause;
    //public AudioSource auSause2;

    private void Awake()
    {
        screenShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ScreenShakeController>();
    }

    private void Start()
    {
        playerControll = GameObject.FindGameObjectWithTag("Player");
        player = playerControll.GetComponent<Player>();
        trPlayer = playerControll.GetComponent<Transform>();

        rgBdy = GetComponent<Rigidbody2D>();
        var rot = transform.rotation; // rot = 45 degrees rotation around Y
        shooDIr = rot * Vector2.up; // rotate vector forward 45 degrees around Y
        StartCoroutine(ExampleCoroutine());
    }

    private void FixedUpdate()
    {
        myV3 = transform.position;
        if (!landed)
        {
            transform.position += shooDIr * Time.deltaTime * speed;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, shooDIr, distance, 1 << LayerMask.NameToLayer("Ground"));
            if (hit & timeUp)
            {
                //Debug.Log("hit");
                landed = true;
                transform.position = hit.point;
                StartCoroutine(Explode());
                rgBdy.constraints = RigidbodyConstraints2D.FreezePosition;
            }
        }
    }

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(time);

        timeUp = true;
        //Debug.Log("timeup");
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(0);
        anim.SetTrigger("Sizzle");

        yield return new WaitForSeconds(boomTime);
        anim.SetTrigger("Boom");
        screenShake.StartShake(0.3f, 0.1f);
        auSause.Play();
        //Instantiate(boom);

        //Destroy(gameObject);
        yield return new WaitForSeconds(0.1f);
        Damage();

        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    public void Damage()
    {
        if (Vector3.Distance(player.hitPoint.position, transform.position) < attackRange)
        {
            player.TakeDamage(attackDamage, myV3);
        }
    }
}
