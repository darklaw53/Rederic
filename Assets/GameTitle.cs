using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTitle : MonoBehaviour
{
    Animator anim, shimmer;
    public GameObject titleImage, twinkle, swordShimmer, pressAnyButton, nextMennu, thisMenu;
    bool pressAnyKey;

    public AudioSource swordShink;

    public GameObject clickSOund;

    private void Awake()
    {
        anim = transform.GetComponent<Animator>();
        shimmer = swordShimmer.GetComponent<Animator>();
        anim.speed = 0;
        shimmer.speed = 0;
        StartCoroutine(TitleSequence());
    }

    private void Start()
    {
        nextMennu.SetActive(true);
        nextMennu.SetActive(false);
    }

    private void Update()
    {
        if (Input.anyKey & pressAnyKey)
        {
            nextMenu();
        }
    }

    IEnumerator TitleSequence()
    {
        yield return new WaitForSeconds(3f);
        anim.speed = 1;
        twinkle.SetActive(true);
        swordShink.enabled = true;
        yield return new WaitForSeconds(0.5f);
        anim.speed = 0;
        twinkle.SetActive(false);
        shimmer.speed = 1;
        yield return new WaitForSeconds(1f);
        swordShink.enabled = false;
        shimmer.speed = 0;
        pressAnyKey = true;
        pressAnyButton.SetActive(true);
    }

    void nextMenu()
    {
        Instantiate(clickSOund);
        pressAnyButton.SetActive(false);
        nextMennu.SetActive(true);
        thisMenu.SetActive(false);
    }
}
