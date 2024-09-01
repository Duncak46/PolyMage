using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.SceneManagement;

public class HPSystem : MonoBehaviour
{
    public TMP_Text coiny;
    public static int coins = 0;
    public static int health = 3;
    private bool canMinus = true;
    private SkinnedMeshRenderer objectRenderer;
    public float blinkDuration = 0.1f; // Délka jednoho bliknutí
    public int blinkCount = 5;
    public GameObject MagebroW;
    public GameObject Magebro;

    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject heart4;
    public GameObject heart5;
    public Rigidbody rb;
    public float deathForce = 10.0f; // Síla vystøelení po ose Y
    public float rotationSpeed = 180.0f; // Rychlost rotace kolem osy Y
    public float destroyDelay = 2.0f;
    float elapsedTime = 0;
    public RectTransform panel;
   
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

    void Update()
    {
        coiny.text = coins.ToString();
        switch (health)
        {
            case 0: heart1.SetActive(false); heart2.SetActive(false); heart3.SetActive(false); heart4.SetActive(false); heart5.SetActive(false); Die(); break;
            case 1: heart1.SetActive(true); heart2.SetActive(false); heart3.SetActive(false); heart4.SetActive(false); heart5.SetActive(false);  break;
            case 2: heart1.SetActive(true); heart2.SetActive(true); heart3.SetActive(false); heart4.SetActive(false); heart5.SetActive(false);  break;
            case 3: heart1.SetActive(true); heart2.SetActive(true); heart3.SetActive(true); heart4.SetActive(false); heart5.SetActive(false);  break;
            case 4: heart1.SetActive(true); heart2.SetActive(true); heart3.SetActive(true); heart4.SetActive(true); heart5.SetActive(false); break;
            case 5: heart1.SetActive(true); heart2.SetActive(true); heart3.SetActive(true); heart4.SetActive(true); heart5.SetActive(true); break;
        }
    }
    private void Die()
    {
        Movement movementScript = rb.gameObject.GetComponent<Movement>();
        if (movementScript != null)
        {
            movementScript.mrtvej = true;
            
        }


        // Zastavit veškerý aktuální pohyb
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Vystøelení po ose Y s definovanou silou
        Vector3 deathDirection = Vector3.up;
        rb.AddForce(deathDirection, ForceMode.Impulse);

        // Rotace kolem osy Y
        Vector3 rotationAxis = new Vector3(0, 1, 0);
        rb.AddTorque(rotationAxis * rotationSpeed, ForceMode.VelocityChange);

        // Znièení objektu po urèitém èase
        elapsedTime += Time.deltaTime;
        if (elapsedTime > 1)
        {
            int childCount = transform.childCount;

            // Prochází všechny dìti pozpátku (kvùli bezpeènosti pøi mazání)
            for (int i = childCount - 1; i >= 0; i--)
            {
                // Získá aktuální dítì
                Transform child = transform.GetChild(i);

                // Znièí dítì
                Destroy(child.gameObject);
            }
        }
        if (elapsedTime > 5f)
        {
            panel.localScale = Vector3.Lerp(panel.localScale, new Vector3(1.01f, 1.01f, 1.01f), 5 * Time.deltaTime);
            if (panel.localScale == new Vector3(1.01f, 1.01f, 1.01f))
            {
                switch (LevelManager.World)
                {
                    case 1: SceneManager.LoadScene("LevelSelectorW1"); break;
                    case 2: SceneManager.LoadScene("LevelSelectorW2"); break;
                    case 3: SceneManager.LoadScene("LevelSelectorW3"); break;
                }
            }
        }
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
    public void AddCoin(int howMuch)
    {
        coins += howMuch;
        Debug.Log(coins);
    }
}
