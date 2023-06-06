using UnityEngine;
using System.Collections;
using TMPro;
public class Boss4Chase : EnemyDamage
{
    [Header("Boss4Chase Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float dashSpeed; // Add this for dash speed
    [SerializeField] private LayerMask solidObjectsLayer; // LayerMask for solid objects

    private Vector3 destination;
    private float checkTimer;
    private bool attacking;
    private Transform playerTransform;
    private Rigidbody2D rb; // Rigidbody2D component

    private SpriteRenderer spriteRenderer; // SpriteRenderer component
    private Animator animator; // Animator component
    private bool isDying = false;

    public static bool isAlive = true; // Add this line
    public TMP_Text countdownText; // Add this line



    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Replace "Player" with your player's tag
        StartCoroutine(Dash()); // Start the Dash coroutine

        animator = GetComponent<Animator>(); // Get the Animator component
    }

    private void Update()
    {
        if (isDying) // Check if the boss is dying
            return; // If so, return early to prevent further actions

        // If attacking, update destination to current player's position
        if (attacking)
        {
            destination = playerTransform.position;
            Vector2 direction = (destination - transform.position).normalized;
            MoveInDirection(direction, speed);
        }
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
                CheckForPlayer();
        }
    }

    private void CheckForPlayer()
    {
        var direction = playerTransform.position - transform.position;
        direction.Normalize();

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, range, playerLayer);

        if (hit.collider != null)
        {
            attacking = true;
            checkTimer = 0;
        }
        else
        {
            attacking = false;
        }
    }

    private void Stop()
    {
        rb.velocity = Vector2.zero; // Stop the Rigidbody2D's movement
        attacking = false;
    }

    // Dash coroutine
   
    IEnumerator Dash()
    {
        while (true)
        {
            if (isDying) // Check if the boss is dying
                yield break; // If so, stop the coroutine
            // Wait for a random time between 4 and 10 seconds
            yield return new WaitForSeconds(Random.Range(3f, 7f));
            float dashTime = 1.2f; // Dash for 1.2 seconds

            // Calculate direction to player
            Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;

            // Determine whether to dash horizontally or vertically
            Vector2 dashDirection;
            if (Mathf.Abs(directionToPlayer.x) > Mathf.Abs(directionToPlayer.y))
            {
                // Dash horizontally
                dashDirection = new Vector2(Mathf.Sign(directionToPlayer.x), 0);
            }
            else
            {
                // Dash vertically
                dashDirection = new Vector2(0, Mathf.Sign(directionToPlayer.y));
            }

            // Change color to red
            spriteRenderer.color = Color.red;

            while (dashTime > 0)
            {
                if (isDying) // Check if the boss is dying
                    yield break; // If so, stop the coroutine

                dashTime -= Time.deltaTime;
                MoveInDirection(dashDirection, dashSpeed);
                yield return null;
            }

            // Stop dashing and change color back to white
            rb.velocity = Vector2.zero;
            spriteRenderer.color = Color.white;
        }
    }


    private void MoveInDirection(Vector2 direction, float speed)
    {
        // Check for collisions with solid objects
        RaycastHit2D hit = Physics2D.Linecast(rb.position, rb.position + direction * speed * Time.deltaTime, solidObjectsLayer);
        if (hit.collider == null)
        {
            // If no collision is detected, move the boss
            rb.velocity = direction * speed;
        }
        else
        {
            //If a collision is detected, stop the boss
            rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDying) // Check if the boss is dying
            return; // If so, return early to prevent further actions

        // Check if the other collider is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Apply damage to the player
            base.OnTriggerEnter2D(collision);
        }
    }

    public void Die()
    {
        isDying = true; // Set isDying to true when the boss starts dying
        animator.SetBool("died", true); // Trigger the death animation
    }
}
