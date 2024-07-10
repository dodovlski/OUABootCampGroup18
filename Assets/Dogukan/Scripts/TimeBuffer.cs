using UnityEngine;

public class TimeBuffer : MonoBehaviour
{
    public float timeBonus = 10f; // Eklenecek s�re

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cat"))
        {
            TimeCounter timeCounter = FindObjectOfType<TimeCounter>();
            if (timeCounter != null)
            {
                timeCounter.AddTime(timeBonus);
            }
            Destroy(gameObject); // Buffer� yok et
        }
    }
}
