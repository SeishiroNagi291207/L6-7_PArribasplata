using UnityEngine;

public abstract class Item : MonoBehaviour, IConsumable
{
    public string itemName;

    public abstract void Consume(GameObject target);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Consume(collision.gameObject);
        }
    }
}