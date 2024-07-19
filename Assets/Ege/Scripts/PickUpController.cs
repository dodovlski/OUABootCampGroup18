using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public GameObject holdPosition;
    public GameObject targetPosition; // Bo� GameObject referans�
    public float pickUpRange = 2f;  // Bu mesafeyi gerekti�i gibi ayarlay�n
    public float placeRange = 2f;   // Yerle�tirme mesafesi
    private GameObject pickableObject;
    private Collider[] colliders;
    private bool isHoldingObject = false;

    void Update()
    {
        // 'E' tu�una bas�l�p bas�lmad���n� kontrol et
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isHoldingObject)
            {
                // E�er bir nesne tutuluyorsa, b�rak
                DropObject();
            }
            else
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

        // E�er bir nesne tutuluyorsa, nesnenin pozisyonunu holdPosition'a e�itle
        if (isHoldingObject && pickableObject != null)
        {
            pickableObject.transform.position = holdPosition.transform.position;
            pickableObject.transform.rotation = holdPosition.transform.rotation;
        }
    }

    public void PickUp()
    {
        if (pickableObject != null)
        {
            isHoldingObject = true;

            // Pickable nesnesini oyuncunun eline ili�tir
            pickableObject.transform.SetParent(holdPosition.transform);
            pickableObject.transform.localPosition = Vector3.zero;  // Nesneyi elde do�ru konumland�r
            pickableObject.transform.localRotation = Quaternion.identity;  // Gerekirse rotasyonu s�f�rla

            Debug.Log("Nesne al�nd�: " + pickableObject.name);

            // E�er nesne bir kedi ise, BoxCollider'�n�n isTrigger'�n� true yap
            if (pickableObject.CompareTag("Cat"))
            {
                BoxCollider catCollider = pickableObject.GetComponent<BoxCollider>();
                if (catCollider != null)
                {
                    catCollider.isTrigger = true;
                }
                Invoke("DropObject", 5f);
            }

            // Nesne tutulurken Rigidbody'yi devre d��� b�rak
            Rigidbody rb = pickableObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }
        }
    }

    void DropObject()
    {
        if (pickableObject != null)
        {
            isHoldingObject = false;

            // Nesneyi oyuncunun elinden b�rak
            pickableObject.transform.SetParent(null);

            // E�er nesne bir kedi ise, BoxCollider'�n�n isTrigger'�n� false yap
            if (pickableObject.CompareTag("Cat"))
            {
                BoxCollider catCollider = pickableObject.GetComponent<BoxCollider>();
                if (catCollider != null)
                {
                    catCollider.isTrigger = false;
                }

                // targetPosition'a olan mesafeyi kontrol et
                if (targetPosition != null && Vector3.Distance(pickableObject.transform.position, targetPosition.transform.position) <= placeRange)
                {
                    pickableObject.transform.position = targetPosition.transform.position;
                    pickableObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                    Debug.Log("Kedi targetPosition'a yerle�tirildi: " + pickableObject.name);
                }
                else
                {
                    // Kediyi zemine yerle�tir
                    RaycastHit hit;
                    if (Physics.Raycast(pickableObject.transform.position, Vector3.down, out hit))
                    {
                        float groundDistance = hit.distance;
                        if (groundDistance > 0.5f)
                        {
                            // Kediyi zemine ta��
                            pickableObject.transform.position = new Vector3(pickableObject.transform.position.x, pickableObject.transform.position.y - groundDistance + 0.5f, pickableObject.transform.position.z);
                        }
                    }
                }
            }

            // Rigidbody'yi yeniden etkinle�tir
            Rigidbody rb = pickableObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }

            Debug.Log("Nesne b�rak�ld�: " + pickableObject.name);
            pickableObject = null;  // B�rak�lan nesneye olan referans� temizle
        }
    }
}
