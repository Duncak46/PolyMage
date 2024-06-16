using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform playerTransform; // Reference na hráèe
    private float initialCameraZ; // Poèáteèní Z pozice kamery

    void Start()
    {
        // Uložení poèáteèní Z pozice kamery
        initialCameraZ = transform.position.z;
    }

    void LateUpdate()
    {
        // Uchování pevné pozice X a Y kamery, aktualizace pouze Z podle hráèe
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, playerTransform.position.z);
        transform.position = newPosition;
    }
}
