using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Boss1 : MonoBehaviour
{
    //Hrac
    public Transform player;
    public Transform playerNONE;
    public Rigidbody playerRigidbody;
    public float forceMagnitude = 1f;
    int damage = 1;
    public float verticalForceMultiplier = 2f;


    //Start
    bool startBoss = false;
    float attackRange = 4f;

    //Faze
    private int phase = 0;

    //Faze 1
    private bool onlyOne;
    public Transform pointA;
    public float speedFaze1 = 3f;

    //Faze 2
    private bool startedPhase2 = false;
    public GameObject WarningPrefab;

    //Faze3
    public Transform pointB;
    private bool startedPhase3 = false;

    //Faze4
    public float speedPhase4 = 5f;
    private int phaseBody;
    public Transform bodB2;
    public Transform bodA;
    public Transform bodC;
    public Transform bodD;
    public Transform bodE;
    public Transform bodF;
    public Transform bodG;
    public float speedFaze4 = 8f;

    //Faze5
    public Transform pointBallsSpawn;
    public GameObject ballsPrefab;
    private bool startedPhase5 = true;

    private GizmosBoss1 odecteniRotace = new GizmosBoss1();
    void Start()
    {
        onlyOne = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Start Bosse
        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;
        if (distanceToPlayer <= attackRange && onlyOne == true)
        {
            startBoss = true;
        }
        if (startBoss)
        {
            phase = 1;
            startBoss = false;
            onlyOne = true;
        }
        //Faze 1 - Oddaleni Bosse do bodu A
        if (phase == 1 && onlyOne)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointA.position, speedFaze1 * Time.deltaTime);
            if (transform.position == pointA.position)
            {
                phase = 2;
                onlyOne = false;
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
                phase = 4;
                phaseBody = 1;
            }
        }
        //Dostat se do bodù
        if (phase == 4)
        {
            //A prom1
            if (phaseBody == 1)
            {
                transform.Rotate(Vector3.up, speedFaze4 * Time.deltaTime * 15);
                transform.position = Vector3.MoveTowards(transform.position, bodA.position, speedFaze4 * Time.deltaTime);
            }
            //B prom2
            if (transform.position == bodA.position || phaseBody == 2)
            {
                phaseBody = 2;
                transform.Rotate(Vector3.down, speedFaze4 * Time.deltaTime * 15);
                transform.position = Vector3.MoveTowards(transform.position, bodC.position, speedFaze4 * Time.deltaTime);
            }
            //C prom3
            if (transform.position == bodC.position || phaseBody == 3)
            {
                phaseBody = 3;
                transform.Rotate(Vector3.up, speedFaze4 * Time.deltaTime * 15);
                transform.position = Vector3.MoveTowards(transform.position, bodD.position, speedFaze4 * Time.deltaTime);
            }
            //D prom4
            if (transform.position == bodD.position || phaseBody == 4)
            {
                phaseBody = 4;
                transform.Rotate(Vector3.down, speedFaze4 * Time.deltaTime * 15);
                transform.position = Vector3.MoveTowards(transform.position, bodB2.position, speedFaze4 * Time.deltaTime);
            }
            //A prom5
            if (transform.position == bodB2.position || phaseBody == 5)
            {
                phaseBody = 5;
                transform.Rotate(Vector3.up, speedFaze4 * Time.deltaTime * 15);
                transform.position = Vector3.MoveTowards(transform.position, bodE.position, speedFaze4 * Time.deltaTime);
            }
            //E prom6
            if (transform.position == bodE.position || phaseBody == 6)
            {
                phaseBody = 6;
                transform.Rotate(Vector3.down, speedFaze4 * Time.deltaTime * 15);
                transform.position = Vector3.MoveTowards(transform.position, bodF.position, speedFaze4 * Time.deltaTime);
            }
            //F prom7
            if (transform.position == bodF.position || phaseBody == 7)
            {
                phaseBody = 7;
                transform.Rotate(Vector3.up, speedFaze4 * Time.deltaTime * 15);
                transform.position = Vector3.MoveTowards(transform.position, bodG.position, speedFaze4 * Time.deltaTime);
            }
            if (transform.position == bodG.position)
            {
                phase = 5;
                StartCoroutine(pomocna());
            }
        }
        if (phase == 5)
        {
            if (!startedPhase5)
            {
                startedPhase5 = true;
                StartCoroutine(Phase5());
            }
            else if (transform.rotation.y != 180f)
            {
                Quaternion targetRotation = Quaternion.Euler(0, 180f, 0);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speedFaze4 * Time.deltaTime * 15);
            }
        }
        if (phase == 6)
        {
            Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speedFaze4 * Time.deltaTime * 15);
            transform.position = Vector3.MoveTowards(transform.position, pointA.position, speedFaze4 * Time.deltaTime);
            if (transform.position == pointA.position && transform.rotation == targetRotation)
            {
                phase = 7;
            }
        }
        if (phase == 7)
        {
            //reset1
            startedPhase2 = false;
            //reset2
            startedPhase5 = true;
            phase = 2;
        }
    }
    IEnumerator pomocna()
    {
        yield return new WaitForSeconds(3f);
        startedPhase5 = false;
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
    }
    IEnumerator Phase5()
    {
        for (int i = 0; i < 15; i++)
        {
            DropBalls();
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(5f);
        phase = 6;
    }
    
    void DropBalls()
    {
        Instantiate(ballsPrefab, pointBallsSpawn.position, Quaternion.Euler(0, 0, 0));
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

    private void OnCollisionEnter(Collision collision)
    {
        GameObject player = collision.gameObject;

        if (player.CompareTag("Player"))
        {
            HPSystem playerHealth = playerNONE.GetComponent<HPSystem>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            Vector3 contactPoint = collision.GetContact(0).point;
            Vector3 normalDirection = collision.GetContact(0).normal;

            if (playerRigidbody != null)
            {
                Vector3 forceDirection;

                // Kontrola, zda kolize pøišla shora
                if (normalDirection.y < -0.5f) // Detekce, jestli normála smìøuje pøevážnì nahoru
                {
                    // Odhození po ose Z (stranou) místo osy Y, aby hráè nespadl zpìt na objekt
                    forceDirection = new Vector3(0, 0, Mathf.Sign(contactPoint.z - transform.position.z) * verticalForceMultiplier);
                }
                else
                {
                    // Standardní odhození ve smìru normály
                    forceDirection = normalDirection * -1;
                }

                playerRigidbody.AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);

                // Spuštìní probliknutí
            }
        }
    }
}
