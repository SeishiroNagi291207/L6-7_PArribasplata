using UnityEngine;

public class Enemy_Melee : MonoBehaviour
{
    public float speed = 2f;
    public float attackRange = 1.5f;
    public int damage = 10;
    public float attackCooldown = 2f;
    public float stopTimeAfterAttack = 1f;
    public float detectionRange = 6f;
    public float knockbackForce = 0f;

    private Transform player;
    private bool canAttack = true;
    private bool isStopped = false;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (!isStopped && distance < detectionRange)
        {
            if (distance > attackRange)
            {
                Vector2 dir = (player.position - transform.position).normalized;
                transform.position += (Vector3)dir * speed * Time.deltaTime;
            }
            else if (canAttack)
            {
                Attack();
            }
        }
    }
    private void Attack()
    {
        canAttack = false;
        isStopped = true;
        Debug.Log($"{gameObject.name} atacó al jugador!");

        Player playerScript = player.GetComponent<Player>();
        if (playerScript != null)
        {
            playerScript.TakeDamage(damage, transform.position);

            // Si tiene knockback
            if (knockbackForce > 0)
            {
                Vector2 dir = (player.position - transform.position).normalized;
                Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
                if (playerRb != null)
                {
                    playerRb.AddForce(dir * knockbackForce, ForceMode2D.Impulse);
                }
            }
        }
        Invoke(nameof(ResumeMovement), stopTimeAfterAttack);
        Invoke(nameof(ResetAttack), attackCooldown);
    }
    private void ResumeMovement()
    {
        isStopped = false;
    }
    private void ResetAttack()
    {
        canAttack = true;
    }
}