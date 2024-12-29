using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Boss1 : MonoBehaviour
{
    //Hrac
    public Transform player;
    //Start
    bool startBoss = false;
    float attackRange = 4f;

    //Faze
    private int phase = 0;

    //Faze 1
    public Transform pointA;
    public float speedFaze1 = 3f;

    //Faze 2
    private bool startedPhase2 = false;
    public GameObject WarningPrefab;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Start Bosse
        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;
        if (distanceToPlayer <= attackRange)
        {
            startBoss = true;
        }
        if (startBoss)
        {
            phase = 1;
            startBoss = false;
        }
        //Faze 1 - Oddaleni Bosse do bodu A
        if (phase == 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointA.position, speedFaze1 * Time.deltaTime);
            if (transform.position == pointA.position)
            {
                phase = 2;
            }
        }
        //Faze 2 - Zvednuti rukou a strileni Koulí tam kde byl hráè - DODELAT RUCE
        if (phase == 2 && !startedPhase2)
        {
            startedPhase2 = true;
            StartCoroutine(Phase2());
        }
        //Faze 3 - Pøiblížení k ploše

    }
    //Phase2
    IEnumerator Phase2()
    {
        for (int i = 0; i < 23; i++)
        {
            DropWarning();
            yield return new WaitForSeconds(0.6f);
        }
        phase = 3;
        startedPhase2 = false;
    }
    void DropWarning()
    {
        if (player.transform.position.z < -31.329f)
        {
            if (player.transform.position.x > -0.639f)
            {
                Instantiate(WarningPrefab, new Vector3(-0.639f, -0.162f, -31.329f), Quaternion.Euler(0, 0, 0));
            }
            else if (player.transform.position.x < -3.462f)
            {
                Instantiate(WarningPrefab, new Vector3(-3.462f, -0.162f, -31.329f), Quaternion.Euler(0, 0, 0));
            }
            else
            {
                Instantiate(WarningPrefab, new Vector3(player.position.x, -0.162f, -31.329f), Quaternion.Euler(0, 0, 0));
            }
        }
        else if (player.transform.position.z > -27.18f)
        {
            if (player.transform.position.x > -0.639f)
            {
                Instantiate(WarningPrefab, new Vector3(-0.639f, -0.162f, -27.18f), Quaternion.Euler(0, 0, 0));
            }
            else if (player.transform.position.x < -3.462f)
            {
                Instantiate(WarningPrefab, new Vector3(-3.462f, -0.162f, -27.18f), Quaternion.Euler(0, 0, 0));
            }
            else
            {
                Instantiate(WarningPrefab, new Vector3(player.position.x, -0.162f, -27.18f), Quaternion.Euler(0, 0, 0));
            }
        }
        else if (player.transform.position.x < -3.462f)
        {
            Instantiate(WarningPrefab, new Vector3(-3.462f, -0.162f, player.position.z), Quaternion.Euler(0, 0, 0));
        }
        else if (player.transform.position.x > -0.639f)
        {
            Instantiate(WarningPrefab, new Vector3(-0.639f, -0.162f, player.position.z), Quaternion.Euler(0, 0, 0));
        }
        else 
        {
            Instantiate(WarningPrefab, new Vector3(player.position.x, -0.162f, player.position.z), Quaternion.Euler(0, 0, 0));
        }
        
    }
    
    void OnDrawGizmos()
    {
        // Nakreslíme radius pro útok v editoru
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
