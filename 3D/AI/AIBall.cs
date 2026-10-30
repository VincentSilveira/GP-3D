using UnityEngine;
using UnityEngine.AI;

public class AIBall : MonoBehaviour
{
    public Transform player;
    //public float speed = 5f;

    //private Rigidbody rb;

    private NavMeshAgent agent;

    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    void FixedUpdate()
    {
        //if (player == null) return;

        //Vector3 direction = player.position - transform.position;
        //direction.y = 0f;

        //rb.linearVelocity = new Vector3(direction.x * speed,
        //rb.linearVelocity.y,
        //direction.z * speed);
        agent.SetDestination(player.position);
    }
}
