using System.Collections.Generic;
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

    List<Weapon> weapons;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        weapons = new List<Weapon>();
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

    public void OnMove(InputAction.CallbackContext context)
    {
        inputVec = context.ReadValue<Vector2>();
    }

    public void AddWeapon()
    {
        //1. �ִ� �������� üũ
            //1-1. �ִ� �Ÿ� �������
            //1-2. ���� �Ÿ� ���� �߰�
    }
}