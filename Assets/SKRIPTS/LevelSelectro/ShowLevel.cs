using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowLevel : MonoBehaviour
{
    public TMP_Text world;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        world.text = LevelManager.World.ToString()+" - 0"+LevelManager.level.ToString();
        
    }
}
