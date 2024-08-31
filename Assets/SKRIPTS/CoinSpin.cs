using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinSpin : MonoBehaviour
{
    public float rotationSpeed = 30f; // Rychlost rotace ve stupních za sekundu
    private GameObject playerNONE;
    private Collider myCollider;
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

    string tagToIgnore = "Fire"; // Název tagu, který chceme ignorovat

    

    
        // Získání Collider komponenty
        
    

    private void OnTriggerEnter(Collider other)
    {
        // Kontrola, zda kolidující objekt má specifikovaný tag
        if (other.CompareTag(tagToIgnore))
        {
            // Ignorování kolize mezi tìmito dvìma kolidery
            Physics.IgnoreCollision(myCollider, other);
            Debug.Log("Trigger: Kolize ignorována s objektem s tagem: " + tagToIgnore);
        }
    }

}
