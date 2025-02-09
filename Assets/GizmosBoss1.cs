using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosBoss1 : MonoBehaviour
{
    public float gizmosValue = 4f;
    // Start is called before the first frame update
    void OnDrawGizmos()
    {
        // Nakresl�me radius pro �tok v editoru
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, gizmosValue);
    }
}
