using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class Player : Singleton<Player>
{
    Rigidbody2D body;
    public Recognition recognition;

    Animator anim;
    SpriteRenderer spriteRenderer;

    public Vector2 inputVec;
    [SerializeField] float moveSpeed;

    [Space]
    [Header("PlayerLV")]
    public int level;
    [SerializeField] GameEvent levelEvent;
    public int exp;
    [SerializeField] int[] maxEXP;
    public int MaxEXP { get { return maxEXP[level]; } }
    [SerializeField] GameEvent expEvent;

    [Space]
    [Header("PlayerHP")]
    public int health;
    [SerializeField] int maxHealth;
    [SerializeField] GameEvent hurtEvent;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        recognition = GetComponent<Recognition>();

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

    public void OnMove(InputAction.CallbackContext context)
    {
        inputVec = context.ReadValue<Vector2>();
    }

    public void GetExp(int value)
    {
        exp += value;
        if(exp >= maxEXP[level])
        {
            exp -= maxEXP[level];
            LevelUP();
        }

        expEvent.Raise();
    }
    public void LevelUP()
    {
        level++;
        levelEvent.Raise();
    }
    public void Hurt(int value)
    {
        health -= value;
        hurtEvent.Raise();
    }
}