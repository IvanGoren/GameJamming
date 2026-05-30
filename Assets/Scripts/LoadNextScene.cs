using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class LoadNextScene : MonoBehaviour
{
    public String nextScene;

    public void LoadGame()
    {
        SceneManager.LoadScene(nextScene);
    }

}