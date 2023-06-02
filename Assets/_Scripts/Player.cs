
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : Singleton<Player>
{
    Rigidbody2D body;

    Animator anim;
    SpriteRenderer spriteRenderer;

    public Vector2 inputVec;
    [SerializeField]
    float moveSpeed;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        if (inputVec.x != 0)
            spriteRenderer.flipX = inputVec.x < 0 ? true : false;

        anim.SetFloat("Horizontal", inputVec.x);
        anim.SetFloat("Vertical", inputVec.y);
    }

    private void FixedUpdate()
    {
        body.MovePosition(body.position + inputVec * moveSpeed * Time.fixedDeltaTime);
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        inputVec = context.ReadValue<Vector2>();
    }
}
