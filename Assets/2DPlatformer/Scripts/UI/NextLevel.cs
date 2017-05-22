using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class NextLevel : MonoBehaviour
{
    [SerializeField]
    private int levelNumber;

    void OnTriggerEnter2D(Collider2D other)
    {
        SceneManager.LoadScene(levelNumber);
        SaveManager.Saving(levelNumber);  
    }
   
}
