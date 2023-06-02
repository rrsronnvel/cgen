using UnityEngine;

public class Spikehead : EnemyDamage
{
    [Header("SpikeHead Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    private Vector3 destination;
    private float checkTimer;
    private bool attacking;
    private Transform playerTransform;

    private void OnEnable()
    {
        Stop();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Replace "Player" with your player's tag
    }
    private void Update()
    {
        // If attacking, update destination to current player's position
        if (attacking)
        {
            destination = playerTransform.position;
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
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

        Debug.DrawRay(transform.position, direction * range, Color.red);
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
        destination = transform.position; //Set destination as current position so it doesn't move
        attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        Stop(); //Stop spikehead once he hits something
    }
}
