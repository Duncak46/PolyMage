using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KolizeShoot : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Zni�� tento GameObject p�i kolizi s jin�m objektem
        Destroy(gameObject);
    }
}
