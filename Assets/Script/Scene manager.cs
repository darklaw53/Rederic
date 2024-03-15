using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenemanager : MonoBehaviour
{
    public int index;
    public string levelName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //loading level with build index
            //SceneManager.LoadScene(index);

            //Loading level with scene name
            //SceneManager.LoadScene(levelName);

            //Restart lvl
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
