using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public GameObject shoot;
    public GameObject blockOriginal;
    public GameObject blockWhenBlink;
    public GameObject coin;
    private bool canDestroy;
    private int HP = 5;
    bool justOne = true;

    void Start()
    {
        int rnd = Random.Range(0, 5);
        if (rnd == 3)
        {
            canDestroy = true;
        }
        else
        {
            canDestroy = false;
        }
    }
    private bool hasBeenHit = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (canDestroy)
        {
            GameObject shoot = collision.gameObject;
            if (shoot.CompareTag("Fire"))
            {
                HP--;
                if (HP <= 0 && justOne)
                {
                    hasBeenHit = true;
                    SpawnCoin();
                    Destroy(gameObject);
                }
                StartCoroutine(Blink());
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        hasBeenHit = false; // Reset the flag when the collision ends if needed
    }

    IEnumerator Blink()
    {
        blockOriginal.SetActive(false);
        blockWhenBlink.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        blockWhenBlink.SetActive(false);
        blockOriginal.SetActive(true);
    }
    private void SpawnCoin()
    {
        if (hasBeenHit)
        {
            Instantiate(coin, new Vector3(transform.position.x, transform.position.y+0.7f, transform.position.z), transform.rotation);
            hasBeenHit= false;
            justOne = false;
        }
    }
}
