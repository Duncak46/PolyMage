using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosBoss1 : MonoBehaviour
{
    public float gizmosValue = 4f;
    // Start is called before the first frame update
    void OnDrawGizmos()
    {
        // Nakreslíme radius pro útok v editoru
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, gizmosValue);
    }
}
