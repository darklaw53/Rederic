using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Transform slider;
    public Transform rascunho;
    public Player rightFace;
    [SerializeField] GameObject player;
    [SerializeField] float timeOffSet;
    float vitality;
    [SerializeField] Vector2 posOffSet;

    void FixedUpdate()
    {
        var newScale = rascunho.localScale;
        vitality = newScale.x;
        if (vitality <= 0)
        {
            slider.localScale = new Vector3(0, 1, 0);
        }
    }

    public void SetMaxHealth(int health)
    {
        slider.localScale = new Vector3(health, 1, 0);
        rascunho.localScale = new Vector3(health, 1, 0);
    }

    public void SetHealth(int health)
    {
        if (vitality > 0)
        {
            rascunho.localScale -= new Vector3(health, 0, 0);
            var newScale = rascunho.localScale;
            vitality = newScale.x;
            if (vitality <= 0)
            {
                return;
            }
            else
            {
                slider.localScale -= new Vector3(health, 0, 0);
            }
        }
    }
}
