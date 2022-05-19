using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Game_Button : MonoBehaviour
{
    public void Change_Scene()
    {
        SceneManager.LoadScene(sceneName: "Game_Scene");
    }

    public void Exit_Game()
    {
        Application.Quit();
    }
}
