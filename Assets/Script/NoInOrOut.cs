using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoInOrOut : MonoBehaviour
{
    public Animator meAnim;

    public void NoIn()
    {
        meAnim.SetBool("in", false);
    }

    public void NoOut()
    {
        meAnim.SetBool("out", false);
    }
}
