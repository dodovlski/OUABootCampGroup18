using UnityEngine;
using UnityEngine.AI;

public class NPCAI : MonoBehaviour
{
    public NavMeshAgent agent; // NavMeshAgent bile�eni
    public Transform target; // Hedef nokta (Kedi veya belirli bir nokta)
    public float walkSpeedThreshold = 1.5f; // Y�r�y�� h�z� e�i�i
    public float runSpeedThreshold = 2f; // Ko�ma h�z� e�i�i
    public Animator animator; // Animator bile�eni

    private void Start()
    {
        if (agent == null) agent = GetComponent<NavMeshAgent>();
        if (animator == null) animator = GetComponent<Animator>();

        // Ba�lang��ta hedef olarak kedi set edilebilir veya ba�ka bir hedef verilebilir
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    private void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);

            // H�z hesaplama
            float speed = agent.velocity.magnitude;

            // Animasyon durumlar�n� belirleme
            if (speed <= 0.1f)
            {
                animator.SetFloat("Speed", 0f);
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", false);
            }
            else if (speed > walkSpeedThreshold && speed <= runSpeedThreshold)
            {
                animator.SetFloat("Speed", speed);
                animator.SetBool("isWalking", true);
                animator.SetBool("isRunning", false);
            }
            else if (speed > runSpeedThreshold)
            {
                animator.SetFloat("Speed", speed);
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", true);
            }
        }
    }
}
