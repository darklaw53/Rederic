using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnlockLevel : MonoBehaviour
{
    int currentLevelNUmb;
    [SerializeField] protected GameObject playerControll = default, bagControll = default;
    [SerializeField] protected Player player;
    [SerializeField] protected TreasureBag cashBag;
    [SerializeField] protected Transform trPlayer;

    private void Awake()
    {
        playerControll = GameObject.FindGameObjectWithTag("Player");
        bagControll = GameObject.FindGameObjectWithTag("Bag");
        player = playerControll.GetComponent<Player>();
        cashBag = bagControll.GetComponent<TreasureBag>();
        trPlayer = playerControll.GetComponent<Transform>();

        currentLevelNUmb = SceneManager.GetActiveScene().buildIndex;
        if (currentLevelNUmb == 3)
        {
            HoldmyShit.lvl2 = true;
            //HoldmyShit.keepHealth2 = HoldmyShit.keepHealth;
            //HoldmyShit.arrows2 = HoldmyShit.arrows;
            //HoldmyShit.keepScore2 = HoldmyShit.keepScore;
        }
        if (currentLevelNUmb == 5)
        {
            HoldmyShit.lvl3 = true;
            //HoldmyShit.keepHealth3 = HoldmyShit.keepHealth;
            //HoldmyShit.arrows3 = HoldmyShit.arrows;
            //HoldmyShit.keepScore3 = HoldmyShit.keepScore;
        }
        if (currentLevelNUmb == 7)
        {
            HoldmyShit.lvl4 = true;
            //HoldmyShit.keepHealth4 = HoldmyShit.keepHealth;
            //HoldmyShit.arrows4 = HoldmyShit.arrows;
            //HoldmyShit.keepScore4 = HoldmyShit.keepScore;
        }
        if (currentLevelNUmb == 9)
        {
            HoldmyShit.lvl5 = true;
            //HoldmyShit.keepHealth5 = HoldmyShit.keepHealth;
            //HoldmyShit.arrows5 = HoldmyShit.arrows;
            //HoldmyShit.keepScore5 = HoldmyShit.keepScore;
        }
    }
}
