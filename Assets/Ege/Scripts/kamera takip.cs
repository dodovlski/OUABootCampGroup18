using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KameraTakip: MonoBehaviour
{
    public Transform target; // Takip edilecek karakter
    public Vector3 offset; // Kamera ile karakter aras�ndaki mesafe

    void Start()
    {
        // Ba�lang��ta kamera ve karakter aras�ndaki mesafeyi hesapla
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        // Karakterin pozisyonunu al ve offset'i ekleyerek kameray� yeni pozisyona ayarla
        Vector3 newPosition = target.position + offset;
        transform.position = newPosition;
    }
}
