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
        // Vytvoøení instance prefabu støely na pozici a rotaci firePointu
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Získání Rigidbody komponenty vytvoøené støely
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

        // Pokud má støela Rigidbody komponentu
        if (bulletRigidbody != null)
        {
            // Nastavení rychlosti støely
            bulletRigidbody.velocity = firePoint.forward * bulletSpeed;
        }
        else
        {
            Debug.LogWarning("Prefab støely neobsahuje Rigidbody komponentu!");
        }
    }
}
