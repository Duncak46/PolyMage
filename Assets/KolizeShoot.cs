using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KolizeShoot : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Zni�� se gameobject, pokud se dotkne jak�hokoli jin�ho objektu
        Destroy(gameObject);
    }
}
