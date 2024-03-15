using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroIntoSong : MonoBehaviour
{
    public AudioSource BossOST;
    public AudioClip BossMusic;
    public Menu menue;

    void Update()
    {
        if (!BossOST.isPlaying & !menue.GameIsPaused)
        {
            BossOST.clip = BossMusic;
            BossOST.Play();
            BossOST.loop = true;
        }
    }
}
