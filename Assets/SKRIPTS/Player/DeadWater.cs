using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadWater : MonoBehaviour
{
    public GameObject playerr;
    HPSystem hPSystem;
    // Start is called before the first frame update
    void Start()
    {
        hPSystem = playerr.GetComponent<HPSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        GameObject player = collision.gameObject;
        if (player.CompareTag("Player"))
        {
            hPSystem.TakeDamage(5);
        }
    }
}
