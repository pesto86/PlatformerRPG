using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform player;
    public CapsuleCollider2D attackHitBox;
    public float moveSpeed;
    public float patrolSpeed = 1f;
    public float attackSpeed = 2f;
    private Vector2 movement;
    public bool patrolActive = true;
    public float secondsPassed;
    public int movementCounter;
    public bool chargeMode;
    public bool returnMode;
    public bool attackMode;
    public bool playerPresent;
    private float direction;
    private Vector2 anchorPoint;
    private bool canAttack = true;
    private Vector2 baseScale;

    void Start()
    {
        anchorPoint = transform.position;
        attackHitBox.enabled = false;
        baseScale = transform.localScale;
    }

    void Patrol()
    {
        

        movement.x = movementCounter >= 4 ? -1f : 1f;
        secondsPassed += Time.deltaTime;
        movementCounter = Mathf.FloorToInt(secondsPassed);
        // Debug.Log(movementCounter);
        if (movementCounter == 8)
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
            attackMode = false;
            // Debug.Log("Enemy is moving towards you");
        }

        if (distance <= 1 && distance >= -1)
        {
            movement.x = 0f;
            attackMode = true;
        }

        moveSpeed = attackSpeed;
        rb.linearVelocity = new Vector2(movement.x * moveSpeed, rb.linearVelocityY);
    
    
    }

    public void Attack()
    {
        if (canAttack)
        {
            canAttack = false;
            StartCoroutine(AttackRoutine()); 
        }
    }
    IEnumerator AttackRoutine()
    {
        attackHitBox.enabled = true;

        yield return new WaitForSeconds(0.2f);

        attackHitBox.enabled = false;

        yield return new WaitForSeconds(1f);

        canAttack = true;
    }
            
    
    

    void Return()
    {
        
        Debug.Log("Return has been called");
        movementCounter = 0;
        secondsPassed = 0;
        Vector2 currentPosition = transform.position;
        Vector2 distanceFromAnchor = anchorPoint - currentPosition;
        bool returnNeeded;

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
        if (!playerPresent)
        {
            rb.linearVelocity = new Vector2(movement.x * moveSpeed, rb.linearVelocityY);
        }

        if (playerPresent)
        {
            chargeMode = true;
            returnMode = false;
        }

        if (returnNeeded == false && !playerPresent)
        {
            returnMode = false;
            patrolActive = true;
        }
    }

    void Flip()
    {
        transform.localScale = new Vector2(direction * baseScale.x, baseScale.y);
        Debug.Log("Enemy did a flip");
    }
    void Update()
    {
        

        if (patrolActive == true)
        {
            Patrol();
            // Debug.Log("Patrol mode active");
        }

        if (chargeMode == true)
        {
            Charge();
            //Debug.Log("Charge mode active");
        }

        

        if (returnMode == true)
        {
            Return();
        }

        if (attackMode && canAttack)
        {
            Attack();
        }

        direction = Mathf.Sign(movement.x);

        if ((Mathf.Sign(transform.localScale.x) != direction) && movement.x != 0)
        {
            Flip();
        }
        
    }

}
