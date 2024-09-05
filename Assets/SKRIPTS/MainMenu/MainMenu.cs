using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using static UnityEngine.Rendering.DebugUI;
using System.Diagnostics;

public class MainMenu : MonoBehaviour
{
    //SAVES
    public static string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
    public static int save = 0;

    public TMP_Text save1;
    public TMP_Text save2;
    public TMP_Text save3;


    string Which = "Menu";
    public float speed = 5f;
    private bool zvetsit = false;
    public RectTransform panel;
    private static bool ok = false;
    //NAZEV
    public Transform Poly;
    public Transform Mage;
    public Transform endPosPoly;
    public Transform endPosMage;
    //Main Menu
    private Vector2 startPosMenu;
    private Vector2 endPosMenu = new Vector2(0,0);
    public RectTransform mainMenu;

    //Settings
    private Vector2 startPosSettings;
    private Vector2 endPosSettings = new Vector2(0, 0);
    public RectTransform Settings;

    //Select save
    private Vector2 startPosSave;
    private Vector2 endPosSave = new Vector2(0, 0);
    public RectTransform Save;

    void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        startPosSave = Save.anchoredPosition;
        startPosMenu = mainMenu.anchoredPosition;
        startPosSettings = Settings.anchoredPosition;
        if (ok)
        {
            panel.localScale = new Vector3(1.01f, 1.01f, 1.01f);
        }
        else
        {
            panel.localScale = Vector3.zero;
        }
        ok = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (zvetsit == false)
        {
            if (File.Exists(Path.Combine(currentDirectory, "data1.txt")) && int.Parse(File.ReadAllText(Path.Combine(currentDirectory, "data1.txt"))) > 0)
            {
                if (int.Parse(File.ReadAllText(Path.Combine(currentDirectory, "data1.txt"))) < 6 && int.Parse(File.ReadAllText(Path.Combine(currentDirectory, "data1.txt"))) > 0)
                {
                    save1.text = "1 - 0" + File.ReadAllText(Path.Combine(currentDirectory, "data1.txt"));
                }
                if (int.Parse(File.ReadAllText(Path.Combine(currentDirectory, "data1.txt"))) < 11 && int.Parse(File.ReadAllText(Path.Combine(currentDirectory, "data1.txt"))) > 5)
                {
                    int pomoc = int.Parse(File.ReadAllText(Path.Combine(currentDirectory, "data1.txt"))) - 5;
                    save1.text = "2 - 0" + pomoc;
                }
                if (int.Parse(File.ReadAllText(Path.Combine(currentDirectory, "data1.txt"))) < 16 && int.Parse(File.ReadAllText(Path.Combine(currentDirectory, "data1.txt"))) > 10)
                {
                    int pomoc = int.Parse(File.ReadAllText(Path.Combine(currentDirectory, "data1.txt"))) - 10;
                    save1.text = "3 - 0" + pomoc;
                }
            }
            if (File.Exists(Path.Combine(currentDirectory, "data2.txt")) && int.Parse(File.ReadAllText(Path.Combine(currentDirectory, "data2.txt"))) > 0)
            {
                if (int.Parse(File.ReadAllText(Path.Combine(currentDirectory, "data2.txt"))) < 6 && int.Parse(File.ReadAllText(Path.Combine(currentDirectory, "data2.txt"))) > 0)
                {
                    save2.text = "1 - 0" + File.ReadAllText(Path.Combine(currentDirectory, "data2.txt"));
                }
                if (int.Parse(File.ReadAllText(Path.Combine(currentDirectory, "data2.txt"))) < 11 && int.Parse(File.ReadAllText(Path.Combine(currentDirectory, "data2.txt"))) > 5)
                {
                    int pomoc = int.Parse(File.ReadAllText(Path.Combine(currentDirectory, "data2.txt"))) - 5;
                    save2.text = "2 - 0" + pomoc;
                }
                if (int.Parse(File.ReadAllText(Path.Combine(currentDirectory, "data2.txt"))) < 16 && int.Parse(File.ReadAllText(Path.Combine(currentDirectory, "data2.txt"))) > 10)
                {
                    int pomoc = int.Parse(File.ReadAllText(Path.Combine(currentDirectory, "data2.txt"))) - 10;
                    save2.text = "3 - 0" + pomoc;
                }
            }
            if (File.Exists(Path.Combine(currentDirectory, "data3.txt")) && int.Parse(File.ReadAllText(Path.Combine(currentDirectory, "data3.txt"))) > 0)
            {
                if (int.Parse(File.ReadAllText(Path.Combine(currentDirectory, "data3.txt"))) < 6 && int.Parse(File.ReadAllText(Path.Combine(currentDirectory, "data3.txt"))) > 0)
                {
                    save3.text = "1 - 0" + File.ReadAllText(Path.Combine(currentDirectory, "data3.txt"));
                }
                if (int.Parse(File.ReadAllText(Path.Combine(currentDirectory, "data3.txt"))) < 11 && int.Parse(File.ReadAllText(Path.Combine(currentDirectory, "data3.txt"))) > 5)
                {
                    int pomoc = int.Parse(File.ReadAllText(Path.Combine(currentDirectory, "data3.txt"))) - 5;
                    save3.text = "2 - 0" + pomoc;
                }
                if (int.Parse(File.ReadAllText(Path.Combine(currentDirectory, "data3.txt"))) < 16 && int.Parse(File.ReadAllText(Path.Combine(currentDirectory, "data3.txt"))) > 10)
                {
                    int pomoc = int.Parse(File.ReadAllText(Path.Combine(currentDirectory, "data3.txt"))) - 10;
                    save3.text = "3 - 0" + pomoc;
                }
            }
        }
        
