using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowUI : MonoBehaviour
{
    ArrowUI bellowStuff;
    public GameObject bellow;
    public Animator anim;
    public ArrowUI lastOne;
    public Player player;
    public GameObject playerObject;
    public bool gotArrow = false;
    public bool fullUp;

    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<Player>();
        anim = GetComponent<Animator>();
        if (bellow != transform)
        {
            bellowStuff = bellow.GetComponent<ArrowUI>();
        }
    }
    private void Update()
    {
        fullUp = bellow != transform & bellowStuff.fullUp;
    }
    public void GotArrow()
    {
        if (!gotArrow)
        {
            //player.arrowNumber++;
            //Debug.Log("gotarrow");
            anim.SetTrigger("Load");
            gotArrow = true;
            if (lastOne.gotArrow)
            {
                fullUp = true;
            }
        }
        else
        {
            if (bellowStuff != null)
            {
                if (bellow != transform & !fullUp)
                {
                    bellowStuff.GotArrow();
                }
                else
                {
                    return;
                }
            }
        }
    }
    public void Fire()
    {
        if (gotArrow)
        {
            //player.arrowNumber--;
            //Debug.Log("fire");
            anim.SetTrigger("Fire");
            gotArrow = false;
        }
        else
        {
            return;
        }
    }
    public void CheckUnder()
    {
        if (bellow != transform)
        {
            if (bellowStuff.gotArrow)
            {
                bellowStuff.Supply();
                anim.SetTrigger("Underload");
                gotArrow = true;
            }
        }     
    }
    void Supply()
    {
        anim.SetTrigger("Passup");
        gotArrow = false;
        if (!lastOne.gotArrow)
        {
            fullUp = false;
        }
    }
}
