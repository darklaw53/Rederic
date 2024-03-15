using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string message;

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("bababui");
        ToolTipManager._instance.SetAndShowToolTip(message);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTipManager._instance.HideToolTip();
    }

    void OnDisable()
    {
        ToolTipManager._instance.HideToolTip();
    }
}
