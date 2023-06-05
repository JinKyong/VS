using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : Singleton<Player>
{
    Rigidbody2D body;
    public Recognition recognition;

    Animator anim;
    SpriteRenderer spriteRenderer;

    public Vector2 inputVec;
    [SerializeField] float moveSpeed;

    [SerializeField] Transform weaponTR;
    List<Weapon> weapons;

    //[Space]
    //[Header("Stat")]
    int level;
    int exp;
    int maxExp;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        recognition = GetComponentInChildren<Recognition>();

        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        weapons = new List<Weapon>();

        level = 1;
        exp = 0;
        maxExp = 10;
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

    private Weapon searchInven(Weapon weapon)
    {
        foreach (var w in weapons)
        {
            if (w.GetType().Equals(weapon.GetType()))
            {
                return w;
            }
        }

        return null;
    }
    public bool AddWeapon(Weapon weapon)
    {
        Weapon w = searchInven(weapon);

        if (w is null)
        {
            weapon.transform.SetParent(weaponTR);
            weapon.transform.localPosition = Vector2.zero;

            weapons.Add(weapon);
            weapon.Init();

            return true;
        }
        else
        {
            w.Levelup();

            return false;
        }
    }

    public void GetExp(int value)
    {
        exp += value;
        if(maxExp <= exp)
        {
            levelUp();
            exp -= maxExp;
        }
    }
    private void levelUp()
    {
        level++;
        //ui켜서 하나 선택하기
    }
}