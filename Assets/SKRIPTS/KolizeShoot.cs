using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KolizeShoot : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Znièí se gameobject, pokud se dotkne jakéhokoli jiného objektu
        Destroy(gameObject);
    }
}
