using UnityEngine;

public class EnemyTank : Enemy
{
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        MoveSpeed = 2f;
        Damage = 20;
        AttackCooldown = 2f;
        entityName = "Enemigo Tanque";
    }
}