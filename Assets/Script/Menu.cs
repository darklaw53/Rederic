using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    public bool GameIsPaused;
    public GameObject pauseMenuUI;

    public static bool gameIsPaused = false;

    Scene m_Scene;
    string sceneName;

    //GameObject mooseCurser;

    public GameObject panell;
    public Slider slide;
    public AudioMixer mixer;

    public AudioSource pauseMusic;

    GameObject playerControll = default;
    MeeleCombat player;

    private void Awake()
    {
        playerControll = GameObject.FindGameObjectWithTag("Player");
        player = playerControll.GetComponent<MeeleCombat>();

        //mooseCurser = GameObject.FindGameObjectWithTag("Mouse");
        //mooseCurser.SetActive(false);

        pauseMenuUI = transform.GetChild(0).gameObject;

        m_Scene = SceneManager.GetActiveScene();
        sceneName = m_Scene.name;

        pauseMusic.ignoreListenerPause = true;
        pauseMusic.Play();
        pauseMusic.Pause();

        //Debug.Log("checkpoint" + HoldmyShit.volume);
        slide.value = HoldmyShit.volume;
        //Debug.Log("also lvl1" + HoldmyShit.volume);
        mixer.SetFloat("musicvol", Mathf.Log10(slide.value) * 20);
        panell.SetActive(false);
    }

    private void Start()
    {
        
        /*slide.value = HoldmyShit.volume;
        mixer.SetFloat("musicvol", Mathf.Log10(slide.value) * 20);
        panell.SetActive(false);*/
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Escape();
        }
    }

    public void Escape()
    {
        if (GameIsPaused)
        {
            GameIsPaused = false;
            Resume();
            NoMenue();
        }
        else
        {
            GameIsPaused = true;
            Pause();
            Menue();
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        //AudioListener.pause = false;

        AudioSource[] audios = FindObjectsOfType<AudioSource>();

        foreach (AudioSource a in audios)
        {
            a.UnPause();
        }

        pauseMusic.Pause();

        player.canhit = true;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        //AudioListener.pause = true;

        AudioSource[] audios = FindObjectsOfType<AudioSource>();

        foreach (AudioSource a in audios)
        {
            a.Pause();
        }

        pauseMusic.UnPause();

        player.canhit = false;
    }

    public void Menue()
    {
        pauseMenuUI.SetActive(true);
        //mooseCurser.SetActive(true);
        //Cursor.visible = false;
    }

    public void NoMenue()
    {
        pauseMenuUI.SetActive(false);
        //mooseCurser.SetActive(false);
        //Cursor.visible = false;
    }

    public void LoadMenu()
    {
        SaveManager.instance.Save();
        GameIsPaused = false;
        Resume();
        SceneManager.LoadScene("MenuScene");
    }

    public void QuitGame()
    {
        GameIsPaused = false;
        Resume();
        Application.Quit();
    }

    public void Restarting()
    {
        GameIsPaused = false;
        Resume();
        SceneManager.LoadScene(sceneName);
    }
}
