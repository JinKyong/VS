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
    [SerializeField] PlayerStat playerStat;
    public PlayerStat Stat { get { return playerStat; } }

    [Space]
    [SerializeField] GameEvent levelEvent;
    [SerializeField] GameEvent expEvent;
    [SerializeField] GameEvent healEvent;
    [SerializeField] GameEvent hurtEvent;

    private void Awake()
    {
        playerStat.Init();
    }

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
        body.MovePosition(body.position + inputVec * playerStat.Speed * Time.fixedDeltaTime);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        inputVec = context.ReadValue<Vector2>();
    }

    public void GetExp(int value)
    {
        playerStat.GetExp(value);
        if(playerStat.exp >= playerStat.MaxEXP)
        {
            playerStat.exp -= playerStat.MaxEXP;
            LevelUP();
        }

        expEvent.Raise();
    }
    public void LevelUP()
    {
        playerStat.level++;
        levelEvent.Raise();
    }
    public void UpdateHP(int value)
    {
        if (value < 0) hurtEvent.Raise();
        else healEvent.Raise();

        playerStat.health += value;
        if (playerStat.health > playerStat.maxHealth) playerStat.health = playerStat.maxHealth;
        else if (playerStat.health <= 0) GameManager.Instance.FinishGame(false);
    }
}