using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPSystem : MonoBehaviour
{
    public static int coins = 0;
    public int health = 5;
    private bool canMinus = true;
    private SkinnedMeshRenderer objectRenderer;
    public float blinkDuration = 0.1f; // D�lka jednoho bliknut�
    public int blinkCount = 5;
    public GameObject MagebroW;
    public GameObject Magebro;

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
        // Zde m��ete p�idat logiku pro smrt hr��e, nap�. restartov�n� �rovn�, zobrazen� zpr�vy, atd.
        Debug.Log("Player died!");
    }

    IEnumerator takingDamage()
    {
        yield return new WaitForSeconds(0.4f);
        canMinus = true;
        Movement.canMove = true;
    }
    IEnumerator Blink()
    {
        for (int i = 0; i < blinkCount; i++)
        {
            MagebroW.SetActive(true);
            Magebro.SetActive(false);
            yield return new WaitForSeconds(blinkDuration);
            Magebro.SetActive(true);
            MagebroW.SetActive(false);
            yield return new WaitForSeconds(blinkDuration);
        }
    }
    public void AddCoin()
    {
        coins += 5;
        Debug.Log(coins);
    }
}
