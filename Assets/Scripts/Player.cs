using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity, IDamageable
{
    public float moveSpeed = 5f;
    private float baseSpeed;
    public float playerHp = 100f;
    public float attackCooldown = 0.5f;

    private Vector2 moveInput;
    private Rigidbody2D rb;
    private float nextAttackTime = 0f;

    private InputSystem_Actions inputActions;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        baseSpeed = moveSpeed;

        inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
        inputActions.Player.Attack.performed += OnAttack;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Attack.performed -= OnAttack;
        inputActions.Disable();
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector2 newPos = rb.position + moveInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPos);
    }

    private void OnAttack(InputAction.CallbackContext ctx)
    {
        Attack();
    }

    public void Attack()
    {
        if (Time.time >= nextAttackTime)
        {
            Debug.Log("¡Jugador ataca!");
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    public void TakeDamage(int damage, Vector3 origin)
    {
        playerHp -= damage;
        Debug.Log("Jugador recibió daño. Vida: " + playerHp);
    }

    public void ResetSpeed()
    {
        moveSpeed = baseSpeed;
        Debug.Log("Velocidad restaurada.");
    }
}
