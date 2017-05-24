using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class DieMenu : MonoBehaviour
{

    Canvas canvas;
    Button[] buttons;

    void Start()
    {
        EventManager.Instance.AddListener(EVENT_TYPE.UNIT_DIE, this.DieConvas);
        canvas = GetComponent<Canvas>();
        buttons = GetComponentsInChildren<Button>();
        canvas.enabled = false;
        foreach (Button button in buttons)
            button.enabled = false;
    }

    public void DieConvas(EVENT_TYPE Event_Type, Component sender, object param)
    {
        foreach (Button button in buttons)
            button.enabled = true;
        canvas.enabled = true;
        Time.timeScale = 0 ;
    }

    public void Awake()
    {
        Time.timeScale = 1;
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
