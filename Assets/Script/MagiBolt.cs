using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagiBolt : MonoBehaviour
{
    [SerializeField] GameObject playerControll = default;
    [SerializeField] Player player;
    [SerializeField] Transform trPlayer;
    public float speed;
    Transform targetForBolt;
    Animator animator;
    public float explodeRadius;
    public int attackDamage;
    float distToPlayer;
    float distToTarget;
    bool notDeltDamage = true;
    GameObject Row1;

    ScreenShakeController screenShake;

    public AudioSource audioSause;

    protected virtual void Awake()
    {
        screenShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ScreenShakeController>();
        playerControll = GameObject.FindGameObjectWithTag("Player");
        player = playerControll.GetComponent<Player>();
        trPlayer = playerControll.GetComponent<Transform>();
        Row1 = new GameObject();
        Row1.transform.position = trPlayer.position;
    }

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, explodeRadius);
    }

    private void Update()
    {
        distToPlayer = Vector3.Distance(trPlayer.position, transform.position);
        distToTarget = Vector3.Distance(Row1.transform.position, transform.position);

        if ((distToTarget < 0.45f) | (distToPlayer < explodeRadius))
        {
            explode();
            return;
        }

        if (notDeltDamage)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }

    bool exploded = false;

    void explode()
    {
        audioSause.enabled = true;

        if (!exploded)
        {
            Vector3 myV3 = transform.position;
            transform.position = transform.position;
            screenShake.StartShake(0.3f, 0.1f);
            if (distToPlayer < explodeRadius & notDeltDamage)
            {
                notDeltDamage = false;
                player.TakeDamage(attackDamage, myV3);
            }
            animator.SetBool("Explode", true);
            exploded = true;
        }
    }

    public bool explodedetect()
    {
        return exploded;
    }

    void die()
    {
        //exploded = true;
        Destroy(Row1);
        Destroy(gameObject);
    }
}
