using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RandomMovement1 : MonoBehaviour
{
    int damage = 1;
    public GameObject playerNONE;
    public float forceMagnitude = 1f;
    public float verticalForceMultiplier = 2f; // P�idan� faktor pro zv��en� odhozen� po ose Z
    public Rigidbody playerRigidbody;

    public float moveSpeed = 2.0f;
    public float waitTime = 2.0f;
    public Vector2 boundaryMin; // Minim�ln� x a z hranice
    public Vector2 boundaryMax; // Maxim�ln� x a z hranice
    public float rotationSpeed = 5.0f;
    public bool canMove = true;
    public float jumpDistance = 2.0f; // Hodnota vzd�lenosti pro sk�k�n�
    public float jumpHeight = 2.0f; // V��ka skoku

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
            // Nastaven� �asova�e
            float elapsedTime = 0f;

            // Kontrola vzd�lenosti k c�lov�mu bodu
            float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

            // Raycast kontrola mezi aktu�ln� pozic� a c�lovou pozic�
            bool isPathClear = true;
            int numChecks = 100; // Po�et kontroln�ch bod� na cest�
            float step = distanceToTarget / numChecks;

            for (int i = 1; i <= numChecks; i++)
            {
                Vector3 point = Vector3.Lerp(transform.position, targetPosition, i / (float)numChecks);
                RaycastHit hit;
                Debug.DrawRay(point, Vector3.up * 10, Color.blue); // Debugov�n� cesty, �hel k detekci v��ky
                if (Physics.Raycast(point, Vector3.down, out hit, 10))
                {
                    if (hit.collider.tag == "Wall")
                    {
                        Debug.Log("P�ek�ka detekov�na: " + hit.collider.name);
                        isPathClear = false;
                        break;
                    }
                }
            }

            if (!isPathClear)
            {
                // Pokud se detekuje p�ek�ka, zvol�me jin� c�l
                SetNewTargetPosition();
                yield return new WaitForSeconds(waitTime);
                continue; // P�esko�� aktu�ln� cyklus a za�ne nov�
            }

            if (distanceToTarget > jumpDistance)
            {
                // Oto�en� k c�lov� pozici p�ed skokem
                yield return StartCoroutine(RotateTowards(targetPosition));
                // Skok k c�lov� pozici
                yield return StartCoroutine(JumpToTarget(targetPosition));
            }
            else
            {
                // Pokra�ov�n� s ot��en�m a pohybem
                Vector3 direction = (targetPosition - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                while (Quaternion.Angle(transform.rotation, lookRotation) > 0.1f)
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
                    yield return null;
                }

                // Pohyb k c�lov� pozici
                while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                    elapsedTime += Time.deltaTime; // Zvy�ov�n� �asova�e

                    // Pokud trv� pohyb p��li� dlouho, restartuj pohyb
                    if (elapsedTime > 0.8f)
                    {
                        Debug.Log("Pohyb trv� p��li� dlouho, restartov�n� korutiny.");
                        SetNewTargetPosition();
                        break; // Ukon�� aktu�ln� cyklus a spust� nov�
                    }

                    yield return null;
                }
            }

            // �ek�n� na dal�� pohyb
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
        // Ur�en� sm�ru k c�lov� pozici
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        // Ot��en� k c�li
        while (Quaternion.Angle(transform.rotation, lookRotation) > 0.1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator JumpToTarget(Vector3 target)
    {
        Vector3 startPos = transform.position;
        Vector3 peakPos = (startPos + target) / 2.0f; // St�edn� bod na zemi
        peakPos.y += jumpHeight; // P�id�me v��ku ke st�edu, aby objekt sko�il

        float jumpProgress = 0;
        float jumpDuration = 1.0f; // D�lka skoku

        while (jumpProgress < 1)
        {
            jumpProgress += Time.deltaTime / jumpDuration;

            // V�po�et pozice p�i skoku
            Vector3 currentPos = Vector3.Lerp(startPos, peakPos, jumpProgress);
            currentPos = Vector3.Lerp(currentPos, target, jumpProgress);

            // Ru�n� nastaven� oblouku ve vzduchu
            float heightFactor = 4 * jumpProgress * (1 - jumpProgress); // Parabolick� pohyb
            currentPos.y = Mathf.Lerp(startPos.y, peakPos.y, heightFactor);

            transform.position = currentPos;

            yield return null;
        }

        transform.position = target; // Ujist�te se, �e skon��te p�esn� v c�li
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


