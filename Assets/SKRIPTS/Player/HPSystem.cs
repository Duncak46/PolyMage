using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPSystem : MonoBehaviour
{
    public int health = 10;
    private bool canMinus = true;
    private SkinnedMeshRenderer objectRenderer;
    public float blinkDuration = 0.1f; // Délka jednoho bliknutí
    public int blinkCount = 5;

    void Start()
    {
        objectRenderer = GetComponent<SkinnedMeshRenderer>();
    }
    public void TakeDamage(int damageAmount)
    {
        if (canMinus)
        {
            Movement.canMove = false;
            canMinus = false;
            StartCoroutine(takingDamage());
            health -= damageAmount;
            if (health <= 0)
            {
                Die();
            }
            Debug.Log(health);
            StartCoroutine(Blink());
        }
    }

    private void Die()
    {
        // Zde mùžete pøidat logiku pro smrt hráèe, napø. restartování úrovnì, zobrazení zprávy, atd.
        Debug.Log("Player died!");
    }

    IEnumerator takingDamage()
    {
        yield return new WaitForSeconds(1f);
        canMinus = true;
        Movement.canMove = true;
    }
    IEnumerator Blink()
    {
        for (int i = 0; i < blinkCount; i++)
        {
            objectRenderer.enabled = false; // Nastaví barvu na blikací barvu
            yield return new WaitForSeconds(blinkDuration);
            objectRenderer.enabled = true; // Vrátí barvu na pùvodní
            yield return new WaitForSeconds(blinkDuration);
        }
    }
}
