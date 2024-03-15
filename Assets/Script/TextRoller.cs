using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Febucci.UI;

public class TextRoller : MonoBehaviour
{
    public TextMeshProUGUI testMeshPro;
    public TextAsset text;
    public TextAnimator textAnimator;
    public TextAnimatorPlayer textPlayer;
    public GameObject textBox;

    public int endAtLine;

    bool endOfLine = false;

    public bool texting = false;

    float textSpeed = 0;

    private void Awake()
    {
        if (textAnimator != null)
        {
            textAnimator.onEvent += OnEvent;
        }
        if (textBox != null)
        {
            textBox.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        textAnimator.onEvent -= OnEvent;
    }

    private void Start()
    {
        testMeshPro.text = text.text;    
    }

    private void Update()
    {
        textPlayer.SetTypewriterSpeed(textSpeed);
        if (Input.GetKeyDown(KeyCode.Space))// & textSpeed != 0)
        {
            if (endOfLine == true)
            {
                textSpeed = 1;
                testMeshPro.pageToDisplay++;
                PlayText();
                Debug.Log("endOfLine");
            }
            else
            {
                textSpeed = 10000000;
                //textPlayer.SkipTypewriter();
                Debug.Log("speedUp");
            }
        }
       
        /*if (testMeshPro.pageToDisplay > endAtLine)
        {
            textBox.SetActive(false);
            texting = false;
        }*/
    }

    public void PauseText()
    {
        textPlayer.StopShowingText();
        endOfLine = true;
        textSpeed = 0;
        Debug.Log("pause");
    }

    public void PlayText()
    {
        textPlayer.StartShowingText();
        endOfLine = false;
        Debug.Log("play");
    }

    void OnEvent(string message)
    {
        switch (message)
        {
            case "pause":
                PauseText();
                //<?pause>
                Debug.Log("textpause");
            break;
        }
    }

    void TextBoxOn()
    {
        Debug.Log("tbOn");
        PlayText();
        textBox.SetActive(true);
    }

    void TextBoxOff()
    {
        Debug.Log("tbOff");
        PauseText();
        textBox.SetActive(false);
    }

    public void StartText()
    {
        Debug.Log("textStart");
        textBox.SetActive(true);
        textSpeed = 1;
        texting = true;
    }
}
