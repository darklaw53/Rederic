using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBubble : MonoBehaviour
{
    public GameObject tutSubject;
    //Player player;
    GameObject playerControll = default;
    float distToPlyaer;
    Transform trPlayer;
    float tetSubOpacity;
    SpriteRenderer tutRend;

    protected virtual void Awake()
    {
        playerControll = GameObject.FindGameObjectWithTag("Player");
        trPlayer = playerControll.GetComponent<Transform>();
        tutRend = tutSubject.GetComponent<SpriteRenderer>();
        //player = playerControll.GetComponent<Player>();
    }

    private void Update()
    {
        distToPlyaer = Vector3.Distance(trPlayer.position, transform.position);
        if (distToPlyaer > 1)
        {
            tetSubOpacity = 0;
        }
        else
        {
            tetSubOpacity = 1 - (distToPlyaer);
        }
        tutRend.color = new Color(1.0f, 1.0f, 1.0f, tetSubOpacity); ;
    }
}
