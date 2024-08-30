using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KolizeShoot : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Znièí tento GameObject pøi kolizi s jiným objektem
        Destroy(gameObject);
    }
}
