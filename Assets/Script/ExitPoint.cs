using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPoint : MonoBehaviour
{
    public Animator levelScreen;
    public TreasureBag tBag;
    GameObject levelChanger;
    NextLevel nexLev;

    public int nextLevel;

    private void Awake()
    {
        nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        levelChanger = GameObject.FindGameObjectWithTag("Level");
        levelScreen = levelChanger.GetComponent<Animator>();
        nexLev = levelChanger.GetComponent<NextLevel>();
    }

    public void ExitProtocol()
    {
        if (HoldmyShit.hasMadePurchaseStep)
        {
            HoldmyShit.hasMadePurchaseStep = false;
            HoldmyShit.hasMadePurchase = true;
        }

        nexLev.FadeOut(true);
        tBag.stopTheCount = true;
        if (nextLevel == 10)
        {
            nextLevel = 0;
        }
        levelScreen.GetComponent<NextLevel>().nextLevel = nextLevel;
    }
}
