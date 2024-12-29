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
    public float verticalForceMultiplier = 2f; // P�idan� faktor pro zv��en� odhozen� po ose Z
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
