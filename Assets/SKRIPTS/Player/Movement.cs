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
    public float jumpForce = 5.0f; // S�la skoku
    public Transform cameraTransform; // Reference na kameru
    public float jumpCooldown = 1.0f; // D�lka �asu, po kter� je sk�k�n� zak�z�no po skoku

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
            // Z�sk�n� vstupu z kl�vesnice
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            // Z�sk�n� sm�ru kamery
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;

            // Zajist�me, �e forward a right budou pouze na horizont�ln� rovin�
            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            // Vytvo�en� vektoru pohybu relativn�ho ke sm�ru kamery
            Vector3 desiredMoveDirection = forward * moveVertical + right * moveHorizontal;

            if (desiredMoveDirection.magnitude > 0.1f && canMove)
            {
                // P�esunut� postavy
                Vector3 moveDirection = desiredMoveDirection * speed * Time.deltaTime;
                rb.MovePosition(transform.position + moveDirection);

                // Rotace postavy sm�rem pohybu
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

            // Pohyb vzh�ru
            transform.position += Vector3.up * Time.deltaTime;
            
            elapsedTime += Time.deltaTime; // Zvy�ov�n� �asova�e

            // Pokud trv� pohyb p��li� dlouho, restartuj pohyb
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
                        //p�id�n� podm�nky �e kdy� je to treti world tak jsou titulky
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
