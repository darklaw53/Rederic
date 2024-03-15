using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureBag : MonoBehaviour
{
    public int goldScore;
    public Text scoreBoard;
    public Transform lootDrop;
    float coinsIn = 0;
    float lootIn = 0;
    public GameObject coin;
    bool running;
    public Transform pointOfPocketing;
    public Transform father;
    Animator anim;
    public GameObject back;
    SpriteRenderer backRender;
    Queue<GameObject> colectedLoot = new Queue<GameObject>();

    void Awake()
    {
        backRender = back.GetComponent<SpriteRenderer>();
        anim = transform.GetComponent<Animator>();
        goldScore = HoldmyShit.keepScore;
        scoreBoard.color = Color.white;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (lootIn <= 0)
        {
            lootIn = 0;
            running = false;
        }
        scoreBoard.text = goldScore.ToString();
        HoldmyShit.keepScore = goldScore;
    }

    public void CashLoot(float val, GameObject it)
    {
        if (val !=10)
        {
            lootIn++;
            colectedLoot.Enqueue(it);
            if (!running)
            {
                StartCoroutine(CountMoney());
            }
        }
        else //working porfectly
        {
            coinsIn = coinsIn+val;
            while (coinsIn > 0)
            {
                colectedLoot.Enqueue(coin);
                coinsIn--;
                lootIn++;
            }
            if (!running)
            {
                StartCoroutine(CountMoney());
            }
        }
    }

    public bool stopTheCount;
    public bool readyToProceed;
    IEnumerator CountMoney()
    {
        while (lootIn > 0)
        {
            running = true;
            //GameObject item = colectedLoot.Dequeue();
            var nuCoin = Instantiate(colectedLoot.Dequeue(), lootDrop.position, Quaternion.Euler(new Vector4(0, 0, Random.Range(0f, 360f), 0)));
            nuCoin.transform.parent = father.transform;
            nuCoin.layer = 12;
            yield return new WaitForSeconds(0.5f);
            lootIn--;
            if (stopTheCount)
            {
                while (lootIn > 0)
                {
                    running = true;
                    nuCoin = Instantiate(colectedLoot.Dequeue(), lootDrop.position, Quaternion.Euler(new Vector4(0, 0, Random.Range(0f, 360f), 0)));
                    nuCoin.transform.parent = father.transform;
                    nuCoin.layer = 12;
                    lootIn--;
                    continue;
                }
                readyToProceed = true;
                stopTheCount = false;
            }
            else
            {
                continue;
            }
        }
    }

    public void Boing()
    {
        anim.SetTrigger("CashIn");
        backRender.enabled = false;
    }

    public void Done()
    {
        backRender.enabled = true;
    }
}
