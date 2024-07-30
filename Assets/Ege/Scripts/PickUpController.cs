using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PickUpController : MonoBehaviour
{
    public GameObject holdPosition;
    public GameObject cagePosition; // Kafes pozisyonu
    public float pickUpRange = 2f;  // Kediyi alma mesafesi
    public float placeRange = 2f;   // Yerle�tirme mesafesi
    private GameObject pickableObject;
    private bool isHoldingObject = false;

    void Update()
    {
        // E�er kedi tutulmuyorsa ve yak�nda bir kedi varsa, onu al
        if (!isHoldingObject)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, pickUpRange);

            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Cat"))
                {
                    pickableObject = collider.gameObject;
                    PickUp();
                    break;
                }
            }
        }

        // E�er bir nesne tutuluyorsa, nesnenin pozisyonunu holdPosition'a e�itle
        if (isHoldingObject && pickableObject != null)
        {
            pickableObject.transform.position = holdPosition.transform.position;
            pickableObject.transform.rotation = holdPosition.transform.rotation;

            // Kafese yak�nsa kediyi b�rak
            if (Vector3.Distance(transform.position, cagePosition.transform.position) <= placeRange)
            {
                DropObject();
            }
        }
    }

    public void PickUp()
    {
        if (pickableObject != null)
        {
            isHoldingObject = true;

            // Kediyi oyuncunun eline ili�tir
            pickableObject.transform.SetParent(holdPosition.transform);
            pickableObject.transform.localPosition = Vector3.zero;
            pickableObject.transform.localRotation = Quaternion.identity;

            Debug.Log("Kedi al�nd�: " + pickableObject.name);

            // Kediyi kald�r�rken Collider'� tetikleyici yap
            Collider objectCollider = pickableObject.GetComponent<Collider>();
            if (objectCollider != null)
            {
                objectCollider.isTrigger = true;
            }

            // Kediyi kald�r�rken Rigidbody'yi devre d��� b�rak
            Rigidbody rb = pickableObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            // Kediyi yakalad���nda hedefi kafes olarak de�i�tir
            GetComponent<NPCAI>().SetTarget(cagePosition.transform);
        }
    }

    void DropObject()
    {
        if (pickableObject != null)
        {
            isHoldingObject = false;

            // Kediyi oyuncunun elinden b�rak
            pickableObject.transform.SetParent(null);

            // Kedinin Collider'�n� tetikleyici yapma
            Collider objectCollider = pickableObject.GetComponent<Collider>();
            if (objectCollider != null)
            {
                objectCollider.isTrigger = false;
            }

            // Kafes pozisyonuna kediyi yerle�tir
            if (cagePosition != null && Vector3.Distance(pickableObject.transform.position, cagePosition.transform.position) <= placeRange)
            {
                pickableObject.transform.position = cagePosition.transform.position;
                pickableObject.transform.rotation = cagePosition.transform.rotation;
                Debug.Log("Kedi kafese yerle�tirildi: " + pickableObject.name);
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

            // Rigidbody'yi yeniden etkinle�tir
            Rigidbody rb = pickableObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }

            Debug.Log("Kedi b�rak�ld�: " + pickableObject.name);
            pickableObject = null;  // B�rak�lan nesneye olan referans� temizle
        }
    }
}
