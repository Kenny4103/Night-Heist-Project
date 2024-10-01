using UnityEngine;

public class MainGuardPatrol : MonoBehaviour
{
    public float moveSpeed = 2f;          
    public Transform[] patrolPoints;       
    private int currentPointIndex = 0;     
    private Rigidbody2D rb;                
    private Animator animator;             
    private Vector2 movementDirection;     

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (patrolPoints.Length > 0)
        {
            transform.position = patrolPoints[0].position;
        }
    }

    void FixedUpdate()
    {
        if (patrolPoints.Length > 0)
        {
            Vector2 targetPosition = patrolPoints[currentPointIndex].position;
            movementDirection = (targetPosition - rb.position).normalized; // Calculate the direction of movement

            Vector2 newPosition = Vector2.MoveTowards(rb.position, targetPosition, moveSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPosition);

            if (Vector2.Distance(rb.position, targetPosition) < 0.1f)
            {
                currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length; // Loop back to the first point
            }

            UpdateAnimation();
        }
    }

    void UpdateAnimation()
    {
        float moveX = movementDirection.x;
        float moveY = movementDirection.y;

        if (Mathf.Abs(moveX) > Mathf.Abs(moveY)) // Moving horizontally
        {
            if (moveX > 0) // Moving right
            {
                animator.Play("MoveRight");
            }
            else if (moveX < 0) // Moving left
            {
                animator.Play("MoveLeft");
            }
        }
        else // Moving vertically
        {
            if (moveY > 0) // Moving up
            {
                animator.Play("MoveUp");
            }
            else if (moveY < 0) // Moving down
            {
                animator.Play("MoveDown");
            }
        }
    }
}