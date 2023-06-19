using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : EnemyDamage
{
    private SpriteRenderer spriteRenderer;


    private Vector2 moveDirection;
    private float moveSpeed;

    private void OnEnable()
    {
        Invoke("Destroy", 10f);
    }
    void Awake()
    {
        moveSpeed = 5f;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }


    public void SetmoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }

    // Add a method to set the bullet's speed
    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision); //Execute logic from parent script first

        // Check if the object collided with is in the SolidObjects or Player layer
        if (collision.gameObject.layer == LayerMask.NameToLayer("SolidObjects") || collision.gameObject.layer == LayerMask.NameToLayer("Player") || collision.gameObject.layer == LayerMask.NameToLayer("Interactable"))
        {
            Destroy();
        }
    }
}
