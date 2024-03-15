using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioImage : MonoBehaviour
{
    Animator anikm;
    public Slider slider;

    void Awake()
    {
        anikm = transform.GetComponent<Animator>();
        anikm.speed = 0;
        slider.value = HoldmyShit.volume;
    }

    void Update()
    {
        HoldmyShit.volume = slider.value;

        if (slider.value == 0.0001f)
        {
            anikm.Play("AudioLevelsNew", 0, 0.99f);
        }
        if (slider.value > 0.0001f & slider.value < 0.25f)
        {
            anikm.Play("AudioLevelsNew", 0, 0.75f);
        }
        if (slider.value >= 0.25f & slider.value < 0.50f)
        {
            anikm.Play("AudioLevelsNew", 0, 0.50f);
        }
        if (slider.value >= 0.50f & slider.value < 0.75f)
        {
            anikm.Play("AudioLevelsNew", 0, 0.25f);
        }
        if (slider.value >= 0.75f)
        {
            anikm.Play("AudioLevelsNew", 0, 0.01f);
        }
    }

    public void ClickOnImage()
    {
        Debug.Log("click");
        if (slider.value == 0.0001f)
        {
            slider.value = 1f;
        }
        else
        {
            slider.value = 0.0001f;
        }
    }
}
