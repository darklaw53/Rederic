using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Physics2D.Simulate(Time.deltaTime);
    }
}