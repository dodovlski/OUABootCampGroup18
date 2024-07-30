using UnityEngine;

public class CageSpawner : MonoBehaviour
{
    public GameObject cagePrefab; // Kafes prefab referans�
    public Vector3[] spawnPositions; // Spawn pozisyonlar�n� saklamak i�in dizi

    private bool isCageSpawned = false; // Kafesin spawnlan�p spawnlanmad���n� kontrol etmek i�in bayrak

    void Start()
    {
        SpawnCage();
    }

    public void SpawnCage()
    {
        if (!isCageSpawned)
        {
            int randomIndex = Random.Range(0, spawnPositions.Length); // Rastgele pozisyon se�imi
            Vector3 spawnPosition = spawnPositions[randomIndex];
            Instantiate(cagePrefab, spawnPosition, Quaternion.identity);
            isCageSpawned = true; // Kafesin spawnland���n� belirtir
        }
    }
}
