using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittingBlink : MonoBehaviour
{
    public GameObject shoot;
    public GameObject blockOriginal;
    public GameObject blockWhenBlink;

    private void OnCollisionEnter(Collision collision)
    {
        shoot = collision.gameObject;
        if (shoot.CompareTag("Fire"))
        {
            StartCoroutine(Blink());
        }
    }

    IEnumerator Blink()
    {
        blockOriginal.SetActive(false);
        blockWhenBlink.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        blockWhenBlink.SetActive(false);
        blockOriginal.SetActive(true);
    }
}
