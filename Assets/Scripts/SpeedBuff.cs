using UnityEngine;

public class SpeedBuff : Item
{
    public float buffAmount = 2f;
    public float duration = 3f;

    public override void Interact(GameObject target)
    {
        Player player = target.GetComponent<Player>();
        if (player != null)
        {
            player.moveSpeed += buffAmount;
            Debug.Log("Velocidad aumentada por " + duration + "s");
            Destroy(gameObject);
            player.Invoke(nameof(player.ResetSpeed), duration);
        }
    }
}
