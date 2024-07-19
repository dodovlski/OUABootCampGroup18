using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufferSpawner : MonoBehaviour
{
    public GameObject[] bufferPrefabs; // Buffer prefablar�n� burada tutuyoruz
    public Vector3 spawnAreaMin;
    public Vector3 spawnAreaMax;
    public float minSpawnInterval = 10f;
    public float maxSpawnInterval = 60f;

    private void Start()
    {
        StartCoroutine(SpawnBuffer());
    }

    private IEnumerator SpawnBuffer()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));

            Vector3 spawnPosition = new Vector3(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                Random.Range(spawnAreaMin.z, spawnAreaMax.z)
            );

            int randomIndex = Random.Range(0, bufferPrefabs.Length);

            // bufferPrefabs dizisindeki prefab'lerin ge�erli olup olmad���n� kontrol edin
            if (bufferPrefabs[randomIndex] != null)
            {
                Instantiate(bufferPrefabs[randomIndex], spawnPosition, Quaternion.Euler(new Vector3(30, 0, 0)));
            }
            else
            {
                Debug.LogWarning("Index " + randomIndex + " deki buffer prefab eksik veya yok edildi.");
            }
        }
    }
}
