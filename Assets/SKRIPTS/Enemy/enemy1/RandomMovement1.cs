using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement1 : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float waitTime = 2.0f;
    public Vector2 boundaryMin; // Minim�ln� x a z hranice
    public Vector2 boundaryMax; // Maxim�ln� x a z hranice
    public float rotationSpeed = 5.0f;
    public bool canMove = true;

    private Vector3 targetPosition;

    void Start()
    {
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
        while (true && canMove)
        {
            // Oto�en� k c�lov� pozici
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
                yield return null;
            }

            // �ek�n� na dal�� pohyb
            yield return new WaitForSeconds(waitTime);
            SetNewTargetPosition();
        }
    }
}
