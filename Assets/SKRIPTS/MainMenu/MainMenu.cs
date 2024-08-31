using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class MainMenu : MonoBehaviour
{
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
        zvetsit = true;
    }
    public void Empty2()
    {
        //Uložení
        zvetsit = true;
    }
    public void Empty3()
    {
        //Uložení
        zvetsit = true;
    }
}
