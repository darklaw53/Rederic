using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class priceTrigger : MonoBehaviour
{
    public Shoppingprice shopPrice;
    public float productPrice;
    public bool isArrows;
    public bool isLever;
    public bool inRange;
    public bool hasPurchased;
    public bool pulled;
    public bool lockable;
    bool locked;
    public priceTrigger otherPrice;
    public Player player;
    public eanchor eAnchor;

    private void Update()
    {
        if (Input.GetKeyDown("e") & inRange)
        {
            if (!eAnchor.pressedE)
            {
                eAnchor.meAnim.SetBool("out", true);
            }
            eAnchor.pressedE = true;

            hasPurchased = true;
            if (otherPrice != null)
            {
                otherPrice.hasPurchased = true;
            }

            if (isArrows)
            {
                player.PlusArrow(50);
                Debug.Log("HASDONEAPURCHASE");
                HoldmyShit.hasMadePurchaseStep = true;
            }
            else if (isLever & !locked)
            {
                if (lockable)
                {
                    locked = true;
                }
                pulled = !pulled;
            }
            else
            {
                player.PlusHealth(80);
                Debug.Log("HASDONEAPURCHASE");
                HoldmyShit.hasMadePurchaseStep = true;
            }
        }
    }

    public void TurnOn()
    {
        if (shopPrice != null)
        {
            shopPrice.TurnOn();
        }
    }

    public void TurnOff()
    {
        if (shopPrice != null)
        {
            shopPrice.TurnOff();
        }
    }
}
