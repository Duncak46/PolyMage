using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KolizeShoot : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
            Destroy(gameObject);
    }
}
