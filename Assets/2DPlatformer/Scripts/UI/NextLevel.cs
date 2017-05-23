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
        FileStream fs = new FileStream("saved.txt",FileMode.OpenOrCreate);
        StreamReader sr = new StreamReader(fs);
        if (!sr.EndOfStream)
        {
            int filelevel = Convert.ToInt32(sr.ReadLine());
            sr.Close();
            fs.Close();
            if (levelNumber > filelevel)
                Write_num();
        }
        else
        {
            sr.Close();
            fs.Close();
            Write_num();
        }    
    }
    void Write_num()
    {
        FileStream fs = new FileStream("saved.txt", FileMode.Truncate);
        StreamWriter sw = new StreamWriter(fs);
        sw.Write(levelNumber);
        sw.Close();
        fs.Close();
    }
}
