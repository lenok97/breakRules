using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void LoadLevel(int number_lv)
    {
        SceneManager.LoadScene(number_lv);
    }
}
