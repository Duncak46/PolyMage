using System.Collections;
using UnityEngine;

public class RandomMovementVosa : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float waitTime = 2.0f;
    public Vector2 boundaryMin; // Minim�ln� x a z hranice
    public Vector2 boundaryMax; // Maxim�ln� x a z hranice
    public float rotationSpeed = 5.0f;
    public bool canMove = true;
    public float detectionRadius = 5.0f; // Vzd�lenost pro detekci hr��e
    public LayerMask playerLayer; // Vrstva hr��e

    private Vector3 targetPosition;
    private Transform player;
    private bool isChasing = false;

    void Start()
    {
        SetNewTargetPosition();
        StartCoroutine(MoveContinuously());
    }

    void Update()
    {
        // Pou�ij Physics.OverlapSphere k detekci hr��e v okruhu
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, playerLayer);
        if (hitColliders.Length > 0)
        {
            player = hitColliders[0].transform;
            isChasing = true;
        }
        else
        {
            player = null;
            isChasing = false;
        }
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

    IEnumerator MoveContinuously()
    {
        while (true && canMove)
        {
            if (isChasing && player != null)
            {
                // P��mo sm��uj k pozici hr��e
                targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);

                
                    targetPosition.x = Mathf.Clamp(targetPosition.x, boundaryMin.x, boundaryMax.x);
                
                
                    targetPosition.z = Mathf.Clamp(targetPosition.z, boundaryMin.y, boundaryMax.y);
                
                
            }

            // Pohyb k c�lov� pozici
            Vector3 direction = (targetPosition - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }

            // �ek�n� na dal�� c�lovou pozici, pokud nejsme v re�imu sledov�n�
            if (!isChasing)
            {
                yield return new WaitForSeconds(waitTime);
                SetNewTargetPosition();
            }

            yield return null;
        }
    }
}
