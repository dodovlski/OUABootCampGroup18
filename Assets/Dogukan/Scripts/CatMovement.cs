using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMovement : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float runSpeed = 5f;
    public float jumpForce = 5f;
    public float maxStamina = 4f; // Maksimum stamina s�resi
    public float staminaRecoveryRate = 1f; // Saniyede ne kadar stamina geri kazan�l�r

    private float currentStamina;
    private bool isRunning;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentStamina = maxStamina; // Ba�lang��ta maksimum stamina ile ba�la
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0)
        {
            // Ko�
            rb.MovePosition(transform.position + movement * runSpeed * Time.deltaTime);
            currentStamina -= Time.deltaTime; // Stamina azalt
            isRunning = true;
        }
        else
        {
            // Y�r�
            rb.MovePosition(transform.position + movement * walkSpeed * Time.deltaTime);
            isRunning = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Z�pla
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Stamina geri kazan�m�
        if (!isRunning && currentStamina < maxStamina)
        {
            currentStamina += staminaRecoveryRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina); // Stamina maksimum de�eri a�mas�n
        }
    }
}