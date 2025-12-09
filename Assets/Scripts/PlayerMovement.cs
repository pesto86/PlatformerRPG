using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float moveSpeed = 3f;
    public Rigidbody2D rb;
    public Animator animator;
    public float jumpForce = 10f;
    private bool isGrounded = false;

    Vector2 movement;

    void Update()
    {
        // Input
        movement.x = Input.GetAxisRaw("Horizontal");

       

        // Are we moving?
        bool isMoving = movement.sqrMagnitude > 0.01f;

        // Tell animator
        // animator.SetBool("IsMoving", isMoving);

        if (Input.GetKeyDown("space") && isGrounded == true)
        {
            Jump();
        }
    }

   
    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        Debug.Log("Jump has been called");
    }

    void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Ground"))
            {
                isGrounded = true;
                Debug.Log("Player is grounded");
            }
        }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
            Debug.Log("Player is NOT grounded");
        }
    }

    void FixedUpdate()
    {
        // Movement
        rb.linearVelocity = new Vector2(movement.x * moveSpeed, rb.linearVelocity.y);
    }
}
