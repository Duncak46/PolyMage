using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinSpin : MonoBehaviour
{
    public float rotationSpeed = 30f; // Rychlost rotace ve stupn�ch za sekundu
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
    string tagToIgnore3 = "Coin";// N�zev tagu, kter� chceme ignorovat




    // Z�sk�n� Collider komponenty


    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag(tagToIgnore))
        {
            // Ignorov�n� kolize mezi t�mito dv�ma kolidery
            Physics.IgnoreCollision(myCollider, other.collider);
        }
        if (other.gameObject.CompareTag(tagToIgnore2))
        {
            // Ignorov�n� kolize mezi t�mito dv�ma kolidery
            Physics.IgnoreCollision(myCollider, other.collider);
        }
        if (other.gameObject.CompareTag(tagToIgnore3))
        {
            // Ignorov�n� kolize mezi t�mito dv�ma kolidery
            Physics.IgnoreCollision(myCollider, other.collider);
        }
        if (other.gameObject.CompareTag("Player"))
        {
            HPSystem pridej = new HPSystem();
            pridej.AddCoin(HowMuch);
            Destroy(gameObject);
        }
    }
    

}
