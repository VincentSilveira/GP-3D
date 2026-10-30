using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private float x;
    private float y;

    [SerializeField] private float speed = 0;

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
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 move = new Vector3(x, 0f ,y);
        //rb.linearVelocity = new Vector3(
        //    move.x * speed,
        //    rb.linearVelocity.y,
        //    move.z * speed
        //);

        rb.AddForce(speed * move);
    }
}
