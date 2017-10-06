using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreenUI : MonoBehaviour {

	public void StartButton()
    {
        SceneManager.LoadScene("MainScene");

    }
    public void Exit()
    {
        Application.Quit();

    }
}
