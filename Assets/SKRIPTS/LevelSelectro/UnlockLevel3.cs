using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockLevel3 : MonoBehaviour
{
    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
    public GameObject level4;
    public GameObject level5;


    // Update is called once per frame
    void Update()
    {
        if (LevelManager.unlockedLevel >= 11)
        {
            level1.SetActive(false);
        }
        if (LevelManager.unlockedLevel >= 12)
        {
            level2.SetActive(false);
        }
        if (LevelManager.unlockedLevel >= 13)
        {
            level3.SetActive(false);
        }
        if (LevelManager.unlockedLevel >= 14)
        {
            level4.SetActive(false);
        }
        if (LevelManager.unlockedLevel >= 15)
        {
            level5.SetActive(false);
        }
    }
}
