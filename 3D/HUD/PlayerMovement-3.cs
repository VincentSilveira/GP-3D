using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement3 : MonoBehaviour
{
    private Rigidbody rb;
    private float x;
    private float y;



    [SerializeField] private float speed = 0;

    private float rollThreshold = 0.5f;

    [SerializeField] private TMP_Text crashText;
    [SerializeField] private AudioSource wallHitSFX;
    [SerializeField]private AudioSource rollSFX;

    private int crash = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        UpdateCrashUI();

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
        rb.linearVelocity = new Vector3(
            move.x * speed,
            rb.linearVelocity.y,
            move.z * speed
        );

        if(x != 0 || y != 0)
        {
            if (!rollSFX.isPlaying)
                rollSFX.Play();
        }
        else
        {
            rollSFX.Stop();
        }
        //rb.AddForce(speed * move);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Obstacle"))
        {
            crash+=1;
            UpdateCrashUI();
            Debug.Log("Wall Hit");
            if(wallHitSFX != null && !wallHitSFX.isPlaying)
            {
                wallHitSFX.PlayOneShot(wallHitSFX.clip);
            }
        }
    }


    private void UpdateCrashUI()
    {
        if (crashText != null)
            crashText.text = "Crashes: " + crash;
    }
    
}
