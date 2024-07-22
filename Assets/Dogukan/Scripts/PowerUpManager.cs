using UnityEngine;
using System.Collections.Generic;

public class PowerUpManager : MonoBehaviour
{
    [System.Serializable]
    public class PowerUp
    {
        public GameObject prefab; // Power-up prefab
        public float spawnProbability; // Spawn olma olas�l���
    }

    public PowerUp[] powerUps; // Farkl� power-up'lar dizisi
    public Transform[] spawnPoints; // Spawn noktalar�
    public float spawnInterval = 10f; // Spawn s�resi

    private List<Transform> availableSpawnPoints; // Kullan�labilir spawn noktalar� listesi

    void Start()
    {
        availableSpawnPoints = new List<Transform>(spawnPoints); // T�m spawn noktalar�n� listeye ekle
        InvokeRepeating("SpawnPowerUp", 0f, spawnInterval); // Spawn i�lemini belirli aral�klarla �a��r
    }

    void SpawnPowerUp()
    {
        if (availableSpawnPoints.Count == 0) // E�er kullan�labilir spawn noktas� yoksa
        {
            availableSpawnPoints = new List<Transform>(spawnPoints); // T�m spawn noktalar�n� tekrar ekle
        }

        int spawnIndex = Random.Range(0, availableSpawnPoints.Count); // Rastgele bir kullan�labilir spawn noktas� se�
        Transform spawnPoint = availableSpawnPoints[spawnIndex];
        availableSpawnPoints.RemoveAt(spawnIndex); // Se�ilen spawn noktas�n� listeden ��kar

        GameObject selectedPowerUp = GetRandomPowerUp(); // Rastgele bir power-up se�
        if (selectedPowerUp != null)
        {
            Quaternion spawnRotation = Quaternion.Euler(30f, 0f, 0f); // Power-up'lar� 45 derece a��yla spawn et
            Instantiate(selectedPowerUp, spawnPoint.position, spawnRotation); // Power-up'� spawn et
        }
    }

    GameObject GetRandomPowerUp()
    {
        float totalProbability = 0f;
        foreach (PowerUp powerUp in powerUps)
        {
            totalProbability += powerUp.spawnProbability;
        }

        float randomPoint = Random.value * totalProbability;

        foreach (PowerUp powerUp in powerUps)
        {
            if (randomPoint < powerUp.spawnProbability)
            {
                return powerUp.prefab;
            }
            else
            {
                randomPoint -= powerUp.spawnProbability;
            }
        }
        return null;
    }
}
