using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RandomMovement1 : MonoBehaviour
{
    int damage = 1;
    public GameObject playerNONE;
    public float forceMagnitude = 1f;
    public float verticalForceMultiplier = 2f; // Pøidaný faktor pro zvýšení odhození po ose Z
    public Rigidbody playerRigidbody;

    public float moveSpeed = 2.0f;
    public float waitTime = 2.0f;
    public Vector2 boundaryMin; // Minimální x a z hranice
    public Vector2 boundaryMax; // Maximální x a z hranice
    public float rotationSpeed = 5.0f;
    public bool canMove = true;
    public float jumpDistance = 2.0f; // Hodnota vzdálenosti pro skákání
    public float jumpHeight = 2.0f; // Výška skoku

    private Vector3 targetPosition;

    private float vyska;

    void Start()
    {
        vyska = transform.position.y;
        SetNewTargetPosition();
        StartCoroutine(MoveToTarget());
    }

    void SetNewTargetPosition()
    {
        if (canMove)
        {
            float x = Random.Range(boundaryMin.x, boundaryMax.x);
            float z = Random.Range(boundaryMin.y, boundaryMax.y);
            targetPosition = new Vector3(x, transform.position.y, z);
        }
    }

    IEnumerator MoveToTarget()
    {
        while (canMove)
        {
            // Nastavení èasovaèe
            float elapsedTime = 0f;

            // Kontrola vzdálenosti k cílovému bodu
            float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

            // Raycast kontrola mezi aktuální pozicí a cílovou pozicí
            bool isPathClear = true;
            int numChecks = 100; // Poèet kontrolních bodù na cestì
            float step = distanceToTarget / numChecks;

            for (int i = 1; i <= numChecks; i++)
            {
                Vector3 point = Vector3.Lerp(transform.position, targetPosition, i / (float)numChecks);
                RaycastHit hit;
                Debug.DrawRay(point, Vector3.up * 10, Color.blue); // Debugování cesty, úhel k detekci výšky
                if (Physics.Raycast(point, Vector3.down, out hit, 10))
                {
                    if (hit.collider.tag == "Wall")
                    {
                        Debug.Log("Pøekážka detekována: " + hit.collider.name);
                        isPathClear = false;
                        break;
                    }
                }
            }

            if (!isPathClear)
            {
                // Pokud se detekuje pøekážka, zvolíme jiný cíl
                SetNewTargetPosition();
                yield return new WaitForSeconds(waitTime);
                continue; // Pøeskoèí aktuální cyklus a zaène nový
            }

            if (distanceToTarget > jumpDistance)
            {
                // Otoèení k cílové pozici pøed skokem
                yield return StartCoroutine(RotateTowards(targetPosition));
                // Skok k cílové pozici
                yield return StartCoroutine(JumpToTarget(targetPosition));
            }
            else
            {
                // Pokraèování s otáèením a pohybem
                Vector3 direction = (targetPosition - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                while (Quaternion.Angle(transform.rotation, lookRotation) > 0.1f)
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
                    yield return null;
                }

                // Pohyb k cílové pozici
                while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                    elapsedTime += Time.deltaTime; // Zvyšování èasovaèe

                    // Pokud trvá pohyb pøíliš dlouho, restartuj pohyb
                    if (elapsedTime > 0.8f)
                    {
                        Debug.Log("Pohyb trvá pøíliš dlouho, restartování korutiny.");
                        SetNewTargetPosition();
                        break; // Ukonèí aktuální cyklus a spustí nový
                    }

                    yield return null;
                }
            }

            // Èekání na další pohyb
            yield return new WaitForSeconds(waitTime);
            SetNewTargetPosition();
        }
    }


    private void Update()
    {
        if (boundaryMin.x > transform.position.x)
        {
            transform.position = new Vector3(boundaryMin.x, vyska, transform.position.z);
        }
        if (boundaryMin.y > transform.position.z)
        {
            transform.position = new Vector3(transform.position.x, vyska, boundaryMin.y);
        }

        if (boundaryMax.x < transform.position.x)
        {
            transform.position = new Vector3(boundaryMax.x, vyska, transform.position.z);
        }
        if (boundaryMax.y < transform.position.z)
        {
            transform.position = new Vector3(transform.position.x, vyska, boundaryMax.y);
        }
        Debug.DrawLine(transform.position, targetPosition, Color.green);
    }
    IEnumerator RotateTowards(Vector3 target)
    {
        // Urèení smìru k cílové pozici
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        // Otáèení k cíli
        while (Quaternion.Angle(transform.rotation, lookRotation) > 0.1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator JumpToTarget(Vector3 target)
    {
        Vector3 startPos = transform.position;
        Vector3 peakPos = (startPos + target) / 2.0f; // Støední bod na zemi
        peakPos.y += jumpHeight; // Pøidáme výšku ke støedu, aby objekt skoèil

        float jumpProgress = 0;
        float jumpDuration = 1.0f; // Délka skoku

        while (jumpProgress < 1)
        {
            jumpProgress += Time.deltaTime / jumpDuration;

            // Výpoèet pozice pøi skoku
            Vector3 currentPos = Vector3.Lerp(startPos, peakPos, jumpProgress);
            currentPos = Vector3.Lerp(currentPos, target, jumpProgress);

            // Ruèní nastavení oblouku ve vzduchu
            float heightFactor = 4 * jumpProgress * (1 - jumpProgress); // Parabolický pohyb
            currentPos.y = Mathf.Lerp(startPos.y, peakPos.y, heightFactor);

            transform.position = currentPos;

            yield return null;
        }

        transform.position = target; // Ujistìte se, že skonèíte pøesnì v cíli
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


