using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushController : MonoBehaviour, Interactable
{
    [SerializeField] float pushForce = 5f;
    [SerializeField] float maxPushDistance = 1f;

    [SerializeField]
    PlayerController playerController;

    Rigidbody2D rb;
    RigidbodyType2D initialBodyType;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        initialBodyType = rb.bodyType;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    public void Interact()
    {
        // Check if player is within range of pushable object
        float distanceToPlayer = Vector2.Distance(transform.position, playerController.transform.position);
        if (distanceToPlayer > maxPushDistance)
        {
            Debug.Log("You are too far away to push this object");
            return;
        }

        Vector2 pushDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        Rigidbody2D targetRigidbody = null;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, pushDir, 1f, playerController.interactableLayer);
        if (hit.collider != null)
        {
            targetRigidbody = hit.collider.GetComponent<Rigidbody2D>();
        }

        if (targetRigidbody != null)
        {
            // Check if the object is already moving
            if (targetRigidbody.velocity.magnitude > 0.1f)
            {
                Debug.Log("The object is already moving");
                return;
            }

            targetRigidbody.bodyType = RigidbodyType2D.Dynamic;
            targetRigidbody.gravityScale = 0f; // Set gravity scale to zero

            // Calculate the target position based on the push direction and the max distance
            Vector2 targetPosition = targetRigidbody.position + pushDir * maxPushDistance;

            // Move the object towards the target position
            StartCoroutine(MoveTowardsTarget(targetRigidbody, targetPosition));

            Debug.Log("You pushed an object");
            Debug.Log("Pushed " + targetRigidbody.gameObject.name + " with force " + (pushDir * pushForce));
        }
    }

    private IEnumerator MoveTowardsTarget(Rigidbody2D targetRigidbody, Vector2 targetPosition)
    {
        // Move the object towards the target position
        while (Vector2.Distance(targetRigidbody.position, targetPosition) > 0.01f)
        {
            targetRigidbody.MovePosition(Vector2.MoveTowards(targetRigidbody.position, targetPosition, pushForce * Time.fixedDeltaTime));
            yield return new WaitForFixedUpdate();
        }
        // Stop the object from moving
        targetRigidbody.velocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pushable"))
        {
            rb.bodyType = initialBodyType;
        }
    }
}