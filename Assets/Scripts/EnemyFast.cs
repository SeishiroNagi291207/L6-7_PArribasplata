using UnityEngine;

public class EnemyFast : Enemy
{
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        moveSpeed = 5f;
        damage = 5;
        attackCooldown = 0.8f;
        entityName = "Enemigo Rápido";
    }
}