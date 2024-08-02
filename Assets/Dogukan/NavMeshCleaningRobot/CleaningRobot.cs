using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CleaningRobot : MonoBehaviour
{
    public NavMeshAgent agent; // NavMeshAgent bile�eni
    public float roamRadius = 20f; // Rastgele gezinme alan� yar��ap�
    public float idleTime = 2f; // Gezindikten sonra duraklama s�resi

    private bool isRoaming = false;
    private Vector3 initialPosition;

    private void Start()
    {
        if (agent == null) agent = GetComponent<NavMeshAgent>();
        initialPosition = transform.position;
        StartCoroutine(Roam());
    }

    private void Update()
    {
        if (!isRoaming)
        {
            StartCoroutine(Roam());
        }
    }

    private IEnumerator Roam()
    {
        isRoaming = true;
        while (true)
        {
            Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
            randomDirection += initialPosition;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, roamRadius, 1);
            Vector3 finalPosition = hit.position;

            agent.SetDestination(finalPosition);

            // Ajan hedefe ula�ana kadar bekle
            while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
            {
                yield return null;
            }

            // Hedefe ula��ld�ktan sonra idle time kadar bekle
            yield return new WaitForSeconds(idleTime);
        }
    }
}
