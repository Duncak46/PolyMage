using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public static bool canMove = true;
    public float speed = 3.0f; // Rychlost pohybu postavy
    public float rotationSpeed = 720.0f; // Rychlost rotace postavy
    public float jumpForce = 5.0f; // S�la skoku
    public Transform cameraTransform; // Reference na kameru
    public float jumpCooldown = 1.0f; // D�lka �asu, po kter� je sk�k�n� zak�z�no po skoku

    private Rigidbody rb;
    private bool canJump = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
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

    IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(jumpCooldown);
        canJump = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject Coin = collision.gameObject;
        if (Coin.CompareTag("Coin"))
        {
            HPSystem.coins += 5;
            Destroy(Coin);
        }
    }
}
