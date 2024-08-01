using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Marker : MonoBehaviour
{
    public Image img;
    public Transform target;
    public TextMeshProUGUI meter;
    public Vector3 offset;
    public Transform initialPosition; // Nesnenin ilk konumunu saklayan obje

    void Start()
    {
        if (img == null)
        {
            Debug.LogError("Marker: img referans� atanmam��.");
            return;
        }
        if (meter == null)
        {
            Debug.LogError("Marker: meter referans� atanmam��.");
            return;
        }

        img.gameObject.SetActive(false); // Ba�lang��ta marker'� gizle
    }

    void Update()
    {
        if (img == null || target == null || initialPosition == null)
        {
            img.gameObject.SetActive(false); // E�er img, target veya initialPosition atanmad�ysa marker'� gizle
            return;
        }

        UpdateMarkerPosition();
    }

    void UpdateMarkerPosition()
    {
        if (img.gameObject.activeSelf)
        {
            float minX = img.GetPixelAdjustedRect().width / 2;
            float maxX = Screen.width - minX;
            float minY = img.GetPixelAdjustedRect().height / 2;
            float maxY = Screen.height - minY;

            Vector2 pos = Camera.main.WorldToScreenPoint(target.position + offset);

            if (Vector3.Dot((target.position - Camera.main.transform.position), Camera.main.transform.forward) < 0)
            {
                // Target is behind the player
                if (pos.x < Screen.width / 2)
                {
                    pos.x = maxX;
                }
                else
                {
                    pos.x = minX;
                }
            }

            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);
            img.transform.position = pos;

            meter.text = ((int)Vector3.Distance(initialPosition.position, target.position)).ToString() + "m"; // initialPosition'dan target'a olan mesafeyi hesapla
        }
    }

    public void SetTarget(Transform newTarget, Transform initialPos)
    {
        target = newTarget;
        initialPosition = initialPos;
        if (img != null)
        {
            img.gameObject.SetActive(true); // Marker'� g�r�n�r yap
        }
    }

    public void ClearTarget()
    {
        target = null;
        initialPosition = null;
        if (img != null)
        {
            img.gameObject.SetActive(false); // Marker'� gizle
        }
    }
}
