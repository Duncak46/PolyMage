using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public static bool canMove = true;
    public float speed = 3.0f; // Rychlost pohybu postavy
    public float rotationSpeed = 720.0f; // Rychlost rotace postavy
    public float jumpForce = 5.0f; // Síla skoku
    public Transform cameraTransform; // Reference na kameru
    public float jumpCooldown = 1.0f; // Délka èasu, po který je skákání zakázáno po skoku

    private Rigidbody rb;
    private bool canJump = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
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
