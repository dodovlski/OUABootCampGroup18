using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI elemanlar�n� kullanmak i�in gerekli

public class EscapeBarController : MonoBehaviour
{
    public Slider escapeBar; // Basma bar�n� temsil eden UI Slider
    public float maxFillTime = 3f; // Bar�n tamamen dolmas� i�in gerekli s�re
    private float currentFillTime = 0f;
    private bool isInCage = false;
    private GameObject catObject;

    void Update()
    {
        if (isInCage && Input.GetKey(KeyCode.E))
        {
            // E tu�una bas�ld�k�a bar� doldur
            currentFillTime += Time.deltaTime;
            escapeBar.value = currentFillTime / maxFillTime;

            // Bar tamamen dolduysa kediyi serbest b�rak
            if (currentFillTime >= maxFillTime)
            {
                ReleaseCat();
            }
        }
        else
        {
            // E tu�u b�rak�ld���nda veya kedi kafeste de�ilse bar� s�f�rla
            currentFillTime = 0f;
            escapeBar.value = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cat"))
        {
            isInCage = true;
            catObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cat"))
        {
            isInCage = false;
            catObject = null;
        }
    }

    private void ReleaseCat()
    {
        // Kediyi serbest b�rak
        if (catObject != null)
        {
            // Kediyi kafesten ��kar
            catObject.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            isInCage = false;
            currentFillTime = 0f;
            escapeBar.value = 0f;
            catObject = null;

            Debug.Log("Kedi kafesten ka�t�!");
        }
    }
}
