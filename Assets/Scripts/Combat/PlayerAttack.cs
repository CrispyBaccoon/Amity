using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
  public Animator animator;
  public Transform attackPoint;
  public float attackRange = 0.5f;
  public LayerMask enemyLayers;

  void Attack()
  {
    // animator.SetTrigger("Attack"); // Add Attack animation

    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

    foreach (Collider2D enemy in hitEnemies)
    {
      Debug.Log("enemy hit: " + enemy.name);
    }
  }
}