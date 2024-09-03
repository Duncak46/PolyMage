using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockLevel1 : MonoBehaviour
{
    public GameObject level2;
    public GameObject level3;
    public GameObject level4;
    public GameObject level5;


    // Update is called once per frame
    void Update()
    {
        
        if (LevelManager.unlockedLevel >= 2)
        {
            level2.SetActive(false);
        }
        if (LevelManager.unlockedLevel >= 3)
        {
            level3.SetActive(false);
        }
        if (LevelManager.unlockedLevel >= 4)
        {
            level4.SetActive(false);
        }
        if (LevelManager.unlockedLevel >= 5)
        {
            level5.SetActive(false);
        }
    }
}
