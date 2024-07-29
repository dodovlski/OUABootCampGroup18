using UnityEngine;

public class ObjectFallDetector : MonoBehaviour
{
    public MessBarController messBarController; // Da��n�kl�k bar� kontrolc�s�
    public float messAmount = 0.1f; // Da��n�kl�k bar�n�n artaca�� miktar

    void OnTriggerEnter(Collider other)
    {
        // E�er d��en obje "Furniture" etiketi ta��yorsa
        if (other.CompareTag("Furniture")|| other.CompareTag("Cat"))
        {
            ItemFallTracker fallTracker = other.GetComponent<ItemFallTracker>();
            if (fallTracker != null && !fallTracker.hasFallen)
            {
                messBarController.IncreaseMessBar(messAmount);
                fallTracker.hasFallen = true; // E�yan�n yere d��t���n� i�aretle
            }
        }
    }
}
