using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursur : MonoBehaviour
{
    SpriteRenderer rend;
    public Sprite handCursor;
    public Sprite normalCursor;

    private void Awake()
    {
        Cursor.visible = false;
        rend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Cursor.visible = false;

        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPos;

        if (Input.GetMouseButtonDown(0))
        {
            rend.sprite = handCursor;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            rend.sprite = normalCursor;
        }
    }
}
