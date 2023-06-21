using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float damage;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
            float knockbackForce = 0f; // Adjust this value as needed
            collision.GetComponent<Health>().TakeDamage(damage, knockbackDirection, knockbackForce);
        }
    }

}