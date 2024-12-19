using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot1 : MonoBehaviour
{
    public Material Black;
    public Material yellow;

    private int power = 10;
    public static float powerLevel = 10;
    public GameObject PW1;
    private Renderer pw1;
    public GameObject PW2;
    private Renderer pw2;
    public GameObject PW3;
    private Renderer pw3;
    public GameObject PW4;
    private Renderer pw4;
    public GameObject PW5;
    private Renderer pw5;
    public GameObject PW6;
    private Renderer pw6;
    public GameObject PW7;
    private Renderer pw7;
    public GameObject PW8;
    private Renderer pw8;
    public GameObject PW9;
    private Renderer pw9;
    public GameObject PW10;
    private Renderer pw10;


    public static int whichshoot = 1;
    public GameObject bulletPrefab;
    public Transform firePoint;

    public Transform firePointDouble2;
    public Transform firePointDouble1;
    public float bulletSpeed = 7f;

    public static bool isShooting = false;

    private void Start()
    {
        isShooting = false;
        power = 10;
        pw1 = PW1.GetComponent<Renderer>();
        pw2 = PW2.GetComponent<Renderer>();
        pw3 = PW3.GetComponent<Renderer>();
        pw4 = PW4.GetComponent<Renderer>();
        pw5 = PW5.GetComponent<Renderer>();
        pw6 = PW6.GetComponent<Renderer>();
        pw7 = PW7.GetComponent<Renderer>();
        pw8 = PW8.GetComponent<Renderer>();
        pw9 = PW9.GetComponent<Renderer>();
        pw10 = PW10.GetComponent<Renderer>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            if (whichshoot == 1)
            {
                Shoots();
            }
            if (whichshoot == 2)
            {
                DoubleShoots();
            }
        }
        if (power == 0)
        {
            pw1.material = Black;
            pw2.material = Black;
            pw3.material = Black;
            pw4.material = Black;
            pw5.material = Black;
            pw6.material = Black;
            pw7.material = Black;
            pw8.material = Black;
            pw9.material = Black;
            pw10.material = Black;
        }
        if (power == 1)
        {
            pw1.material = yellow;
            pw2.material = Black;
            pw3.material = Black;
            pw4.material = Black;
            pw5.material = Black;
            pw6.material = Black;
            pw7.material = Black;
            pw8.material = Black;
            pw9.material = Black;
            pw10.material = Black;
        }
        if (power == 2)
        {
            pw1.material = yellow;
            pw2.material = yellow;
            pw3.material = Black;
            pw4.material = Black;
            pw5.material = Black;
            pw6.material = Black;
            pw7.material = Black;
            pw8.material = Black;
            pw9.material = Black;
            pw10.material = Black;
        }
        if (power == 3)
        {
            pw1.material = yellow;
            pw2.material = yellow;
            pw3.material = yellow;
            pw4.material = Black;
            pw5.material = Black;
            pw6.material = Black;
            pw7.material = Black;
            pw8.material = Black;
            pw9.material = Black;
            pw10.material = Black;
        }
        if (power == 4)
        {
            pw1.material = yellow;
            pw2.material = yellow;
            pw3.material = yellow;
            pw4.material = yellow;
            pw5.material = Black;
            pw6.material = Black;
            pw7.material = Black;
            pw8.material = Black;
            pw9.material = Black;
            pw10.material = Black;
        }
        if (power == 5)
        {
            pw1.material = yellow;
            pw2.material = yellow;
            pw3.material = yellow;
            pw4.material = yellow;
            pw5.material = yellow;
            pw6.material = Black;
            pw7.material = Black;
            pw8.material = Black;
            pw9.material = Black;
            pw10.material = Black;
        }
        if (power == 6)
        {
            pw1.material = yellow;
            pw2.material = yellow;
            pw3.material = yellow;
            pw4.material = yellow;
            pw5.material = yellow;
            pw6.material = yellow;
            pw7.material = Black;
            pw8.material = Black;
            pw9.material = Black;
            pw10.material = Black;
        }
        if (power == 7)
        {
            pw1.material = yellow;
            pw2.material = yellow;
            pw3.material = yellow;
            pw4.material = yellow;
            pw5.material = yellow;
            pw6.material = yellow;
            pw7.material = yellow;
            pw8.material = Black;
            pw9.material = Black;
            pw10.material = Black;
        }
        if (power == 8)
        {
            pw1.material = yellow;
            pw2.material = yellow;
            pw3.material = yellow;
            pw4.material = yellow;
            pw5.material = yellow;
            pw6.material = yellow;
            pw7.material = yellow;
            pw8.material = yellow;
            pw9.material = Black;
            pw10.material = Black;
        }
        if (power == 9)
        {
            pw1.material = yellow;
            pw2.material = yellow;
            pw3.material = yellow;
            pw4.material = yellow;
            pw5.material = yellow;
            pw6.material = yellow;
            pw7.material = yellow;
            pw8.material = yellow;
            pw9.material = yellow;
            pw10.material = Black;
        }
        if (power == 10)
        {
            pw1.material = yellow;
            pw2.material = yellow;
            pw3.material = yellow;
            pw4.material = yellow;
            pw5.material = yellow;
            pw6.material = yellow;
            pw7.material = yellow;
            pw8.material = yellow;
            pw9.material = yellow;
            pw10.material = yellow;
        }
    }
    void DoubleShoots()
    {
        StopAllCoroutines();
        if (power > 0 && Movement.pohyb)
        {
            isShooting = true;
            // Vytvoøení instance prefabu støely na pozici a rotaci firePointu
            GameObject bullet = Instantiate(bulletPrefab, firePointDouble1.position, firePointDouble1.rotation);
            GameObject bullet2 = Instantiate(bulletPrefab, firePointDouble2.position, firePointDouble2.rotation);

            // Získání Rigidbody komponenty vytvoøené støely
            Rigidbody bulletRigidbody1 = bullet.GetComponent<Rigidbody>();
            Rigidbody bulletRigidbody2 = bullet2.GetComponent<Rigidbody>();

            // Pokud má støela Rigidbody komponentu
            if (bulletRigidbody1 != null)
            {
                // Nastavení rychlosti støely
                bulletRigidbody1.velocity = firePointDouble1.forward * bulletSpeed;
            }
            if (bulletRigidbody2 != null)
            {
                // Nastavení rychlosti støely
                bulletRigidbody2.velocity = firePointDouble2.forward * bulletSpeed;
            }
            power--;
        }
        StartCoroutine(Wait());
    }
    void Shoots()
    {
        StopAllCoroutines();
        if (power > 0 && Movement.pohyb)
        {
            isShooting = true;
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
            power--;
        }
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.3f);
        isShooting = false;
        while (!isShooting && power < 10)
        {
            float waitsecond = powerLevel / 15;
            yield return new WaitForSeconds(waitsecond);
            power++;
        }
    }
    
}
