using UnityEngine;

public interface IDamageable
{
    void TakeDamage(int damage, Vector3 origin);
}

public interface IAttack
{
    void Attack();
}

public interface IDropItem
{
    void DropItem();
}

public interface IConsumable
{
    void Consume(GameObject target);
}

public interface IApplyBuff
{
    void ApplyBuff(GameObject target);
}