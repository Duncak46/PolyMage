using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    public GameObject shoot;
    public GameObject VFX;    
    private void OnCollisionEnter(Collision collision)
    {
        shoot = collision.gameObject;
        if (shoot.CompareTag("Fire"))
        {
            Destroy(shoot);
            VFX.SetActive(true);
        }
    }
}
