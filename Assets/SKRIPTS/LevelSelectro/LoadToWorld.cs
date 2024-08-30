using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadToWorld : MonoBehaviour
{
    private bool zvetsit = false;
    public RectTransform panel;
    private float speed = 5f;
    private string scene;
    // Update is called once per frame
    void Update()
    {
        if (zvetsit == false)
        {
            panel.localScale = Vector3.Lerp(panel.localScale, Vector3.zero, speed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Return) && zvetsit == false)
        {
            zvetsit = true;
            switch (MovementToWorld.levelNum)
            {
                case 1: scene = "LevelSelectorW1"; break;
                case 2: scene = "LevelSelectorW2"; break;
                case 3: scene = "LevelSelectorW3"; break;
            }
        }
        if (zvetsit)
        {
            panel.localScale = Vector3.Lerp(panel.localScale, new Vector3(1.01f, 1.01f, 1.01f), speed * Time.deltaTime);
            if (panel.localScale == new Vector3(1.01f, 1.01f, 1.01f))
            {
                SceneManager.LoadScene(scene);
            }
        }
        
    }
    
}
