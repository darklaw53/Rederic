using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Shoppingprice : MonoBehaviour
{
    private Tilemap _sRenderer;
    private WaitForSeconds _threeSecs = new WaitForSeconds(3f);

    private Color _spriteColour;
    private float _originalalpha;

    private void Start()
    {
        _sRenderer = GetComponent<Tilemap>();

        _spriteColour = _sRenderer.color;
        _originalalpha = _spriteColour.a;
    }

    /*private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            _spriteColour.a = 100f;
            _sRenderer.color = _spriteColour;
        }

        if (Input.GetKeyDown("q"))
        {
            _spriteColour.a = _originalalpha;
            _sRenderer.color = _spriteColour;
        }
    }*/

    public void TurnOn()
    {
        _spriteColour.a = 100f;
        _sRenderer.color = _spriteColour;
    }

    public void TurnOff()
    {
        _spriteColour.a = _originalalpha;
        _sRenderer.color = _spriteColour;
    }

    IEnumerator Waiter()
    {
        while (true)
        {
            int wait_time = Random.Range(1, 11);

            yield return new WaitForSecondsRealtime(wait_time);

            _spriteColour.a = 0f;
            _sRenderer.color = _spriteColour;

            yield return _threeSecs;

            _spriteColour.a = _originalalpha;
            _sRenderer.color = _spriteColour;
        }
    }
}
