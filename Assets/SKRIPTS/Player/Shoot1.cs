using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot1 : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 7f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Shoots();
        }
    }

    void Shoots()
    {
        // Vytvo�en� instance prefabu st�ely na pozici a rotaci firePointu
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Z�sk�n� Rigidbody komponenty vytvo�en� st�ely
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

        // Pokud m� st�ela Rigidbody komponentu
        if (bulletRigidbody != null)
        {
            // Nastaven� rychlosti st�ely
            bulletRigidbody.velocity = firePoint.forward * bulletSpeed;
        }
        else
        {
            Debug.LogWarning("Prefab st�ely neobsahuje Rigidbody komponentu!");
        }
    }
}
