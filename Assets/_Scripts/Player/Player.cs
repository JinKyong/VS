using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : Singleton<Player>
{
    Rigidbody2D body;
    Animator anim;
    SpriteRenderer spriteRenderer;

    public Vector2 inputVec;
    [SerializeField] PlayerStat playerStat;
    public PlayerStat Stat { get { return playerStat; } }

    [Space]
    [Header("Recognition")]
    [SerializeField] float recogRange;
    [SerializeField] LayerMask targetLayer;
    [SerializeField] RaycastHit2D[] targets;
    [SerializeField] LayerMask expLayer;

    [Space]
    [Header("Event")]
    [SerializeField] GameEvent levelEvent;
    [SerializeField] GameEvent expEvent;
    [SerializeField] GameEvent coinEvent;
    [SerializeField] GameEvent healEvent;
    [SerializeField] GameEvent hurtEvent;

    private void Awake()
    {
        playerStat.GameStart();
    }

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //체력 리젠
        if(playerStat.health < playerStat.maxHealth && playerStat.healthRegen > 0)
        {
            UpdateHP(playerStat.healthRegen * Time.deltaTime);
        }
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

        //Enemy Search
        targets = Physics2D.CircleCastAll(transform.position, recogRange, Vector2.zero, 0, targetLayer);
        //Exp Search
        var colls = Physics2D.OverlapBoxAll(transform.position, 
            new Vector2(Stat.MagnetSize, Stat.MagnetSize), 0, expLayer);
        foreach(var c in colls)
            c.GetComponent<Exp>().OnTarget();
    }
    public Transform GetNearTarget()
    {
        //감지된 타겟중 가장 가까운 타겟 하나 반환
        if (targets.Length <= 0) return null;

        Array.Sort(targets, (x, y) => Vector2.Distance(transform.position, x.transform.position).CompareTo(
            Vector2.Distance(transform.position, y.transform.position)));

        return targets[0].transform;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        inputVec = context.ReadValue<Vector2>();
    }

    public void GetExp(int value)
    {
        playerStat.exp += value;
        if(playerStat.exp >= playerStat.MaxEXP)
        {
            playerStat.exp -= playerStat.MaxEXP;
            LevelUP();
        }

        expEvent.Raise();
    }
    public void GetCoin(int value)
    {
        playerStat.coin += value;
        coinEvent.Raise();
    }
    public void LevelUP()
    {
        playerStat.level++;
        levelEvent.Raise();
    }
    public void UpdateHP(float value)
    {
        playerStat.health += value;
        if (playerStat.health > playerStat.maxHealth) playerStat.health = playerStat.maxHealth;
        else if (playerStat.health <= 0) GameManager.Instance.FinishGame(false);

        if (value < 0) hurtEvent.Raise();
        else healEvent.Raise();
    }
}