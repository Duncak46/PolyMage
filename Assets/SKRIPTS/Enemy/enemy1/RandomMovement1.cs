using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement1 : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float waitTime = 2.0f;
    public Vector2 boundaryMin; // Minimální x a z hranice
    public Vector2 boundaryMax; // Maximální x a z hranice
    public float rotationSpeed = 5.0f;
    public bool canMove = true;
    public float jumpDistance = 2.0f; // Hodnota vzdálenosti pro skákání
    public float jumpHeight = 2.0f; // Výška skoku

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
            // Kontrola vzdálenosti k cílovému bodu
            float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

            if (distanceToTarget > jumpDistance)
            {
                // Otoèení k cílové pozici pøed skokem
                yield return StartCoroutine(RotateTowards(targetPosition));
                // Skok k cílové pozici
                yield return StartCoroutine(JumpToTarget(targetPosition));
            }
            else
            {
                // Otoèení k cílové pozici
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
                    yield return null;
                }
            }

            // Èekání na další pohyb
            yield return new WaitForSeconds(waitTime);
            SetNewTargetPosition();
        }
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
}
