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
    private float spaceCounterHeld;
    private float spaceCounterMax = 0.2f;
    private float t;
    private float maxJumpForce = 9f;
    private float minJumpForce = 8f;
    private int jumpCounter;
    private float direction;
    private Vector2 baseScale;

    public Vector2 movement;
    public Vector3 playerLocation;

    void Awake()
    {
        baseScale = transform.localScale;
    }

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
            Debug.Log(jumpCounter);
            // script for jumping - checks if player is grounded then adds 1 to jumpCounter
        }

        else if (Input.GetKeyDown("space") && jumpCounter == 1)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, minJumpForce);
            isJumping = true;
            spaceCounter = 0f;
            jumpCounter++;
            Debug.Log(jumpCounter);
            // script for double jump - checks if space is pressed and jumpCounter is 1 - then adds 1 to jumpCounter
        }
            
        

        if (isJumping && Input.GetKey("space") && rb.linearVelocityY > 1f)
        {
            spaceCounter += Time.deltaTime;
            spaceCounterHeld = Mathf.Min(spaceCounter, spaceCounterMax);
            t = spaceCounterHeld / spaceCounterMax;

            float targetY = Mathf.Lerp(minJumpForce, maxJumpForce, t);

            if (rb.linearVelocityY < targetY)
            {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, targetY);
            }
            //Debug.Log(targetY);
        }

        if (spaceCounter >= spaceCounterMax)
        {
            isJumping = false;
        }

        if (Input.GetKeyUp("space"))
            {
                isJumping = false;
                spaceCounter = 0;
                Debug.Log("space key has been released");
            }

        rb.linearVelocity = new Vector2(movement.x * moveSpeed, rb.linearVelocity.y);
        playerLocation = new Vector3(rb.transform.position.x, rb.transform.position.y, 0);

        direction = Mathf.Sign(movement.x);
        
        if ((Mathf.Sign(transform.localScale.x) != direction) && movement.x != 0)
        {
            Flip();
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
    
    void Flip()
    {
        Debug.Log("Player did a flip");
        transform.localScale = new Vector2(direction * baseScale.x, baseScale.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Ground"))
            {
                isGrounded = true;
                isJumping = false;
                jumpCounter = 0;
                Debug.Log("Player is grounded");
                // jumpCounter set to 0 to allow double jump on the next jump
            }
        }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
            jumpCounter++;
            Debug.Log("Player is NOT grounded");
        }
    }

    void FixedUpdate()
    {
        // Movement
        
    }
}
