using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public GameObject holdPosition;  // Bu de�i�keni Inspector'da atay�n
    public float pickUpRange = 2f;  // Bu mesafeyi gerekti�i gibi ayarlay�n
    private GameObject pickableObject;
    private Collider[] colliders;

    void Update()
    {
        // 'E' tu�una bas�l�p bas�lmad���n� kontrol et
        if (Input.GetKeyDown(KeyCode.E))
        {
            // pickUpRange i�indeki t�m collider'lar� bul
            colliders = Physics.OverlapSphere(transform.position, pickUpRange);

            // T�m collider'lar �zerinde dola�
            foreach (Collider collider in colliders)
            {
                // Collider'�n "Pickable" veya "Cat" tag'ine sahip olup olmad���n� kontrol et
                if (collider.CompareTag("Pickable") || collider.CompareTag("Cat"))
                {
                    pickableObject = collider.gameObject;
                    PickUp();
                    break;  // Bir nesne al�nd���nda kontrol etmeyi durdur
                }
            }
        }
    }

    public void PickUp()
    {
        if (pickableObject != null)
        {
            // Pickable nesnesini oyuncunun eline ili�tir
            pickableObject.transform.SetParent(holdPosition.transform);
            pickableObject.transform.localPosition = Vector3.zero;  // Nesneyi elde do�ru konumland�r
            pickableObject.transform.localRotation = Quaternion.identity;  // Gerekirse rotasyonu s�f�rla

            Debug.Log("Nesne al�nd�: " + pickableObject.name);
        }
    }
}
