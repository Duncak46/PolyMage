using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinSpin : MonoBehaviour
{
    public float rotationSpeed = 30f; // Rychlost rotace ve stupních za sekundu
    private GameObject playerNONE;
    private void Start()
    {
        playerNONE = GameObject.Find("Hrac");
    }
    void Update()
    {
        // Rotace kolem osy Y
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
    
}
