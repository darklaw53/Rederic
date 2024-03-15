using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioControl : MonoBehaviour
{
    public AudioMixer mixer;

    /*public void SetLevel (float sliderValue)
    {
        mixer.SetFloat("musicvol", Mathf.Log10(sliderValue) * 20);
    }*/

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("musicvol", Mathf.Log10(sliderValue) * 20);
        HoldmyShit.volume = sliderValue;
        SaveManager.instance.Save();
        //Debug.Log("main menu" + HoldmyShit.volume);
    }
}
