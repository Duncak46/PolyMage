using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyOnly : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(destroyBro());
    }
    IEnumerator destroyBro()
    {
        yield return new WaitForSeconds(6f);
        Destroy(gameObject);
    }
}
