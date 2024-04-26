using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private int _gameBuildSceneId;

    public void Play()
    {
        SceneManager.LoadScene(_gameBuildSceneId);
    }

    public void Exit()
    {
        Application.Quit();
    }
}