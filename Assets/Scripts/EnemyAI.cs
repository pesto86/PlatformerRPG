using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Rigidbody2D rb;
    public CircleCollider2D attackRadius;
    public Transform player;
    public float moveSpeed;
    public float patrolSpeed = 2f;
    public float attackSpeed = 3f;
    Vector2 movement;
    bool patrolActive = true;
    bool movingRight;
    float secondsPassed;
    int movementCounter;
    bool attackMode;
    bool returnMode;
    private Vector2 anchorPoint;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anchorPoint = transform.position;
    }

    void Patrol()
    {
        movement.x = movementCounter >= 5 ? -1f : 1f;
        secondsPassed += Time.deltaTime;
        movementCounter = Mathf.FloorToInt(secondsPassed);
        // Debug.Log(movementCounter);
        if (movementCounter == 10)
        {
            secondsPassed = 0;
            movementCounter = 0;
        }
        moveSpeed = patrolSpeed;
        rb.linearVelocity = new Vector2(movement.x * moveSpeed, rb.linearVelocityY);

    }

    void Charge()
    {
        float distance = player.position.x - transform.position.x;
        float attackDirection = Mathf.Sign(player.position.x - transform.position.x);
        
        if (distance > 1 || distance < -1)
        {
            movement.x = attackDirection;
            // Debug.Log("Enemy is moving towards you");
        }

        if (distance <= 1 && distance >= -1)
        {
            movement.x = 0f;
            Attack();

        }

        moveSpeed = attackSpeed;
        rb.linearVelocity = new Vector2(movement.x * moveSpeed, rb.linearVelocityY);
    
    
    }

    void Attack()
    {
        // Debug.Log("Enemy is stopped");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            patrolActive = false;
            attackMode = true;
            returnMode = false;
        }
    }

    void Return()
    {
        Debug.Log("Return has been called");
        movementCounter = 0;
        secondsPassed = 0;
        Vector2 currentPosition = transform.position;
        Vector2 distanceFromAnchor = anchorPoint - currentPosition;
        bool returnNeeded = false;

        if (distanceFromAnchor.x > 0.1f || distanceFromAnchor.x < -0.1f)
        {
            returnNeeded = true;
        }
        else
        {
            returnNeeded = false;
        }

        movement.x = Mathf.Sign(distanceFromAnchor.x);

        moveSpeed = patrolSpeed;

        rb.linearVelocity = new Vector2(movement.x * moveSpeed, rb.linearVelocityY);

        if (returnNeeded == false)
        {
            returnMode = false;
            patrolActive = true;
        }

    }

    void OnTriggerExit2D(Collider2D other)
    { 
        if (other.CompareTag("Player"))
        {
            patrolActive = false;
            attackMode = false;
            returnMode = true;
        }
    }

    
    void Update()
    {
        if (patrolActive == true)
        {
            Patrol();
            // Debug.Log("Patrol mode active");
        }

        if (attackMode == true)
        {
            Charge();
            //Debug.Log("Charge mode active");
        }

        

        if (returnMode == true)
        {
            Return();
        }

        
    }

}
