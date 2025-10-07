using UnityEngine;

public class HealthPotion : Item
{
    public int healAmount = 25;

    public override void Interact(GameObject target)
    {
        Player player = target.GetComponent<Player>();
        if (player != null)
        {
            player.playerHp += healAmount;
            Debug.Log("Jugador curado +" + healAmount);
            Destroy(gameObject);
        }
    }
}