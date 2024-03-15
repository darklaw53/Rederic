using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    public GameObject cam;
    public float parallaxEffect, pointAnum, pointBnum, mark, persStartpos, cloudSpeed;
    float cloudDist;
    bool care = true;
    public Transform pointA;
    public Transform pointB;

    private void Start()
    {
        persStartpos = transform.position.x;
        if (pointA != null) mark = (pointA.transform.position.x - pointB.transform.position.x) / 2;     
    }

    private void FixedUpdate()
    {
        if (pointA != null & pointB != null)
        {
            pointAnum = pointA.transform.position.x; 
            pointBnum = pointB.transform.position.x;
        }
        //float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);
        
        if (care) transform.position = new Vector2(persStartpos + dist + cloudDist, transform.position.y);

        if (pointA != null)
        {
            if (cloudSpeed != 0)
            {
                cloudDist = cloudDist + cloudSpeed;
                if ((pointA.transform.position.x - pointB.transform.position.x) > -0.01 & (pointA.transform.position.x - pointB.transform.position.x) < 0.01)
                {
                    transform.position = new Vector2(transform.position.x + mark, transform.position.y);
                    cloudDist = 0;
                    care = false;
                }
                else
                {
                    care = true;
                }
            }

            //if (temp > startpos + length) startpos += length;
            //else if (temp < startpos - length) startpos -= length;
        }       
    }
}
