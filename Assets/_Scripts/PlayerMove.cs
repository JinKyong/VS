using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMove : MonoBehaviour
{
    Vector2 inputVec;
    Rigidbody2D body;

    [SerializeField]
    float moveSpeed;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        body.MovePosition(body.position + inputVec * moveSpeed * Time.deltaTime);
    }

    public void KeySetting()
    {
        Event keyEvent = Event.current;
    }
}
