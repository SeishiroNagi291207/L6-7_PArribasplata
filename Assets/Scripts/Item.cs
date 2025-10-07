using UnityEngine;

public abstract class Item : Entity, IInteractable
{
    public string itemName;

    public abstract void Interact(GameObject target);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Interact(collision.gameObject);
        }
    }
}