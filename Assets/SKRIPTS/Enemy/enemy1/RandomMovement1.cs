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
    public float jumpDistance = 2.0f; // Hodnota vzd�lenosti pro sk�k�n�
    public float jumpHeight = 2.0f; // V��ka skoku

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
            // Kontrola vzd�lenosti k c�lov�mu bodu
            float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

            if (distanceToTarget > jumpDistance)
            {
                // Oto�en� k c�lov� pozici p�ed skokem
                yield return StartCoroutine(RotateTowards(targetPosition));
                // Skok k c�lov� pozici
                yield return StartCoroutine(JumpToTarget(targetPosition));
            }
            else
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
            }

            // �ek�n� na dal�� pohyb
            yield return new WaitForSeconds(waitTime);
            SetNewTargetPosition();
        }
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
}
