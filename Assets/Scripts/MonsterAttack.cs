using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    [SerializeField] private float damage = 2f;
    [SerializeField] private float attackCooldown = 1f;

    private bool InRange { get; set; }
    private PlayerController player = null;
    private bool IsReady { get; set; } = true;

    private void Update()
    {
        if(IsReady)
        {
            Attack();
        }
    }

    public void Attack()
    {
        if (player == null) return;
        player.TakeDamage(damage);
        IsReady = false;
        StartCoroutine(Cooldown(attackCooldown));
    }

    IEnumerator Cooldown(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        IsReady = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            InRange = true;
            player = other.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            InRange = false;
            player = null;
        }
    }
}
