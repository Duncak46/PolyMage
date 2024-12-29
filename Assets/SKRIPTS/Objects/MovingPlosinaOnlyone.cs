using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlosinaOnlyone : MonoBehaviour
{
    public Transform pointA; // Bod A
    public Transform pointB; // Bod B
    public float speed = 1f; // Rychlost pohybu
    private Vector3 target; // Aktuální cíl
    private bool hejbatSe = false;
    void Start()
    {
        transform.position = pointA.position;
        target = pointB.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (hejbatSe == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject player = collision.gameObject;

        if (player.CompareTag("Player"))
        {
            hejbatSe=true;
        }
    }
}
