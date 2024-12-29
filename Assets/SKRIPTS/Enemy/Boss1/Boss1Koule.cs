using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Boss1Koule : MonoBehaviour
{
    public Transform Warning;
    private Vector3 warning;
    private float speed = 5f;
    public GameObject prefab;
    private bool moving = false;

    int damage = 1;
    private GameObject playerNONE;
    public float forceMagnitude = 1f;
    public float verticalForceMultiplier = 2f; // Pøidaný faktor pro zvýšení odhození po ose Z
    GameObject existingObject;
    private Rigidbody playerRigidbody;
    void Start()
    {
        playerNONE = GameObject.Find("MageBro");
        existingObject = GameObject.Find("Hrac");
        playerRigidbody = existingObject.GetComponent<Rigidbody>();
        StartCoroutine(StartLol());
        warning = Warning.position;
    }
    void Update()
    {
        if (moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, warning, speed * Time.deltaTime);
        }
        if (transform.position == warning)
        {
            moving = false;
        }
    }
    IEnumerator StartLol()
    {
        yield return new WaitForSeconds(1.5f);
        moving = true;
        yield return new WaitForSeconds(2f);
        Destroy(prefab);
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
