using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : EnemyDamage
{
    Vector3 targetPosition;
    public float speed;
    // Start is called before the first frame update
    public void Start()
    {
        targetPosition = FindObjectOfType<PlayerController>().transform.position;
    }

    // Update is called once per frame
    public void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if(transform.position == targetPosition)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision); //Execute logic from parent script first
    }
}
