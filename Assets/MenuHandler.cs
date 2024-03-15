using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    public GameObject eyeTwinkle, coverMenu, coverLevelMenu, coverLevelMenu2, mainMenu, selectLevel, achievmants,
        level1, level2, level3, level4, level5, level2C, level3C, level4C, level5C,
        BountyComplete, Juggling, Hoarder, Frugal, UnloadQuiver, DoNoEvil,
        BountyCompleteC, JugglingC, HoarderC, FrugalC, UnloadQuiverC, DoNoEvilC;

    Animator levelScreen;
    GameObject levelChanger;
    NextLevel nexLev;

    public GameObject clickSOund;
    public GameObject mouseOverSOund;

    bool doingAThing;

    private void Start()
    {
        levelChanger = GameObject.FindGameObjectWithTag("Level");
        levelScreen = levelChanger.GetComponent<Animator>();
        nexLev = levelChanger.GetComponent<NextLevel>();
        eyeTwinkle.SetActive(false);
        if (HoldmyShit.lvl2)
        {
            UnlockLevel2();
        }
        if (HoldmyShit.lvl3)
        {
            UnlockLevel3();
        }
        if (HoldmyShit.lvl4)
        {
            UnlockLevel4();
        }
        if (HoldmyShit.lvl5)
        {
            UnlockLevel5();
        }

        if (HoldmyShit.BountyCompleteR)
        {
            UnlockBountyComplete();
        }
        if (HoldmyShit.JugglingR)
        {
            UnlockJuggling();
        }
        if (HoldmyShit.HoarderR)
        {
            UnlockHoarder();
        }
        if (HoldmyShit.FrugalR)
        {
            UnlockFrugal();
        }
        if (HoldmyShit.UnloadQuiverR)
        {
            UnlockUnloadQuiver();
        }
        if (HoldmyShit.DoNoEvilR)
        {
            UnlockDoNoEvil();
        }
    }

    public void MouseOverSound()
    {
        Instantiate(mouseOverSOund);
    }

    public void ClickSound()
    {
        Instantiate(clickSOund);
    }

    public void UnlockBountyComplete()
    {
        BountyCompleteC.SetActive(true);
        BountyComplete.SetActive(false);
    }

    public void UnlockJuggling()
    {
        JugglingC.SetActive(true);
        Juggling.SetActive(false);
    }

    public void UnlockHoarder()
    {
        HoarderC.SetActive(true);
        Hoarder.SetActive(false);
    }

    public void UnlockFrugal()
    {
        FrugalC.SetActive(true);
        Frugal.SetActive(false);
    }

    public void UnlockUnloadQuiver()
    {
        UnloadQuiverC.SetActive(true);
        UnloadQuiver.SetActive(false);
    }

    public void UnlockDoNoEvil()
    {
        DoNoEvilC.SetActive(true);
        DoNoEvil.SetActive(false);
    }

    public void GoEyeTwinkle()
    {
        StartCoroutine(EyeTwankle());
        //doingAThing = true;
    }

    public void GoNewGame()
    {
        if (!doingAThing)
        {
            StartCoroutine(NewGame());
            doingAThing = true;
        }
    }

    public void GoLevel1()
    {
        if (!doingAThing)
        {
            StartCoroutine(Level1());
            doingAThing = true;
        }
    }

    public void GoLevel2()
    {
        if (!doingAThing)
        {
            StartCoroutine(Level2());
            doingAThing = true;
        }
    }

    public void GoLevel3()
    {
        if (!doingAThing)
        {
            StartCoroutine(Level3());
            doingAThing = true;
        }
    }

    public void GoLevel4()
    {
        if (!doingAThing)
        {
            StartCoroutine(Level4());
            doingAThing = true;
        }
    }

    public void GoLevel5()
    {
        if (!doingAThing)
        {
            StartCoroutine(Level5());
            doingAThing = true;
        }
    }

    public void GoLevelSelect()
    {
        if (!doingAThing)
        {
            StartCoroutine(LevelSelect());
            doingAThing = true;
        }
    }

    public void GoAchievmants()
    {
        if (!doingAThing)
        {
            StartCoroutine(Achievmants());
            doingAThing = true;
        }
    }

    public void GoBack()
    {
        if (!doingAThing)
        {
            StartCoroutine(Back());
            doingAThing = true;
        }
    }

    public void UnlockLevel2()
    {
        level2.SetActive(true);
        level2C.SetActive(false);
    }

    public void UnlockLevel3()
    {
        level3.SetActive(true);
        level3C.SetActive(false);
    }

    public void UnlockLevel4()
    {
        level4.SetActive(true);
        level4C.SetActive(false);
    }

    public void UnlockLevel5()
    {
        level5.SetActive(true);
        level5C.SetActive(false);
    }

    public void Quit()
    {
        SaveManager.instance.Save();
        Application.Quit();
    }

    IEnumerator EyeTwankle()
    {
        yield return new WaitForSeconds(0f);
        eyeTwinkle.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        eyeTwinkle.SetActive(false);
    }

    IEnumerator NewGame()
    {
        yield return new WaitForSeconds(0.5f);
        HoldmyShit.keepScore = 0;
        HoldmyShit.keepHealth = 100;
        HoldmyShit.arrows = 0;
        nexLev.nextLevel = 1;
        nexLev.FadeOut(true);
    }

    IEnumerator Level1()
    {
        yield return new WaitForSeconds(0.5f);
        HoldmyShit.keepScore = 0;
        HoldmyShit.keepHealth = 100;
        HoldmyShit.arrows = 0;
        nexLev.nextLevel = 1;
        nexLev.FadeOut(true);
    }

    IEnumerator Level2()
    {
        yield return new WaitForSeconds(0.5f);
        HoldmyShit.keepScore = HoldmyShit.keepScore2;
        HoldmyShit.keepHealth = HoldmyShit.keepHealth2;
        HoldmyShit.arrows = HoldmyShit.arrows2;
        nexLev.nextLevel = 3;
        nexLev.FadeOut(true);
    }

    IEnumerator Level3()
    {
        yield return new WaitForSeconds(0.5f);
        HoldmyShit.keepScore = HoldmyShit.keepScore3;
        HoldmyShit.keepHealth = HoldmyShit.keepHealth3;
        HoldmyShit.arrows = HoldmyShit.arrows3;
        nexLev.nextLevel = 5;
        nexLev.FadeOut(true);
    }

    IEnumerator Level4()
    {
        yield return new WaitForSeconds(0.5f);
        HoldmyShit.keepScore = HoldmyShit.keepScore4;
        HoldmyShit.keepHealth = HoldmyShit.keepHealth4;
        HoldmyShit.arrows = HoldmyShit.arrows4;
        nexLev.nextLevel = 7;
        nexLev.FadeOut(true);
    }

    IEnumerator Level5()
    {
        yield return new WaitForSeconds(0.5f);
        HoldmyShit.keepScore = HoldmyShit.keepScore5;
        HoldmyShit.keepHealth = HoldmyShit.keepHealth5;
        HoldmyShit.arrows = HoldmyShit.arrows5;
        nexLev.nextLevel = 9;
        nexLev.FadeOut(true);
    }

    IEnumerator LevelSelect()
    {
        yield return new WaitForSeconds(0.5f);
        coverMenu.GetComponent<Animator>().Play("fadeout");
        yield return new WaitForSeconds(2f);
        mainMenu.SetActive(false);
        selectLevel.SetActive(true);
        doingAThing = false;
    }

    IEnumerator Achievmants()
    {
        yield return new WaitForSeconds(0.5f);
        coverMenu.GetComponent<Animator>().Play("fadeout");
        yield return new WaitForSeconds(2f);
        mainMenu.SetActive(false);
        achievmants.SetActive(true);
        doingAThing = false;
    }

    IEnumerator Back()
    {
        yield return new WaitForSeconds(0.5f);
        if (coverLevelMenu == isActiveAndEnabled)
        {
            coverLevelMenu.GetComponent<Animator>().Play("fadeout");
        }
        if (coverLevelMenu2 == isActiveAndEnabled)
        {
            coverLevelMenu2.GetComponent<Animator>().Play("fadeout");
        }
        yield return new WaitForSeconds(2f);
        selectLevel.SetActive(false);
        achievmants.SetActive(false);
        mainMenu.SetActive(true);
        doingAThing = false;
    }
}
