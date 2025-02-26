using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEnemy : MonoBehaviour
{
    public int HP;
    public float deathForce = 10.0f; // Síla vystøelení po ose Y
    public float rotationSpeed = 180.0f; // Rychlost rotace kolem osy Y
    public float destroyDelay = 2.0f; // Zpoždìní pøed znièením objektu

    public GameObject original;
    public GameObject blink;
    public GameObject coin;

    private Rigidbody rb;
    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject shoot = collision.gameObject;
        if (shoot.CompareTag("Fire"))
        {
            Destroy(shoot);
            StartCoroutine(Blink());
            HP--;
            Debug.Log(HP);
            if (HP <= 0 && !isDead)
            {
                Destroy(gameObject.GetComponent<RandomMovement1>());
                Destroy(gameObject.GetComponent<RandomMovementVosa>());
                Destroy(gameObject.GetComponent<BoxCollider>());
                StartDeath();
            }
        }
    }
    IEnumerator Blink()
    {
        original.SetActive(false);
        blink.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        original.SetActive(true);
        blink.SetActive(false);
    }
    private void StartDeath()
    {
        RandomMovement1 movementScript = gameObject.GetComponent<RandomMovement1>();
        if (movementScript != null)
        {
            movementScript.canMove = false;
        }

        isDead = true;

        // Zastavit veškerý aktuální pohyb
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Vystøelení po ose Y s definovanou silou
        Vector3 deathDirection = Vector3.up * deathForce;
        rb.AddForce(deathDirection, ForceMode.Impulse);

        // Rotace kolem osy Y
        Vector3 rotationAxis = new Vector3(0, 1, 0);
        rb.AddTorque(rotationAxis * rotationSpeed, ForceMode.VelocityChange);

        // Znièení objektu po urèitém èase
        StartCoroutine(SpawnCoin());
        Destroy(gameObject, destroyDelay);
        
    }

    IEnumerator SpawnCoin()
    {
        yield return new WaitForSeconds(destroyDelay-0.1f);
        Instantiate(coin, transform.position, transform.rotation);
    }
}
