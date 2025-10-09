using UnityEngine;

public class HealthPotion : Item
{
    public float healAmount = 25f;

    public override void Consume(GameObject target)
    {
        Player player = target.GetComponent<Player>();
        if (player != null)
        {
            player.PlayerHp += healAmount;
            Debug.Log("Jugador curado +" + healAmount);
            Destroy(gameObject);
        }
    }
}