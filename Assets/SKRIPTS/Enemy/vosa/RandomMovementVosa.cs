using System.Collections;
using UnityEngine;

public class RandomMovementVosa : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float waitTime = 2.0f;
    public Vector2 boundaryMin; // Minimální x a z hranice
    public Vector2 boundaryMax; // Maximální x a z hranice
    public float rotationSpeed = 5.0f;
    public bool canMove = true;
    public float detectionRadius = 5.0f; // Vzdálenost pro detekci hráèe
    public LayerMask playerLayer; // Vrstva hráèe

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
        // Použij Physics.OverlapSphere k detekci hráèe v okruhu
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
                // Pøímo smìøuj k pozici hráèe
                targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);

                
                    targetPosition.x = Mathf.Clamp(targetPosition.x, boundaryMin.x, boundaryMax.x);
                
                
                    targetPosition.z = Mathf.Clamp(targetPosition.z, boundaryMin.y, boundaryMax.y);
                
                
            }

            // Pohyb k cílové pozici
            Vector3 direction = (targetPosition - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }

            // Èekání na další cílovou pozici, pokud nejsme v režimu sledování
            if (!isChasing)
            {
                yield return new WaitForSeconds(waitTime);
                SetNewTargetPosition();
            }

            yield return null;
        }
    }
}
