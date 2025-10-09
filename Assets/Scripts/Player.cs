using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity, IDamageable
{
    [SerializeField] private float moveSpeed = 5f;
    private float baseSpeed;
    [SerializeField] private float playerHp = 100f;
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private int attackDamage = 20;
    [SerializeField] private float attackRange = 1.2f;

    private Vector2 moveInput;
    private float nextAttackTime = 0f;

    private InputSystem_Actions inputActions;

    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = Mathf.Max(0, value); }
    }
    public float PlayerHp
    {
        get { return playerHp; }
        set { playerHp = Mathf.Clamp(value, 0, 100f); }
    }
    public int AttackDamage
    {
        get { return attackDamage; }
        set { attackDamage = Mathf.Max(0, value); }
    }
    public float AttackCooldown
    {
        get { return attackCooldown; }
        set { attackCooldown = Mathf.Max(0.1f, value); }
    }
    public float AttackRange
    {
        get { return attackRange; }
        set { attackRange = Mathf.Max(0.1f, value); }
    }
    private void Awake()
    {
        baseSpeed = MoveSpeed;
        inputActions = new InputSystem_Actions();
    }
    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Move.started += OnMove;
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
        inputActions.Player.Attack.started += OnAttack;
        inputActions.Player.Attack.performed += OnAttack;
        inputActions.Player.Attack.canceled += OnAttack;
    }
    private void OnDisable()
    {
        inputActions.Player.Move.started -= OnMove;
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Attack.started -= OnAttack;
        inputActions.Player.Attack.performed -= OnAttack;
        inputActions.Player.Attack.canceled -= OnAttack;
        inputActions.Disable();
    }
    private void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }
    private void Update()
    {
        Vector3 moveDir = new Vector3(moveInput.x, moveInput.y, 0);
        transform.position += moveDir * MoveSpeed * Time.deltaTime;
    }
    private void OnAttack(InputAction.CallbackContext ctx)
    {
        TryAttack();
    }
    private void TryAttack()
    {
        if (Time.time < nextAttackTime) return;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, AttackRange);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                IDamageable damageable = hit.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(AttackDamage, transform.position);
                    Debug.Log("Golpeó a " + hit.name + " por " + AttackDamage + " de daño.");
                }
            }
        }
        Debug.Log("Jugador ataca!");
        nextAttackTime = Time.time + AttackCooldown; //Time.time es una implementación simple que cuenta los segundos desde que empezó el juego.

        /*Una Manera de Implementarlo aparte de esta nueva implementación seria:
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackCooldown)
        {
        Attack();
        attackTimer = 0f;
        }
        Y simplemente agregar arriba la variable:
        private float attackTimer = 0f;
        */
    }
    public void TakeDamage(int damage, Vector3 origin)
    {
        PlayerHp -= damage;
        Debug.Log("Jugador recibió " + damage + " de daño. Vida: " + PlayerHp);

        if (PlayerHp <= 0)
            Die();
    }
    private void Die()
    {
        Debug.Log("Jugador ha muerto.");
        Destroy(gameObject);
    }
    public void ResetSpeed()
    {
        MoveSpeed = baseSpeed;
        Debug.Log("Velocidad restaurada.");
    }
}