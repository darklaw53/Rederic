using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public int value;
    public GameObject me = null;
    TreasureBag trueBag;
    bool canBeCaught = true;
    public string myName;
    public Rigidbody2D rigBod;
    public bool shouldBeYeeted = true;
    public LayerMask bodyLayer;
    Transform bodyPosition;
    public Renderer rendererer;

    public GameObject coinSound;

    private void Awake()
    {
        trueBag = GameObject.FindGameObjectWithTag("Bag").GetComponent<TreasureBag>();
        me = Resources.Load<GameObject>(myName);
        rigBod = transform.GetComponent<Rigidbody2D>();
        rendererer = transform.GetComponent<Renderer>();
    }

    private void Start()
    {
        if (shouldBeYeeted)
        {
            GetDroper();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LootDestroyer") & canBeCaught)
        {
            canBeCaught = false;
            trueBag.Boing();
            trueBag.goldScore += value;
            Instantiate(coinSound);
            Destroy(transform.gameObject);
        }
    }

    public void GetYeeted()
    {
        if (transform.position.x > bodyPosition.position.x)
        {
            rigBod.velocity = new Vector2(Random.Range(4.5f, 5.5f), 1f);
        }
        else if (transform.position.x < bodyPosition.position.x)
        {
            rigBod.velocity = new Vector2((Random.Range(4.5f, 5.5f)) * -1f, 1f);
        }
    }

    void GetDroper()
    {
        Collider2D[] Bodys = Physics2D.OverlapCircleAll(transform.position, 0.1f, bodyLayer);
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector2 currentPos = transform.position;
        foreach (Collider2D t in Bodys)
        {
            float dist = Vector2.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t.transform;
                minDist = dist;
            }
        }
        bodyPosition = tMin;
        if (bodyPosition != null)
        {
            GetYeeted();
            StartCoroutine(BlinkOut());
        }
        else
        {
            return;
        }
    }

    IEnumerator BlinkOut()
    {
        yield return new WaitForSeconds(3f);
        float time = 5;
        while (time >= 0)
        {
            rendererer.enabled = false;
            yield return new WaitForSeconds(0.2f);
            rendererer.enabled = true;
            yield return new WaitForSeconds(0.2f);
            time--;
        }
        Destroy(gameObject);
    }
}
