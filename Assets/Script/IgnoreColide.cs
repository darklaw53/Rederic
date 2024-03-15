using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreColideSponanios : MonoBehaviour
{
    private void Update()
    {
        Physics2D.IgnoreLayerCollision(8, 10);
    }
}
