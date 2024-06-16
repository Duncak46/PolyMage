using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform playerTransform; // Reference na hr��e
    private float initialCameraZ; // Po��te�n� Z pozice kamery

    void Start()
    {
        // Ulo�en� po��te�n� Z pozice kamery
        initialCameraZ = transform.position.z;
    }

    void LateUpdate()
    {
        // Uchov�n� pevn� pozice X a Y kamery, aktualizace pouze Z podle hr��e
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, playerTransform.position.z);
        transform.position = newPosition;
    }
}
