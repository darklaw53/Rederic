using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class ToolTipManager : MonoBehaviour
{
    public static ToolTipManager _instance;

    public TextMeshProUGUI textComponent;

    public Image img;

    private void Awake()
    {
        if (_instance != null & _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        //Cursor.visible = true;
        textComponent.text = string.Empty;
        img.enabled = false;
    }

    private void Update()
    {
        transform.position = Input.mousePosition;

        /*if (Input.GetMouseButtonDown(0))
        {
            img.enabled = false;
            textComponent.text = string.Empty;
        }*/
    }

    public void SetAndShowToolTip(String message)
    {
        if (img != null)
        {
            img.enabled = true;
        }
        textComponent.text = message;
    }

    public void HideToolTip()
    {
        if (img != null)
        {
            img.enabled = false;
        }
        textComponent.text = string.Empty;
    }
}
