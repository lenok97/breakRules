using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance
    {
        get { return instance; }
        set { }
    }
    private static SaveManager instance = null;
    Canvas canvas;
    Button[] buttons;
    static string slotname= Convert.ToString(DateTime.Now), savename="Slot1";
    void Start()
    {
        canvas = GetComponent<Canvas>();
        buttons = GetComponentsInChildren<Button>();
        canvas.enabled = false;
        foreach (Button button in buttons)
            button.enabled = false;
    }
    public void MenuSlot()
    {
        foreach (Button button in buttons)
            button.enabled = true;
        canvas.enabled = !canvas.enabled;
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }
    public void SlotName()
    {
        string[] slots = {"Slot1","Slot2","Slot3","Slot4"};
        for (int i = 0; i < slots.Length; i++)                     
        {
            FileStream fs = new FileStream(slots[i]+".txt", FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);
            if (!sr.EndOfStream)
                buttons[i].GetComponentInChildren<Text>().text = sr.ReadLine();
            sr.Close();
            fs.Close();
        }

    }
    public void OK()
    {
        FileStream fs = new FileStream(savename + ".txt", FileMode.OpenOrCreate);
        StreamReader sr = new StreamReader(fs);
        if (!sr.EndOfStream)
        {
            sr.Close();
            fs.Close();
        }
        else
        {
            sr.Close();
            fs.Close();
            Saving(1);
        }
        MenuSlot();
    }

    public void Clear()
    {
        FileStream fs = new FileStream(savename + ".txt", FileMode.Truncate);
        fs.Close();
    }
    public void Slot1()
    {
        savename = "Slot1";
        slotname = Convert.ToString(DateTime.Now);
    }
    public void Slot2()
    {
        savename = "Slot2";
        slotname = Convert.ToString(DateTime.Now);
    }
    public void Slot3()
    {
        savename = "Slot3";
        slotname = Convert.ToString(DateTime.Now);
    }
    public void Slot4()
    {
        savename = "Slot4";
        slotname = Convert.ToString(DateTime.Now);
    }

    public static void Saving(int levelNumber)
    {
        string str;
        int filelevel=0;
        FileStream fs = new FileStream(savename + ".txt", FileMode.OpenOrCreate);
        StreamReader sr = new StreamReader(fs);
        while (!sr.EndOfStream)
        {
            str = sr.ReadLine();   
            if(str.Length==1)
                filelevel = Convert.ToInt32(sr.ReadLine());
            
        }
        if (levelNumber > filelevel)
        {
            sr.Close();
            fs.Close();
            Write_num(levelNumber);
        }
        else
        {
            sr.Close();
            fs.Close();
        }

    }
    static void Write_num(int levelNumber)
    {
        FileStream fs = new FileStream(savename + ".txt", FileMode.Truncate);
        StreamWriter sw = new StreamWriter(fs);
        sw.WriteLine(slotname);
        sw.Write(levelNumber);
        sw.Close();
        fs.Close();
    }

    public void Continue()
    {

        int number_lv = 1;
        FileStream fs = new FileStream(savename + ".txt", FileMode.OpenOrCreate);
        StreamReader sr = new StreamReader(fs);
        string str;
        while (!sr.EndOfStream)
        {
            str = sr.ReadLine();
            if (str.Length == 1)
                number_lv = Convert.ToInt32(str);
        }        
        sr.Close();
        fs.Close();
        SceneManager.LoadScene(number_lv);
    }

}