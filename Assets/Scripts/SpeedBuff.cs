using UnityEngine;

public class SpeedBuff : Item
{
    public float buffAmount = 3f;
    public float duration = 5f;

    public override void Consume(GameObject target)
    {
        Player player = target.GetComponent<Player>();
        if (player != null)
        {
            player.MoveSpeed += buffAmount;
            Debug.Log("Jugador más rápido por " + duration + " segundos.");
            player.Invoke(nameof(player.ResetSpeed), duration);
            Destroy(gameObject);
        }
    }
}