using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retreatingwithshooting : MonoBehaviour
{
    public float speed;
    public Transform target;
    public float minimumDistance;

    public GameObject projectile;
    public float timeBetweenShots;
    private float nextShotTime;

    // Update is called once per frame
    public void Update()
    {
        if(Time.time > nextShotTime)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            nextShotTime = Time.time + timeBetweenShots;
        }
        if (Vector2.Distance(transform.position, target.position) < minimumDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, -speed * Time.deltaTime);
        }
    }
}
