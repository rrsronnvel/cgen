using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : MonoBehaviour
{
   


    [SerializeField]
    private int bulletsAmount = 15;
    [SerializeField]
    private float startAngle = 90f, endAngle = 270f;

    private Vector2 bulletMoveDirection;

    // Sideways movement variables
    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;
    private float movementDistance;
    private float speed;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Fire", 0f, 2f);

        // Initialize sideways movement variables
        movementDistance = Random.Range(14f, 15f); // Randomize movement distance between 1 and 5
        speed = Random.Range(8f, 15f); // Randomize speed between 1 and 3
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }

    private void OnDestroy()
    {
        // Cancel the invocation of the Fire method
        CancelInvoke("Fire");

      
    }

    private void Fire()
    {
        // Randomize the number of bullets
        bulletsAmount = Random.Range(10, 30); // Change these numbers to the minimum and maximum number of bullets you want


        float angleStep = (endAngle - startAngle) / bulletsAmount;
        float angle = startAngle;


        for (int i = 0; i < bulletsAmount + 1; i++)
        {
            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject bul = BulletPool.bulletPoolInstanse.GetBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<Bullet>().SetmoveDirection(bulDir);


            // Set the color of the bullet
            if (i % 3 == 0)
            {
                bul.GetComponent<Bullet>().SetColor(Color.white);
            }
            else if (i % 1.5f == 1)
            {
                bul.GetComponent<Bullet>().SetColor(Color.red);
            }

            // Randomize the bullet's speed
            float bulletSpeed = Random.Range(6f, 20f);
            bul.GetComponent<Bullet>().SetSpeed(bulletSpeed);

            angle += angleStep;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Sideways movement logic
        if (movingLeft)
        {
            if (transform.position.x > leftEdge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                movingLeft = false;
                speed = Random.Range(8f, 15f); // Randomize speed again when direction changes

                
            }
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                movingLeft = true;
                speed = Random.Range(8f, 15f); // Randomize speed again when direction changes

                
            }
        }

        // Check if all 5 BossStopper buttons are pressed
        if (BossStopper.stoppersPressed >= 5)
        {
            KillBoss();
        }
    }

    private void KillBoss()
    {
        // Here, you can add code to "kill" the boss.
        // For example, you could disable the boss's scripts, stop it from moving, play a death animation, etc.
        // For now, let's just disable the boss object.
        Destroy(gameObject);

    }


}
