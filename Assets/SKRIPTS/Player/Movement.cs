using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public static bool canMove = true;
    public float speed = 3.0f; // Rychlost pohybu postavy
    public float rotationSpeed = 720.0f; // Rychlost rotace postavy
    public float jumpForce = 5.0f; // Síla skoku
    public Transform cameraTransform; // Reference na kameru
    public float jumpCooldown = 1.0f; // Délka èasu, po který je skákání zakázáno po skoku

    public RectTransform panel;
    private Rigidbody rb;
    private bool canJump = true;

    TorchSystem torchSystem;
    public GameObject parentObject;
    bool konec;
    public bool mrtvej;

    void Start()
    {
        torchSystem = parentObject.GetComponent<TorchSystem>();
        rb = GetComponent<Rigidbody>();
    }
    float elapsedTime = 0;
    void Update()
    {
        if (!konec && !mrtvej)
        {
            panel.localScale = Vector3.Lerp(panel.localScale, new Vector3(0,0,0), 5 * Time.deltaTime);
            // Získání vstupu z klávesnice
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            // Získání smìru kamery
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;

            // Zajistíme, že forward a right budou pouze na horizontální rovinì
            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            // Vytvoøení vektoru pohybu relativního ke smìru kamery
            Vector3 desiredMoveDirection = forward * moveVertical + right * moveHorizontal;

            if (desiredMoveDirection.magnitude > 0.1f && canMove)
            {
                // Pøesunutí postavy
                Vector3 moveDirection = desiredMoveDirection * speed * Time.deltaTime;
                rb.MovePosition(transform.position + moveDirection);

                // Rotace postavy smìrem pohybu
                Quaternion toRotation = Quaternion.LookRotation(desiredMoveDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }

            // Skok
            if (canJump && Input.GetKey(KeyCode.RightShift) && canMove)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                canJump = false;
                StartCoroutine(JumpCooldown());
            }
        }
        if (konec)
        {
            rb.useGravity = false;
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

            // Pohyb vzhùru
            transform.position += Vector3.up * Time.deltaTime;
            
            elapsedTime += Time.deltaTime; // Zvyšování èasovaèe

            // Pokud trvá pohyb pøíliš dlouho, restartuj pohyb
            if (elapsedTime > 5f)
            {
                panel.localScale = Vector3.Lerp(panel.localScale, new Vector3(1.01f, 1.01f, 1.01f), 5 * Time.deltaTime);
                if (panel.localScale == new Vector3(1.01f, 1.01f, 1.01f))
                {
                    string scene = "";
                    LevelManager.level++;
                    if (LevelManager.level == 6)
                    {
                        LevelManager.World++;
                        LevelManager.level = 1;
                        //pøidání podmínky že když je to treti world tak jsou titulky
                    }
                    switch (LevelManager.World)
                    {
                        case 1: scene = "LevelSelectorW1"; break;
                        case 2: scene = "LevelSelectorW2"; break;
                        case 3: scene = "LevelSelectorW3"; break;
                    }
                    SceneManager.LoadScene(scene);
                }
            }
        }
    }

    IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(jumpCooldown);
        canJump = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EndePortal") && torchSystem.iHave == torchSystem.HowMuch)
        {
            konec = true;
        }
    }
}
