using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool bulletPoolInstanse;

    [SerializeField]
    private GameObject poolBullet;
    private bool notEnoughBulletsInPool = true;

    private List<GameObject> bullets;

    // Add a serialized field for the parent object
    [SerializeField]
    private Transform bulletParent;

    private void Awake()
    {
        bulletPoolInstanse = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        bullets = new List<GameObject>();
    }

    public GameObject GetBullet()
    {


        if (bullets.Count > 0)
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                if (!bullets[i].activeInHierarchy)
                {
                    return bullets[i];
                }
            }
        }

        if (notEnoughBulletsInPool)
        {
            // Instantiate the bullet as a child of bulletParent
            GameObject bul = Instantiate(poolBullet, bulletParent);
            bul.SetActive(false);
            bullets.Add(bul);
            return bul;
        }
        return null;
    }
}
