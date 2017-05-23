using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseManager : MonoBehaviour
{

    Canvas canvas;
    Button[] buttons;

    void Start()
    {
        canvas = GetComponent<Canvas>();
        buttons = GetComponentsInChildren<Button>();
        canvas.enabled = false;
        foreach (Button button in buttons)
            button.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        foreach (Button button in buttons)
            button.enabled = true;
        canvas.enabled = !canvas.enabled;
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}