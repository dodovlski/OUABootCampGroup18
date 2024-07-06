using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAttack : MonoBehaviour
{
    public float attackRange = 1f; // Sald�r� menzili
    public LayerMask interactableLayer; // Etkile�imli objeleri tan�mlamak i�in katman

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Sol mouse t�klamas�yla sald�r
        {
            Attack();
        }
    }

    void Attack()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange, interactableLayer))
        {
            Debug.Log("Hit " + hit.collider.name);
            // Burada hasar vermeyi kald�rd�k, sadece vurma i�lemini logluyoruz.
        }
    }
}