using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class Continue : MonoBehaviour
{
    public void LoadLevel()
    {
        int number_lv;
        FileStream fs = new FileStream("saved.txt", FileMode.OpenOrCreate);
        StreamReader sr = new StreamReader(fs);
        if (!sr.EndOfStream)
        {
            number_lv = Convert.ToInt32(sr.ReadLine());
        }
        else number_lv = 1;
        sr.Close();
        fs.Close();
        SceneManager.LoadScene(number_lv);
    }
}
