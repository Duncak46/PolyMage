using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ostny : MonoBehaviour
{
    public float maxBounceVelocity = 5f; // Maximální povolená rychlost odrazu

    void OnCollisionEnter(Collision collision)
    {
        // Zkontroluje, zda se objekt, který se srazil s ostny, má Rigidbody (tedy hráè)
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Získá aktuální rychlost odrazu
            Vector3 bounceVelocity = rb.velocity;

            // Zkontroluje, zda je odrazová rychlost vìtší než povolená
            if (bounceVelocity.magnitude > maxBounceVelocity)
            {
                // Omezí rychlost odrazu na maximální hodnotu
                rb.velocity = bounceVelocity.normalized * maxBounceVelocity;
            }
        }
    }
}
