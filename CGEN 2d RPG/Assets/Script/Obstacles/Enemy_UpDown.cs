using UnityEngine;

public class Enemy_UpDown : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    [SerializeField] private float damage;

    private bool movingUp;
    private float lowerEdge;
    private float upperEdge;

    private void Awake()
    {
        lowerEdge = transform.position.y - movementDistance;
        upperEdge = transform.position.y + movementDistance;
    }

    private void Update()
    {
        if (movingUp)
        {
            if (transform.position.y < upperEdge)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
            }
            else
            {
                movingUp = false;
            }
        }
        else
        {
            if (transform.position.y > lowerEdge)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
            }
            else
            {
                movingUp = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
