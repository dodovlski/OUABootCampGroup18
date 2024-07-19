using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 150f; // D�nd�rme h�z� (derece/saniye)

    void Update()
    {
        // Global Y ekseninde d�n��
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
    }
}
