using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float moveSpeed = 3f;
    public Rigidbody2D rb;
    public Animator animator;
    private float jumpForce;
    public float defaultJumpForce = 2f;
    private bool isGrounded = false;
    private bool isJumping = false;
    private float spaceCounter;
    private bool spaceHeld = false;
    private float spaceCounterHeld;
    private float spaceCounterMax = 0.2f;
    private float spaceMultiplier;
    private float maxJumpForce = 5.8f;
    private float minJumpForce = 5f;


    Vector2 movement;

    void Update()
    {
        // Input
        movement.x = Input.GetAxisRaw("Horizontal");

        

        // Are we moving?
        bool isMoving = movement.sqrMagnitude > 0.01f;
        
        // Tell animator
        // animator.SetBool("IsMoving", isMoving);

        if (Input.GetKeyDown("space") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, minJumpForce);
            isJumping = true;
            spaceCounter = 0f;
            
        }

        if (isJumping && Input.GetKey("space") && rb.linearVelocityY > 1f)
        {
            spaceCounter += Time.deltaTime;
            spaceCounterHeld = Mathf.Min(spaceCounter, spaceCounterMax);
            spaceMultiplier = spaceCounterHeld / spaceCounterMax;

            float targetY = minJumpForce + spaceMultiplier * (maxJumpForce - minJumpForce);

            if (rb.linearVelocityY < targetY)
            {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, targetY);
            }
            Debug.Log(targetY);
        }

        if (spaceCounter >= spaceCounterMax)
        {
            isJumping = false;
        }

        if (Input.GetKeyUp("space"))
        {
            spaceHeld = false;
            isJumping = false;
            spaceCounter = 0;
            Debug.Log("space key has been released");
        }

        
    }

   
    void Jump()
    {
        /*if (Input.GetKey("space"))
        {
            spaceCounter += Time.deltaTime;
            jumpForce = defaultJumpForce * Mathf.Min(spaceCounter + 1f, 5f); 
            Debug.Log("space is being held down");
            
        }*/
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        Debug.Log("Jump has been called");
    }

    void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Ground"))
            {
                isGrounded = true;
                isJumping = false;
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
