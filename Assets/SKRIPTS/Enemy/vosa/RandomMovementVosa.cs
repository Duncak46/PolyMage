using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RandomMovementVosa : MonoBehaviour
{
    int damage = 1;
    public GameObject playerNONE;
    public float forceMagnitude = 1f;
    public float verticalForceMultiplier = 2f; // P�idan� faktor pro zv��en� odhozen� po ose Z
    public Rigidbody playerRigidbody;

    public float detectionRadius = 5f;
    public float moveSpeed = 3f;
    public float randomMoveSpeed = 2f;
    public float changeTargetTime = 2f;
    public float floatAmplitude = 0.5f;
    public float floatFrequency = 2f;

    public float boundaryMinX, boundaryMaxX, boundaryMinZ, boundaryMaxZ;

    private Transform player;
    private Vector3 targetPosition;
    private float timer;
    private bool chasingPlayer = false;
    private float baseY;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        SetRandomTarget();
        baseY = transform.position.y;
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        chasingPlayer = distanceToPlayer <= detectionRadius;

        if (chasingPlayer)
        {
            Vector3 chaseTarget = GetClampedPosition(player.position);
            RotateTowards(chaseTarget);
            MoveTowards(chaseTarget, moveSpeed);
        }
        else
        {
            timer -= Time.deltaTime;
            if (timer <= 0 || Vector3.Distance(transform.position, targetPosition) < 0.5f)
            {
                SetRandomTarget();
                timer = changeTargetTime;
            }
            RotateTowards(targetPosition);
            MoveTowards(targetPosition, randomMoveSpeed);
        }

        FloatMovement();
    }

    void SetRandomTarget()
    {
        float randomX = Random.Range(boundaryMinX, boundaryMaxX);
        float randomZ = Random.Range(boundaryMinZ, boundaryMaxZ);
        targetPosition = new Vector3(randomX, baseY, randomZ);
    }

    Vector3 GetClampedPosition(Vector3 position)
    {
        float clampedX = Mathf.Clamp(position.x, boundaryMinX, boundaryMaxX);
        float clampedZ = Mathf.Clamp(position.z, boundaryMinZ, boundaryMaxZ);
        return new Vector3(clampedX, baseY, clampedZ);
    }

    void MoveTowards(Vector3 target, float speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    void RotateTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        direction.y = 0;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }

    void FloatMovement()
    {
        transform.position = new Vector3(transform.position.x, baseY + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude, transform.position.z);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
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


