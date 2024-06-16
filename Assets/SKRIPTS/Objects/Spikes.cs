using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public int damage = 1;
    public float forceMagnitude = 1f;
    public Rigidbody playerRigidbody;
    public GameObject player;
    public GameObject playerNONE;

    private void OnCollisionEnter(Collision collision)
    {
        player = collision.gameObject;

        if (player.CompareTag("Player"))
        {
            HPSystem playerHealth = playerNONE.GetComponent<HPSystem>();
            Vector3 contactPoint = collision.GetContact(0).point;
            Vector3 objectForward = transform.forward;
            Vector3 objectCenter = transform.position;

            if (playerRigidbody != null)
            {
                playerHealth.TakeDamage(damage);
                Vector3 toPlayer = contactPoint - objectCenter;
                float dotProduct = Vector3.Dot(objectForward, toPlayer);

                Vector3 forceDirection;

                if (dotProduct > 0)
                {
                    // Dotek z pøední strany, odhození do pravé strany objektu
                    forceDirection = Quaternion.Euler(0, 90, 0) * objectForward;
                }
                else
                {
                    // Dotek ze zadní strany, odhození do levé strany objektu
                    forceDirection = Quaternion.Euler(0, -90, 0) * objectForward;
                }

                playerRigidbody.AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);
            }
        }
    }
}
