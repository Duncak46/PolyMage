using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public Transform player; // Reference na hr��e (nastavte v inspektoru nebo najd�te v k�du)
    public float rotationSpeed = 5f; // Rychlost ot��en�
    public float attackRange = 2f; // Vzd�lenost pro �tok
   
    // jde dolu
    public bool GoDown = false;
    public bool StartCour = false;

    //Hrac
    int damage = 1;
    public GameObject playerNONE;
    public float forceMagnitude = 1f;
    public float verticalForceMultiplier = 2f; // P�idan� faktor pro zv��en� odhozen� po ose Z
    public Rigidbody playerRigidbody;

    void Update()
    {
        if (player != null)
        {
            // Zjist�me sm�r k hr��i
            Vector3 directionToPlayer = player.position - transform.position;
            directionToPlayer.y = 0; // Zajist�me, �e se ot��� pouze po ose Y

            float distanceToPlayer = directionToPlayer.magnitude;

            if (distanceToPlayer <= attackRange && GoDown == false)
            {
                if (StartCour == false)
                {
                    StartCour = true;
                    StartCoroutine(waitForGoDown());
                }
                // Pokud hr�� je v dosahu, ot���me se za n�m
                Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
                float targetY = targetRotation.eulerAngles.y + 90f;
                transform.rotation = Quaternion.Euler(0, Mathf.LerpAngle(transform.rotation.eulerAngles.y, targetY, rotationSpeed * Time.deltaTime), 180f);
            }
        }
    }
    IEnumerator waitForGoDown()
    {
        yield return new WaitForSeconds(0.5f);
        GoDown = true;

        yield return RotateToAngle(270f);

        // Rotace z 90 zp�t na 180
        yield return RotateToAngle(180f);
        GoDown = false;
        StartCour = false;
    }
    IEnumerator RotateToAngle(float targetAngle)
    {
        while (Mathf.Abs(transform.rotation.eulerAngles.z - targetAngle) > 0.1f)
        {
            float newAngle = Mathf.LerpAngle(transform.rotation.eulerAngles.z, targetAngle, Time.deltaTime * 2f);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, newAngle);
            yield return null;
        }

        // Nastaven� p�esn� hodnoty c�lov�ho �hlu
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, targetAngle);
    }

    void OnDrawGizmos()
    {
        // Nakresl�me radius pro �tok v editoru
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
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
