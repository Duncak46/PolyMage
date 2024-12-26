using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    public Transform pointA; // Bod A
    public Transform pointB; // Bod B
    public float speed = 2f; // Rychlost pohybu

    private Vector3 target; // Aktu�ln� c�l
    private float currentRotationX; // Aktu�ln� rotace na ose X
    private float rotationSpeed = 120f;

    int damage = 1;
    public GameObject playerNONE;
    public float forceMagnitude = 1f;
    public float verticalForceMultiplier = 2f; // P�idan� faktor pro zv��en� odhozen� po ose Z
    public Rigidbody playerRigidbody;
    void Start()
    {
        // Nastav�me po��te�n� c�l na bod A
        target = pointB.position;
    }

    void Update()
    {
        // Pohyb GameObjectu sm�rem k c�li
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (target == pointB.position)
        {
            transform.Rotate(Vector3.left, rotationSpeed * Time.deltaTime);
        }
        if (target == pointA.position)
        {
            transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
        }
        
        // Pokud dos�hneme c�le, p�epneme c�l na druh� bod
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

                // Kontrola, zda kolize p�i�la shora
                if (normalDirection.y < -0.5f) // Detekce, jestli norm�la sm��uje p�ev�n� nahoru
                {
                    // Odhozen� po ose Z (stranou) m�sto osy Y, aby hr�� nespadl zp�t na objekt
                    forceDirection = new Vector3(0, 0, Mathf.Sign(contactPoint.z - transform.position.z) * verticalForceMultiplier);
                }
                else
                {
                    // Standardn� odhozen� ve sm�ru norm�ly
                    forceDirection = normalDirection * -1;
                }

                playerRigidbody.AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);

                // Spu�t�n� probliknut�
            }
        }
    }
}
