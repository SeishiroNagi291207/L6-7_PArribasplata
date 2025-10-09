using UnityEngine;

public class EnemyTank : Enemy
{
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        moveSpeed = 2f;
        damage = 20;
        attackCooldown = 2f;
        entityName = "Enemigo Tanque";
    }
}