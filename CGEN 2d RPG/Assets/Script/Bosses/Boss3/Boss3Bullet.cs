using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3Bullet : MonoBehaviour
{
    [SerializeField]
    private int bulletsAmount = 10;
    [SerializeField]
    private float startAngle = 90f, endAngle = 270f;

    private Vector2 bulletMoveDirection;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Fire", 0f, 2f);
    }

    public void Fire()
    {
        // Randomize the number of bullets
        bulletsAmount = Random.Range(10, 16); // Change these numbers to the minimum and maximum number of bullets you want

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

            // Randomize the bullet's speed
            float bulletSpeed = 7.5f;
            bul.GetComponent<Bullet>().SetSpeed(bulletSpeed);

            angle += angleStep;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
