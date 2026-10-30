
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public PathfindingManager pathfinder;

    public float speed = 2f;
    public float detectionRange = 8f;
    public float nextWaypointDistance = 0.2f;
    public float repathInterval = 0.5f;

    private Rigidbody2D rb;
    private List<Vector2> path;
    private int currentWaypoint;
    private float timer;
    private bool isChasing;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (target == null || pathfinder == null)
        {
            isChasing = false;
            path = null;
            return;
        }

        float distance = Vector2.Distance(
            transform.position, target.position);

        isChasing = distance <= detectionRange;

        if (!isChasing)
        {
            path = null;
            timer = 0f;
            return;
        }

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            List<Vector2> newPath;

            if (pathfinder.FindWorldPath(
                transform.position,
                target.position,
                out newPath))
            {
                path = newPath;
                currentWaypoint = 0;
            }
            else
            {
                path = null;
            }

            timer = repathInterval;
        }
    }

    void FixedUpdate()
    {
        if (!isChasing || path == null)
            return;

        while (currentWaypoint < path.Count &&
            Vector2.Distance(
                rb.position, path[currentWaypoint])
                <= nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (currentWaypoint >= path.Count)
            return;

        Vector2 nextPosition = Vector2.MoveTowards(
            rb.position,
            path[currentWaypoint],
            speed * Time.fixedDeltaTime
        );

        rb.MovePosition(nextPosition);
    }
}
