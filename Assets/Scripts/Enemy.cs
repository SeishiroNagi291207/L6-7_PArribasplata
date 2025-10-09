using UnityEngine;

public class Enemy : Entity, IDamageable
{
    [SerializeField] private int enemyHP = 30;
    [SerializeField] private int damage = 10;
    [SerializeField] private float detectionRange = 6f;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float attackCooldown = 1.2f;
    [SerializeField] private float moveSpeed = 3f;

    protected Transform player;
    private float nextAttackTime;
    public int EnemyHP
    {
        get { return enemyHP; }
        set { enemyHP = Mathf.Max(0, value); }
    }
    public int Damage
    {
        get { return damage; }
        set { damage = Mathf.Max(0, value); }
    }
    public float DetectionRange
    {
        get { return detectionRange; }
        set { detectionRange = Mathf.Max(0, value); }
    }
    public float AttackRange
    {
        get { return attackRange; }
        set { attackRange = Mathf.Max(0.1f, value); }
    }
    public float AttackCooldown
    {
        get { return attackCooldown; }
        set { attackCooldown = Mathf.Max(0.1f, value); }
    }
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = Mathf.Max(0, value); }
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }
    private void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= AttackRange)
            Attack();
        else if (distance <= DetectionRange)
            FollowPlayer();
    }
    protected virtual void FollowPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position,player.position,MoveSpeed * Time.deltaTime);
    }
    protected virtual void Attack()
    {
        if (Time.time < nextAttackTime) return;

        Player Layla = player.GetComponent<Player>();
        if (Layla != null)
        {
            Layla.TakeDamage(Damage, transform.position);
        }
        nextAttackTime = Time.time + AttackCooldown;
    }
    public void TakeDamage(int damage, Vector3 origin)
    {
        EnemyHP = Mathf.Max(0, EnemyHP - damage);
        Debug.Log(gameObject.name + " recibió " + damage + ". HP restante: " + EnemyHP);

        if (EnemyHP <= 0)
            Die();
    }
    protected virtual void Die()
    {
        Debug.Log(gameObject.name + " ha muerto.");
        Destroy(gameObject);
    }
}