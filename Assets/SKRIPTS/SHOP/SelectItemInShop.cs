using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class SelectItemInShop : MonoBehaviour
{
    [SerializeField] private TMP_Text coins;
    [SerializeField] private TMP_Text lifes;
    [SerializeField] private TMP_Text typeshoot;
    [SerializeField] private TMP_Text magic;

    [SerializeField] private RectTransform panel1;
    [SerializeField] private RectTransform panel2;
    [SerializeField] private RectTransform panel3;
    [SerializeField] private RectTransform panel4;
    [SerializeField] private RectTransform choosingPanel;
    public int choosedPanel;
    public static int HowMuchMinus;

    public static int MagicLevel = 1;

    void Start()
    {
        choosingPanel.position = panel1.position;
        choosedPanel = 1;
        if (Shoot1.whichshoot == 1)
        {
            typeshoot.text = "\nSingle";
        }
        else if (Shoot1.whichshoot == 2)
        {
            typeshoot.text = "\nDouble";
        }
    }

    // Update is called once per frame
    void Update()
    {
        //InfoUpdate
        coins.text = HPSystem.coins.ToString() + "\n \n \n \n";
        lifes.text = HPSystem.health.ToString() + "\n \n";
        //Dodìlat text na shoot
        magic.text = "\n \n \n" + MagicLevel.ToString();
        //Choosing
        if (choosingPanel.position == panel1.position)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                choosingPanel.position = panel3.position;
                choosedPanel = 3;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                choosingPanel.position = panel2.position;
                choosedPanel = 2;
            }
        }

        if (choosingPanel.position == panel2.position)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                choosingPanel.position = panel4.position;
                choosedPanel = 4;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                choosingPanel.position = panel1.position;
                choosedPanel = 1;
            }
        }

        if (choosingPanel.position == panel3.position)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                choosingPanel.position = panel1.position;
                choosedPanel = 1;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                choosingPanel.position = panel4.position;
                choosedPanel = 4;
            }
        }

        if (choosingPanel.position == panel4.position)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                choosingPanel.position = panel2.position;
                choosedPanel = 2;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                choosingPanel.position = panel3.position;
                choosedPanel = 3;
            }
        }

        switch (choosedPanel)
        {
            case 1:
                HowMuchMinus = 50;
                break;
            case 2:
                HowMuchMinus = 100;
                break;
            case 3:
                HowMuchMinus = 200;
                break;
            case 4:
                HowMuchMinus = 400;
                break;
            default:
                break;
        }

        //Buy
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (HowMuchMinus <= HPSystem.coins)
            {
                if (choosedPanel == 1)
                {
                    if (HPSystem.health < 5)
                    {
                        HPSystem.coins -= HowMuchMinus;
                        HPSystem.health++;
                        if (MainMenu.save == 1)
                        {
                            string filePathCOINS = Path.Combine(MainMenu.currentDirectory, "coins1.txt");
                            File.WriteAllText(filePathCOINS, HPSystem.coins.ToString());
                        }
                        if (MainMenu.save == 2)
                        {
                            string filePathCOINS = Path.Combine(MainMenu.currentDirectory, "coins2.txt");
                            File.WriteAllText(filePathCOINS, HPSystem.coins.ToString());
                        }
                        if (MainMenu.save == 3)
                        {
                            string filePathCOINS = Path.Combine(MainMenu.currentDirectory, "coins3.txt");
                            File.WriteAllText(filePathCOINS, HPSystem.coins.ToString());
                        }
                    }
                }
                if (choosedPanel == 2)
                {
                    if (MagicLevel < 10)
                    {
                        HPSystem.coins -= HowMuchMinus;
                        MagicLevel++;
                        if (MainMenu.save == 1)
                        {
                            //Coiny
                            string filePathCOINS = Path.Combine(MainMenu.currentDirectory, "coins1.txt");
                            File.WriteAllText(filePathCOINS, HPSystem.coins.ToString());
                            //Mana
                            string filePathMANA = Path.Combine(MainMenu.currentDirectory, "mana1.txt");
                            File.WriteAllText(filePathMANA, MagicLevel.ToString());
                        }
                        if (MainMenu.save == 2)
                        {
                            //Coiny
                            string filePathCOINS = Path.Combine(MainMenu.currentDirectory, "coins2.txt");
                            File.WriteAllText(filePathCOINS, HPSystem.coins.ToString());
                            //Mana
                            string filePathMANA = Path.Combine(MainMenu.currentDirectory, "mana2.txt");
                            File.WriteAllText(filePathMANA, MagicLevel.ToString());
                        }
                        if (MainMenu.save == 3)
                        {
                            //Coiny
                            string filePathCOINS = Path.Combine(MainMenu.currentDirectory, "coins3.txt");
                            File.WriteAllText(filePathCOINS, HPSystem.coins.ToString());
                            //Mana
                            string filePathMANA = Path.Combine(MainMenu.currentDirectory, "mana3.txt");
                            File.WriteAllText(filePathMANA, MagicLevel.ToString());
                        }
                    }
                }
                if (choosedPanel == 3)
                {
                    if (Shoot1.whichshoot == 2)
                    {
                        HPSystem.coins -= HowMuchMinus;
                        Shoot1.whichshoot = 1;
                        typeshoot.text = "\nSingleShoot";
                        if (MainMenu.save == 1)
                        {
                            //Coiny
                            string filePathCOINS = Path.Combine(MainMenu.currentDirectory, "coins1.txt");
                            File.WriteAllText(filePathCOINS, HPSystem.coins.ToString());
                            //Mana
                            string filePathSHOOT = Path.Combine(MainMenu.currentDirectory, "shoot1.txt");
                            File.WriteAllText(filePathSHOOT, "1");
                        }
                        if (MainMenu.save == 2)
                        {
                            //Coiny
                            string filePathCOINS = Path.Combine(MainMenu.currentDirectory, "coins2.txt");
                            File.WriteAllText(filePathCOINS, HPSystem.coins.ToString());
                            //Mana
                            string filePathSHOOT = Path.Combine(MainMenu.currentDirectory, "shoot2.txt");
                            File.WriteAllText(filePathSHOOT, "1");
                        }
                        if (MainMenu.save == 3)
                        {
                            //Coiny
                            string filePathCOINS = Path.Combine(MainMenu.currentDirectory, "coins3.txt");
                            File.WriteAllText(filePathCOINS, HPSystem.coins.ToString());
                            //Mana
                            string filePathSHOOT = Path.Combine(MainMenu.currentDirectory, "shoot3.txt");
                            File.WriteAllText(filePathSHOOT, "1");
                        }
                    }
                }
                if (choosedPanel == 4)
                {
                    if (Shoot1.whichshoot == 1)
                    {
                        HPSystem.coins -= HowMuchMinus;
                        Shoot1.whichshoot = 2;
                        typeshoot.text = "\nDoubleShoot";
                        if (MainMenu.save == 1)
                        {
                            //Coiny
                            string filePathCOINS = Path.Combine(MainMenu.currentDirectory, "coins1.txt");
                            File.WriteAllText(filePathCOINS, HPSystem.coins.ToString());
                            //Mana
                            string filePathSHOOT = Path.Combine(MainMenu.currentDirectory, "shoot1.txt");
                            File.WriteAllText(filePathSHOOT, "2");
                        }
                        if (MainMenu.save == 2)
                        {
                            //Coiny
                            string filePathCOINS = Path.Combine(MainMenu.currentDirectory, "coins2.txt");
                            File.WriteAllText(filePathCOINS, HPSystem.coins.ToString());
                            //Mana
                            string filePathSHOOT = Path.Combine(MainMenu.currentDirectory, "shoot2.txt");
                            File.WriteAllText(filePathSHOOT, "2");
                        }
                        if (MainMenu.save == 3)
                        {
                            //Coiny
                            string filePathCOINS = Path.Combine(MainMenu.currentDirectory, "coins3.txt");
                            File.WriteAllText(filePathCOINS, HPSystem.coins.ToString());
                            //Mana
                            string filePathSHOOT = Path.Combine(MainMenu.currentDirectory, "shoot3.txt");
                            File.WriteAllText(filePathSHOOT, "2");
                        }
                    }
                }
            }
        }

        
    }
}
