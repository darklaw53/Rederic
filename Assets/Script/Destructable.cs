using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Destructable : MonoBehaviour
{
    public Tilemap destructableTilemap;
    public ParticleSystem bombFetti;

    public GameObject crack;

    private void Start()
    {
        destructableTilemap = GetComponent<Tilemap>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Destroyer"))
        {
            Vector3 hitPosition = Vector3.zero;
            foreach (ContactPoint2D hit in collision.contacts)
            {
                hitPosition.x = hit.point.x - (0.01f * hit.normal.x);
                hitPosition.y = hit.point.y - (0.01f * hit.normal.y);
                destructableTilemap.SetTile(destructableTilemap.WorldToCell(hitPosition), null);
                if (bombFetti != null)
                {
                    if (crack != null)
                    {
                        Instantiate(crack);
                    }
                    Instantiate(bombFetti, hitPosition, transform.rotation);
                }
            }
        }
    }
}
