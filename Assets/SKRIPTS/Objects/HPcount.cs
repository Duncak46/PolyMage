using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPcount : MonoBehaviour
{
    public int HP;
    public GameObject shoot;
    private void OnCollisionEnter(Collision collision)
    {
        shoot = collision.gameObject;
        if (shoot.CompareTag("Fire")) 
        {
            HP--;
            Debug.Log(HP);
            if (HP <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
