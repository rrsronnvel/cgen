using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Boss4Range : MonoBehaviour
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

    public GameObject victoryPanel;

    private bool isDying = false; // Add this line

    public static bool isAlive = true; // Add this line
    public TMP_Text countdownText; // Add this line



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
        // Don't fire bullets if the boss is dying
        if (isDying) return; // Add this line

        // Randomize the number of bullets
        bulletsAmount = Random.Range(10, 30); // Change these numbers to the minimum and maximum number of bullets you want


        float angleStep = (startAngle - endAngle) / bulletsAmount;

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
        // Don't move if the boss is dying
        if (isDying) return; // Add this line


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

      
    }

    public void KillBoss()
    {
        // If isDying is already true, do nothing
        if (isDying) return;

        // Set isDying to true
        isDying = true;

        // Get a reference to the Animator component
        Animator animator = GetComponent<Animator>();

        // Set the "Die" parameter to true
        animator.SetBool("die", true);

        // Wait for a few seconds before disabling the boss
        StartCoroutine(WaitAndDisable());
    }


    private IEnumerator WaitAndDisable()
    {
        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);

        // Show the VictoryPanel
        victoryPanel.SetActive(true); // Modify this line

        // Disable the boss
        Destroy(gameObject);
    }


}
