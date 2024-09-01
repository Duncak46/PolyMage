using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MovementToLevels : MonoBehaviour
{
    private float doLeva = 180f;
    private float doPrava = 0f;
    private float staticke = 90f;
    private float jakaRotace = 0;

    private bool rotuje = false;
    private bool pohybujeSe = true;

    public int levelNum = 1;
    private Vector3 level;
    private Vector3 level1;
    private Vector3 level2;
    private Vector3 level3;
    private Vector3 level4;
    private Vector3 level5;

    public Transform levelT;
    public Transform level1T;
    public Transform level2T;
    public Transform level3T;
    public Transform level4T;
    public Transform level5T;
    public Transform player;

    private Vector3 target;
    public float speed = 5.0f;
    public float rotationSpeed = 180.0f; // Rychlost rotace ve stupních za sekundu
    public RectTransform panel;
    private bool odchazi = false;

    
    private bool load;

    // Start is called before the first frame update
    void Start()
    {
        level = levelT.position;
        level1 = level1T.position;
        level2 = level2T.position;
        level3 = level3T.position;
        level4 = level4T.position;
        level5 = level5T.position;

        player.transform.position = level;
        jakaRotace = staticke;
        levelNum = LevelManager.level;
        if (levelNum == 1) { target = level1; }
        if (levelNum == 2) { target = level2; }
        if (levelNum == 3) { target = level3; }
        if (levelNum == 4) { target = level4; }
        if (levelNum == 5) { target = level5; }
    }

    // Update is called once per frame
    void Update()
    {
        if (load)
        {
            odchazi = true;
            panel.localScale = Vector3.Lerp(panel.localScale, new Vector3(1.01f, 1.01f, 1.01f), speed * Time.deltaTime);
            if (panel.localScale == new Vector3(1.01f, 1.01f, 1.01f))
            {
                if (LevelManager.World == 1)
                {
                    if (HPSystem.health <= 0)
                    {
                        HPSystem.health = 3;
                    }
                    SceneManager.LoadScene("Level1"+LevelManager.level.ToString());
                }
                if (LevelManager.World == 2)
                {
                    if (HPSystem.health <= 0)
                    {
                        HPSystem.health = 3;
                    }
                    SceneManager.LoadScene("Level2" + LevelManager.level.ToString());
                }
                if (LevelManager.World == 3)
                {
                    if (HPSystem.health <= 0)
                    {
                        HPSystem.health = 3;
                    }
                    SceneManager.LoadScene("Level3" + LevelManager.level.ToString());
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (levelNum)
            {
                case 1:
                    LevelManager.level = 1; load = true; break;
                case 2:
                    LevelManager.level = 2; load = true; break;
                case 3:
                    LevelManager.level = 3; load = true; break;
                case 4:
                    LevelManager.level = 4; load = true; break;
                case 5:
                    LevelManager.level = 5; load = true; break;
                    
            }
        }
        
        if (odchazi == false)
        {
            panel.localScale = Vector3.Lerp(panel.localScale, Vector3.zero, speed * Time.deltaTime);
        }
        if (player.transform.position == level && target == level)
        {
            odchazi = true;
            panel.localScale = Vector3.Lerp(panel.localScale, new Vector3(1.01f, 1.01f, 1.01f), speed * Time.deltaTime);
            if (panel.localScale == new Vector3(1.01f, 1.01f, 1.01f))
            {
                SceneManager.LoadScene("WORLDSELECT");
            }
        }
        // Pohyb hráèe, pokud se pohybuje
        if (pohybujeSe)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, target, speed * Time.deltaTime);

            // Pokud hráè dosáhl cílové pozice, zastaví se a zaène rotace do staticke
            if (player.transform.position == target)
            {
                pohybujeSe = false;
                rotuje = true;
                jakaRotace = staticke;
            }
        }

        // Nastavení cílové pozice podle èísla úrovnì
        switch (levelNum)
        {
            case 0: target = level; break;
            case 1: target = level1; break;
            case 2: target = level2; break;
            case 3: target = level3; break;
            case 4: target = level4; break;
            case 5: target = level5; break;
        }

        // Ovládání rotace doprava
        if (Input.GetKeyDown(KeyCode.D) && odchazi == false)
        {
            if (levelNum < 5)
            {
                rotuje = true;
                jakaRotace = doPrava;
                levelNum++;
            }
        }

        // Ovládání rotace doleva
        if (Input.GetKeyDown(KeyCode.A) && odchazi == false)
        {
            if (levelNum > 0)
            {
                rotuje = true;
                jakaRotace = doLeva;
                levelNum--;
            }
        }

        // Plynulá rotace hráèe
        if (rotuje)
        {
            Vector3 targetRotation = new Vector3(0, jakaRotace, 0); // Rotujeme pouze kolem osy Y
            Quaternion targetQuaternion = Quaternion.Euler(targetRotation);
            player.rotation = Quaternion.RotateTowards(player.rotation, targetQuaternion, rotationSpeed * Time.deltaTime);

            // Kontrola, zda je aktuální rotace blízko cílové rotace
            if (Quaternion.Angle(player.rotation, targetQuaternion) < 0.1f)
            {
                player.rotation = targetQuaternion; // Ujistìte se, že se nastaví pøesnì cílová rotace
                rotuje = false;

                // Jakmile rotace skonèí, zaène pohyb k cíli
                if (jakaRotace == doPrava || jakaRotace == doLeva)
                {
                    pohybujeSe = true;
                }
            }
        }
    }

}
