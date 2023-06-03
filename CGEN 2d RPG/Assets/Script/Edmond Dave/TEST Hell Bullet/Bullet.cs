using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : EnemyDamage
{
    private Vector2 moveDirection;
    private float moveSpeed;

    private void OnEnable()
    {
        Invoke("Destroy", 3f);
    }
    void Start()
    {
        moveSpeed = 5f;
    }
    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void SetmoveDirection(Vector2 dir)
    {
        moveDirection = dir;
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
       
    }
}
