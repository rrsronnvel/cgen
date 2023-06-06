using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3 : EnemyDamage
{
    [Header("Boss Attributes")]
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

    private bool dashing;

    private float health = 5f; // The boss's health

    private Animator animator; // Animator component
    public GameObject victoryPanel;

    public bool isDying = false; // Add this line

    public float GetHealth()
    {
        return health;
    }


    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
        animator = GetComponent<Animator>(); // Get the Animator component
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Replace "Player" with your player's tag
        StartCoroutine(Dash()); // Start the Dash coroutine
    }


    private void Update()
    {
        // If the boss is dying, don't do anything
        if (isDying)
            return;

        // If not dashing and attacking, update destination to current player's position
        if (!dashing && attacking)
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
            // If the boss is dying, don't do anything
            if (isDying)
                yield break;

            // Wait for a random time between 2 and 5 seconds before starting the dash
            yield return new WaitForSeconds(Random.Range(1f, 4f));

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

            // Start dashing
            dashing = true;

            while (dashTime > 0)
            {
                dashTime -= Time.deltaTime;
                MoveInDirection(dashDirection, dashSpeed);
                yield return null;
            }

            // Stop dashing and change color back to white
            rb.velocity = Vector2.zero;
            spriteRenderer.color = Color.white;

            // Stop dashing
            dashing = false;

            // Stop attacking
            attacking = false;

            // Stop for 3 seconds
            yield return new WaitForSeconds(3f);

            // Fire bullets
            GetComponent<Boss3Bullet>().Fire();

            // Stop for 1.5 seconds
            yield return new WaitForSeconds(1.5f);
            // Fire bullets
            GetComponent<Boss3Bullet>().Fire();

            // Wait for 1.5 seconds before starting the next dash
            yield return new WaitForSeconds(1.5f);
        }
    }




    private void MoveInDirection(Vector2 direction, float speed)
    {
        // Check for collisions with solid objects
        RaycastHit2D hit = Physics2D.Linecast(rb.position, rb.position + direction * speed * Time.deltaTime, solidObjectsLayer);
        if (hit.collider == null)
        {
            // If no collision is detected, move the boss
            if (dashing) // If the boss is dashing
            {
                rb.velocity = direction * dashSpeed; // Use the dash speed
            }
            else // If the boss is not dashing
            {
                rb.velocity = direction * speed; // Use the normal speed
            }
        }
        else
        {
            //If a collision is detected, stop the boss
            rb.velocity = Vector2.zero;
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the other collider is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Apply damage to the player
            base.OnTriggerEnter2D(collision);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        // Log a message to the console
        Debug.Log("Boss took " + damage + " damage. Current health: " + health);

        // If the boss's health is 0 or less, play the death animation and then destroy the boss
        if (health <= 0)
        {
            isDying = true; // The boss is dying
            animator.SetBool("die", true); // Start the death animation
            StartCoroutine(DestroyAfterDelay(3f)); // Wait for 3 seconds before destroying the boss
        }
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        victoryPanel.SetActive(true); // Show the VictoryPanel
        Destroy(gameObject);
    }


}
