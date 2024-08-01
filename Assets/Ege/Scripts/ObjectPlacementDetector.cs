using UnityEngine;

public class ObjectPlacementDetector : MonoBehaviour
{
    public MessBarController messBarController; // Da��n�kl�k bar� kontrolc�s�
    public float messAmount = 0.1f; // Da��n�kl�k bar�n�n azalaca�� miktar

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            ItemFallTracker fallTracker = other.GetComponent<ItemFallTracker>();
            if (fallTracker != null && fallTracker.hasFallen && !fallTracker.isPlaced)
            {
                messBarController.DecreaseMessBar(messAmount);
                fallTracker.isPlaced = true; // E�yan�n yerle�tirildi�ini i�aretle
                fallTracker.hasFallen = false; // E�yan�n d��t���n� resetle
            }
        }
    }
}
