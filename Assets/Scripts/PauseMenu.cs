using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {


    public bool Paused;
    public GameObject PauseMenu1;
    public Manager manager;

    public string Character;
    public string Level;
    public string Home;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (Paused)
        {
            PauseMenu1.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            PauseMenu1.SetActive(false);
            Time.timeScale = 1f;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Paused = !Paused;

        }
    }
    public void LevelSelect()
    {
        Paused = false;
        SceneManager.LoadScene(Level);
        Time.timeScale = 1f;
    }
    public void StartScreen()
    {
        Paused = false;
        SceneManager.LoadScene(Home);
        Time.timeScale = 1f;
    }
    public void Resume()
    {
        Paused = false;
    }

    public void Retry()
    {
        manager.RespawnPlayers();   
    }

    public void ExitGame()
    {
        Application.Quit();
    }



}
