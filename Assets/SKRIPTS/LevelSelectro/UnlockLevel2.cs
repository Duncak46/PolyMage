using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockLevel2 : MonoBehaviour
{
    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
    public GameObject level4;
    public GameObject level5;
    

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.unlockedLevel >= 6)
        {
            level1.SetActive(false);
        }
        if (LevelManager.unlockedLevel >= 7)
        {
            level2.SetActive(false);
        }
        if (LevelManager.unlockedLevel >= 8)
        {
            level3.SetActive(false);
        }
        if (LevelManager.unlockedLevel >= 9)
        {
            level4.SetActive(false);
        }
        if (LevelManager.unlockedLevel >= 10)
        {
            level5.SetActive(false);
        }
    }
}
