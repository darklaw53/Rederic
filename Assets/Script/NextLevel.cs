using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public int nextLevel;
    Animator anim;
    bool waarp;
    GameObject playerControll;
    Player player;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerControll = GameObject.FindGameObjectWithTag("Player");
        if (playerControll != null)
        {
            player = playerControll.GetComponent<Player>();
        }
    }

    public void FadeOut(bool warp)
    {
        anim.SetTrigger("Fade Out");
        waarp = warp;
    }

    public void OnFadeComplete()
    {
        if (waarp)
        {
            SceneManager.LoadScene(nextLevel);
        }
        else
        {
            if (player != null)
            {
                playerControll.transform.position = player.astualSafeGround;
                anim.Play("Fade screen");
                player.invincible = false;
                player.dead = false;
            }
        }
    }
}
