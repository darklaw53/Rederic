using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBramble : MonoBehaviour
{
    [SerializeField] protected GameObject playerControll = default;
    [SerializeField] protected Player player;
    public Vector3 myV3;
    public int attackDamage;
    bool reseting;
    public Animator levelScreen;
    GameObject levelChanger;
    NextLevel nexLev;

    private void Awake()
    {
        playerControll = GameObject.FindGameObjectWithTag("Player");
        player = playerControll.GetComponent<Player>();
        levelChanger = GameObject.FindGameObjectWithTag("Level");
        levelScreen = levelChanger.GetComponent<Animator>();
        nexLev = levelChanger.GetComponent<NextLevel>();
    }

    private void Update()
    {
        myV3 = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8 & !reseting)
        {
            if (player.lookForGround)
            {
                ResetFromPain();
                reseting = true;
                StartCoroutine(Setre());
                return;
            }
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

    void ResetFromPain()
    {
        nexLev.FadeOut(false);
        player.Die(true);
    }

    IEnumerator Setre()
    {
        yield return new WaitForSeconds(1f);
        reseting = false;
    }
}
