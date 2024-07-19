using UnityEngine;

public class ThrowableObject : MonoBehaviour
{
    public GameObject collisionVFX; // �arp��ma VFX prefab'i
    public float vfxLifetime = 2f; // VFX'in sahnede kalma s�resi

    private void OnCollisionEnter(Collision collision)
    {
        // �arp��ma noktas�nda VFX olu�turma
        if (collisionVFX != null)
        {
            GameObject vfxInstance = Instantiate(collisionVFX, collision.contacts[0].point, Quaternion.identity);
            Destroy(vfxInstance, vfxLifetime); // VFX'i belirli bir s�re sonra yok et
        }

        // Nesneyi yok et
        Destroy(gameObject);
    }
}
