using UnityEngine;

public class ObjectFallDetector : MonoBehaviour
{
    public MessBarController messBarController; // Da��n�kl�k bar� kontrolc�s�
    public float messAmount = 0.1f; // Da��n�kl�k bar�n�n artaca�� miktar

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            ItemFallTracker fallTracker = other.GetComponent<ItemFallTracker>();
            if (fallTracker != null)
            {
                if (!fallTracker.hasFallen)
                {
                    messBarController.IncreaseMessBar(messAmount);
                    fallTracker.hasFallen = true; // E�yan�n yere d��t���n� i�aretle
                    fallTracker.isPlaced = false; // E�yan�n yerle�tirildi�ini resetle
                }
            }
        }
    }
}
