using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField]
    private int levelNumber;

    void OnTriggerEnter2D(Collider2D other)
    {
        SceneManager.LoadScene(levelNumber);
    }
}
