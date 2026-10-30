using UnityEngine;

public class AIController : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;
    public float followDistance = 1.5f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (player == null) return;

        Vector2 direction =
            (Vector2)player.position - rb.position;

        if (direction.magnitude > followDistance)
        {
            direction.Normalize();

            rb.MovePosition(
                rb.position + direction * speed * Time.fixedDeltaTime
            );
        }
    }

}
