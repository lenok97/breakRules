using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.sceneCount);
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }
}
