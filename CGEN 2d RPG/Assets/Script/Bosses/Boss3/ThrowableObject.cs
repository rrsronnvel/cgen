using UnityEngine;

public class ThrowableObject : MonoBehaviour, Interactable
{
    public float damage = 1f; // The damage this object deals to the boss
    public float minVelocity = 2f; // The minimum velocity this object needs to have to deal damage

    [SerializeField] private float throwSpeed = 10f; // The speed at which the object is thrown
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Interact()
    {
        // Get the player's facing direction
        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        Vector2 playerFacingDirection = new Vector2(player.animator.GetFloat("moveX"), player.animator.GetFloat("moveY"));

        // Throw the object in the player's facing direction
        rb.velocity = playerFacingDirection * throwSpeed;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Check if the object's velocity is above the threshold
            if (rb.velocity.magnitude > minVelocity)
            {
                // If it is, deal damage to the boss
                collision.gameObject.GetComponent<Boss3>().TakeDamage(damage);

                // Destroy the throwable object
                gameObject.SetActive(false);
            }
        }
    }
}