        //ok
        if (zvetsit)
        {
            panel.localScale = Vector3.Lerp(panel.localScale, new Vector3(1.01f, 1.01f, 1.01f), speed * Time.deltaTime);
            if (panel.localScale == new Vector3(1.01f, 1.01f, 1.01f))
            {
                SceneManager.LoadScene("WORLDSELECT");
            }
        }
        if (!zvetsit)
        {
            panel.localScale = Vector3.Lerp(panel.localScale, Vector3.zero, speed * Time.deltaTime);
        }
        
        Poly.position = Vector3.MoveTowards(Poly.position, endPosPoly.position, speed*2 * Time.deltaTime);
        if (Poly.position == endPosPoly.position)
        {
            Mage.position = Vector3.MoveTowards(Mage.position, endPosMage.position, speed*2 * Time.deltaTime);
        }
        if (Poly.position == endPosPoly.position && Mage.position == endPosMage.position)
        {
            //MENU
            if (Which == "Menu" && Settings.anchoredPosition == startPosSettings && Save.anchoredPosition == startPosSave)
            {
                mainMenu.anchoredPosition = Vector2.MoveTowards(mainMenu.anchoredPosition, endPosMenu, speed * 150 * Time.deltaTime);
            }
            if (Which != "Menu")
            {
                mainMenu.anchoredPosition = Vector2.MoveTowards(mainMenu.anchoredPosition, startPosMenu, speed * 150 * Time.deltaTime);
            }

            //Settings
            if (Which == "Settings" && mainMenu.anchoredPosition == startPosMenu)
            {
                Settings.anchoredPosition = Vector2.MoveTowards(Settings.anchoredPosition, endPosSettings, speed * 150 * Time.deltaTime);
            }
            if (Which != "Settings")
            {
                Settings.anchoredPosition = Vector2.MoveTowards(Settings.anchoredPosition, startPosSettings, speed * 150 * Time.deltaTime);
            }

            //Save
            if (Which == "Save" && mainMenu.anchoredPosition == startPosMenu)
            {
                Save.anchoredPosition = Vector2.MoveTowards(Save.anchoredPosition, endPosSave, speed * 150 * Time.deltaTime);
            }
            if (Which != "Save")
            {
                Save.anchoredPosition = Vector2.MoveTowards(Save.anchoredPosition, startPosSave, speed * 150 * Time.deltaTime);
            }

            //Mackani tlacitka
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Which = "Menu";
            }
        }
    }
    public void makeSave()
    {
        Which = "Save";
    }
    public void makeSettings()
    {
        Which = "Settings";
    }
    public void Quit()
    {
        //Pøidat uložení
        Application.Quit();
    }
    public void Empty1()
    {
        //Uložení
        string filePath = Path.Combine(currentDirectory, "data1.txt");
        if (!File.Exists(filePath)) 
        {
            File.WriteAllText(filePath, "1");
        }
        LevelManager.unlockedLevel = int.Parse(File.ReadAllText(filePath));
        save = 1;
        zvetsit = true;
    }
    public void Empty2()
    {
        //Uložení
        string filePath = Path.Combine(currentDirectory, "data2.txt");
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "1");
        }
        LevelManager.unlockedLevel = int.Parse(File.ReadAllText(filePath));
        save = 2;
        zvetsit = true;
    }
    public void Empty3()
    {
        //Uložení
        string filePath = Path.Combine(currentDirectory, "data3.txt");
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "1");
        }
        LevelManager.unlockedLevel = int.Parse(File.ReadAllText(filePath));
        save = 3;
        zvetsit = true;
    }
}
