using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    public Transform pointA; // Bod A
    public Transform pointB; // Bod B
    public float speed = 2f; // Rychlost pohybu

    private Vector3 target; // Aktuální cíl
    private float currentRotationX; // Aktuální rotace na ose X
    private float rotationSpeed = 120f;

    int damage = 1;
    public GameObject playerNONE;
    public float forceMagnitude = 1f;
    public float verticalForceMultiplier = 2f; // Pøidaný faktor pro zvýšení odhození po ose Z
    public Rigidbody playerRigidbody;
    void Start()
    {
        // Nastavíme poèáteèní cíl na bod A
        target = pointB.position;
    }

    void Update()
    {
        // Pohyb GameObjectu smìrem k cíli
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (target == pointB.position)
        {
            transform.Rotate(Vector3.left, rotationSpeed * Time.deltaTime);
        }
        if (target == pointA.position)
        {
            transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
        }
        
        // Pokud dosáhneme cíle, pøepneme cíl na druhý bod
        if (transform.position == pointA.position)
        {
            target = pointB.position;
        }
        if (transform.position == pointB.position)
        {
            target = pointA.position;
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject player = collision.gameObject;

        if (player.CompareTag("Player"))
        {
            HPSystem playerHealth = playerNONE.GetComponent<HPSystem>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            Vector3 contactPoint = collision.GetContact(0).point;
            Vector3 normalDirection = collision.GetContact(0).normal;

            if (playerRigidbody != null)
            {
                Vector3 forceDirection;

                // Kontrola, zda kolize pøišla shora
                if (normalDirection.y < -0.5f) // Detekce, jestli normála smìøuje pøevážnì nahoru
                {
                    // Odhození po ose Z (stranou) místo osy Y, aby hráè nespadl zpìt na objekt
                    forceDirection = new Vector3(0, 0, Mathf.Sign(contactPoint.z - transform.position.z) * verticalForceMultiplier);
                }
                else
                {
                    // Standardní odhození ve smìru normály
                    forceDirection = normalDirection * -1;
                }

                playerRigidbody.AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);

                // Spuštìní probliknutí
            }
        }
    }
}
