using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovementToWorld : MonoBehaviour
{
    public TMP_Text levelNumber;
    private float doLeva = 180f;
    private float doPrava = 0f;
    private float staticke = 90f;
    private float jakaRotace = 0;

    private bool rotuje = false;
    private bool pohybujeSe = true;

    public static int levelNum = 1;
    private Vector3 level1;
    private Vector3 level2;
    private Vector3 level3;

    public Transform level1T;
    public Transform level2T;
    public Transform level3T;
    public Transform player;

    private Vector3 target;
    public float speed = 5.0f;
    public float rotationSpeed = 180.0f; // Rychlost rotace ve stupních za sekundu

    // Start is called before the first frame update
    void Start()
    {
        level1 = level1T.position;
        level2 = level2T.position;
        level3 = level3T.position;
        LevelManager.World = 1;
        LevelManager.level = 1;
        player.transform.position = level1; 
        target = level1; 
        levelNum = 1;
        player.rotation = Quaternion.Euler(player.rotation.eulerAngles.x, 90f, player.rotation.eulerAngles.z);
        jakaRotace = staticke;
        
    }

    // Update is called once per frame
    void Update()
    {
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
            case 1: target = level1; levelNumber.text = "FLORALIS"; break;
            case 2: target = level2; levelNumber.text = "SAHARMUS"; break;
            case 3: target = level3; levelNumber.text = "ICSNOEWE"; break;
        }

        // Ovládání rotace doprava
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (levelNum < 3)
            {
                rotuje = true;
                jakaRotace = doPrava;
                levelNum++;
            }
        }

        // Ovládání rotace doleva
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (levelNum > 1)
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
