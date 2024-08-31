using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ostny : MonoBehaviour
{
    public float maxBounceVelocity = 5f; // Maxim�ln� povolen� rychlost odrazu

    void OnCollisionEnter(Collision collision)
    {
        // Zkontroluje, zda se objekt, kter� se srazil s ostny, m� Rigidbody (tedy hr��)
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Z�sk� aktu�ln� rychlost odrazu
            Vector3 bounceVelocity = rb.velocity;

            // Zkontroluje, zda je odrazov� rychlost v�t�� ne� povolen�
            if (bounceVelocity.magnitude > maxBounceVelocity)
            {
                // Omez� rychlost odrazu na maxim�ln� hodnotu
                rb.velocity = bounceVelocity.normalized * maxBounceVelocity;
            }
        }
    }
}
