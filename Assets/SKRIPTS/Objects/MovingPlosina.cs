using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlosina : MonoBehaviour
{
    public Transform pointA; // Bod A
    public Transform pointB; // Bod B
    public float speed = 1f; // Rychlost pohybu
    private Vector3 target; // Aktuální cíl
    void Start()
    {
        target = pointB.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Pokud dosáhneme cíle, přepneme cíl na druhý bod
        if (transform.position == pointA.position)
        {
            target = pointB.position;
        }
        if (transform.position == pointB.position)
        {
            target = pointA.position;
        }
    }
}
