using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class CoinSpin : MonoBehaviour
{
    public float rotationSpeed = 30f; // Rychlost rotace ve stupních za sekundu
    private GameObject playerNONE;
    private Collider myCollider;
    public int HowMuch;
    private void Start()
    {
         
        playerNONE = GameObject.Find("Hrac");
        myCollider = GetComponent<Collider>();
    }
    void Update()
    {
        // Rotace kolem osy Y
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    string tagToIgnore = "Fire";
    string tagToIgnore2 = "Enemy";
    string tagToIgnore3 = "Coin";// Název tagu, který chceme ignorovat




    // Získání Collider komponenty


    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag(tagToIgnore))
        {
            // Ignorování kolize mezi tìmito dvìma kolidery
            Physics.IgnoreCollision(myCollider, other.collider);
        }
        if (other.gameObject.CompareTag(tagToIgnore2))
        {
            // Ignorování kolize mezi tìmito dvìma kolidery
            Physics.IgnoreCollision(myCollider, other.collider);
        }
        if (other.gameObject.CompareTag(tagToIgnore3))
        {
            // Ignorování kolize mezi tìmito dvìma kolidery
            Physics.IgnoreCollision(myCollider, other.collider);
        }
        if (other.gameObject.CompareTag("Player"))
        {
            HPSystem pridej = new HPSystem();
            pridej.AddCoin(HowMuch);
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
            Destroy(gameObject);
        }
    }
    

}
