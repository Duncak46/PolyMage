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

    //Faze3
    public Transform pointB;
    private bool startedPhase3 = false;

    //Faze4
    public GameObject gizmoObject;
    public float angle = 0f;
    public float speedPhase4 = 5f;

    private GizmosBoss1 odecteniRotace = new GizmosBoss1();
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
        if (phase==3)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointB.position, speedFaze1 * Time.deltaTime);
            if (transform.position == pointB.position)
            {
                StartCoroutine(Phase4());
                phase = 4;
            }
        }
        //Faze 4 - Rotace okolo hrace
        if (phase == 4)
        {
            float radius = gizmoObject.transform.localScale.x / 2f;

            // Aktualizace úhlu (rychlost + èas)
            angle += speedPhase4 * Time.deltaTime;

            // Pøevod úhlu na radiány a výpoèet pozice
            float x = gizmoObject.transform.position.x + Mathf.Cos(angle) * radius;
            float z = gizmoObject.transform.position.z + Mathf.Sin(angle) * radius;
            float y = gizmoObject.transform.position.y; // Udržet stejnou výšku

            // Nastavení nové pozice objektu
            transform.position = new Vector3(x, y, z);
        }
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
    IEnumerator Phase4()
    {
        while (odecteniRotace.gizmosValue != 2f)
        {
            odecteniRotace.gizmosValue -= 0.1f;
            yield return new WaitForSeconds(0.2f);
        }
        while (odecteniRotace.gizmosValue != 4f)
        {
            odecteniRotace.gizmosValue -= 0.1f;
            yield return new WaitForSeconds(0.2f);
        }
        phase = 5;
        startedPhase3 = false;
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
