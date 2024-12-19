using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    }

    // Update is called once per frame
    void Update()
    {
        //InfoUpdate
        coins.text = HPSystem.coins.ToString() + "\n \n \n \n";
        lifes.text = HPSystem.health.ToString() + "\n \n";
        //Dodìlat text na shoot
        typeshoot.text = "\n XXX";
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
                    }
                }
                if (choosedPanel == 2)
                {
                    if (MagicLevel < 10)
                    {
                        HPSystem.coins -= HowMuchMinus;
                        MagicLevel++;
                    }
                }
                if (choosedPanel == 3)
                {
                    if (Shoot1.whichshoot == 2)
                    {
                        HPSystem.coins -= HowMuchMinus;
                        Shoot1.whichshoot = 1;
                    }
                }
                if (choosedPanel == 4)
                {
                    if (Shoot1.whichshoot == 1)
                    {
                        HPSystem.coins -= HowMuchMinus;
                        Shoot1.whichshoot = 2;
                    }
                }
            }
        }

        
    }
}
