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
    private GameObject initialPositionObject; // Nesnenin ilk konumunu saklayan bo� obje
    private Collider[] colliders;
    private bool isHoldingObject = false;
    private Marker marker; // Marker script'ine referans

    void Start()
    {
        // Marker referans�n� al
        marker = FindObjectOfType<Marker>();

        if (marker == null)
        {
            Debug.LogError("Marker script not found in the scene.");
        }
    }

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
                    // Collider'�n "Pickable", "Cat" veya "mug" tag'ine sahip olup olmad���n� kontrol et
                    if (collider.CompareTag("Pickable") || collider.CompareTag("Cat") || collider.CompareTag("mug"))
                    {
                        pickableObject = collider.gameObject;
                        initialPositionObject = GameObject.Find(pickableObject.tag + "Konum"); // �lk konumu saklayan objeyi bul

                        if (initialPositionObject == null)
                        {
                            Debug.LogError("Initial position object not found for tag: " + pickableObject.tag);
                        }
                        else
                        {
                            Debug.Log("Initial position object found: " + initialPositionObject.name);
                        }

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

            // E�er nesne bir kedi, "Pickable" veya "mug" ise, BoxCollider'�n�n isTrigger'�n� true yap
            if (pickableObject.CompareTag("Cat") || pickableObject.CompareTag("Pickable") || pickableObject.CompareTag("mug"))
            {
                BoxCollider objectCollider = pickableObject.GetComponent<BoxCollider>();
                if (objectCollider != null)
                {
                    objectCollider.isTrigger = true;
                }

                // E�er nesne bir kedi ise, 5 saniye sonra b�rak
                if (pickableObject.CompareTag("Cat"))
                {
                    Invoke("DropObject", 5f);
                }
            }

            // Nesne tutulurken Rigidbody'yi devre d��� b�rak
            Rigidbody rb = pickableObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            // Marker script'ine initialPosition'� ve target'� ayarla ve marker'� g�r�n�r yap
            if (marker != null)
            {
                marker.initialPosition = initialPositionObject?.transform;
                marker.target = initialPositionObject?.transform;
                marker.img.gameObject.SetActive(true); // Marker'� g�r�n�r yap
                Debug.Log("Marker initialPosition set to: " + marker.initialPosition?.name);
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

            // E�er nesne bir kedi, "Pickable" veya "mug" ise, BoxCollider'�n�n isTrigger'�n� false yap
            if (pickableObject.CompareTag("Cat") || pickableObject.CompareTag("Pickable") || pickableObject.CompareTag("mug"))
            {
                BoxCollider objectCollider = pickableObject.GetComponent<BoxCollider>();
                if (objectCollider != null)
                {
                    objectCollider.isTrigger = false;
                }

                // initialPositionObject'e olan mesafeyi kontrol et
                if (initialPositionObject != null && Vector3.Distance(pickableObject.transform.position, initialPositionObject.transform.position) <= placeRange)
                {
                    pickableObject.transform.position = initialPositionObject.transform.position;
                    pickableObject.transform.rotation = initialPositionObject.transform.rotation;
                    Debug.Log("Nesne initialPositionObject'e yerle�tirildi: " + pickableObject.name);
                }
                else
                {
                    // Nesneyi zemine yerle�tir
                    RaycastHit hit;
                    if (Physics.Raycast(pickableObject.transform.position, Vector3.down, out hit))
                    {
                        float groundDistance = hit.distance;
                        if (groundDistance > 0.5f)
                        {
                            // Nesneyi zemine ta��
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
            initialPositionObject = null; // �lk konum referans�n� temizle

            // Marker'� gizle
            if (marker != null)
            {
                marker.img.gameObject.SetActive(false);
                marker.target = null;
            }
        }
    }
}
