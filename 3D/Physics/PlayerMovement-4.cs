using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement4 : MonoBehaviour
{
    private Rigidbody rb;
    private float x;
    private float y;

    [SerializeField] private float speed = 10;
    [SerializeField] private float jumpForce = 5;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }


    public void OnMove(InputValue move)
    {
        Vector2 moveVector = move.Get<Vector2>();

        x = moveVector.x;
        y = moveVector.y;

    }


    public void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 move = new Vector3(x, 0f ,y);
        //rb.linearVelocity = new Vector3(
        //    move.x * speed,
        //    rb.linearVelocity.y,
        //    move.z * speed
        //);

        rb.AddForce(speed * move, ForceMode.Impulse);
    }
}
