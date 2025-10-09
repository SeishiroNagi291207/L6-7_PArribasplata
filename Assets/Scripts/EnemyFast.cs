using UnityEngine;

public class EnemyFast : Enemy
{
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        MoveSpeed = 5f;
        Damage = 5;
        AttackCooldown = 0.8f;
        entityName = "Enemigo Rápido";
    }
}