using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinSpin : MonoBehaviour
{
    public float rotationSpeed = 30f; // Rychlost rotace ve stupn�ch za sekundu
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

    string tagToIgnore = "Fire"; // N�zev tagu, kter� chceme ignorovat

    

    
        // Z�sk�n� Collider komponenty
        
    

    private void OnTriggerEnter(Collider other)
    {
        // Kontrola, zda koliduj�c� objekt m� specifikovan� tag
        if (other.CompareTag(tagToIgnore))
        {
            // Ignorov�n� kolize mezi t�mito dv�ma kolidery
            Physics.IgnoreCollision(myCollider, other);
            Debug.Log("Trigger: Kolize ignorov�na s objektem s tagem: " + tagToIgnore);
        }
    }

}
