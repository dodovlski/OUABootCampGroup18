using System.Collections;
using UnityEngine;

public class InvisibilityBuffer : MonoBehaviour
{
    private Renderer catRenderer;
    private Collider catCollider;

    void Start()
    {
        catRenderer = GetComponent<Renderer>();
        catCollider = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("invisibilityBuffer"))
        {
            Destroy(other.gameObject);
            StartCoroutine(BecomeInvisible());
        }
    }

    private IEnumerator BecomeInvisible()
    {
        catRenderer.enabled = false;  // Cat objesini g�r�nmez yap
        catCollider.enabled = false;  // Cat objesinin collider'�n� devre d��� b�rak
        yield return new WaitForSeconds(5);  // 5 saniye bekle
        catRenderer.enabled = true;  // Cat objesini tekrar g�r�n�r yap
        catCollider.enabled = true;  // Cat objesinin collider'�n� tekrar etkinle�tir
    }
}
