using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform playerTransform; // Reference na hr·Ëe
    

    void Start()
    {
        
    }
    void LateUpdate()
    {
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, playerTransform.position.z);
            transform.position = newPosition;
    }
}
