using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    public GameObject shoot;
    public GameObject VFX;
    private bool zapaleno;
    TorchSystem torchSystem;
    public GameObject parentObject;

    private void Start()
    {
        torchSystem = parentObject.GetComponent<TorchSystem>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        shoot = collision.gameObject;
        if (shoot.CompareTag("Fire") && zapaleno == false)
        {
            torchSystem.iHave++;
            zapaleno = true;
            Destroy(shoot);
            VFX.SetActive(true);
        }
    }
}
