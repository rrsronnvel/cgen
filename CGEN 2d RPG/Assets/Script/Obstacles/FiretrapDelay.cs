using System.Collections;
using UnityEngine;

public class FiretrapDelay : MonoBehaviour
{
    [SerializeField] private float damage;

    [Header("Firetrap Timers")]
    [SerializeField] private float activeTime;
    private Animator anim;
    private SpriteRenderer spriteRend;

    private bool paused;

    private bool active; // when the trap is active and can hurt the player

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        ActivateFiretrap();
    }

    public void PauseTrap()
    {
        paused = true;
        active = false;
        anim.SetBool("activated", false);
    }

    public void ResumeTrap()
    {
        paused = false;
        ActivateFiretrap();
    }

    private void ActivateFiretrap()
    {
        spriteRend.color = Color.white; // turn the sprite back to its initial color
        active = true;
        anim.SetBool("activated", true);

        StartCoroutine(DeactivateFiretrap());
    }

    private IEnumerator DeactivateFiretrap()
    {
        // wait until X seconds, deactivate trap and reset all variables and animator
        yield return new WaitForSeconds(activeTime);
        active = false;
        anim.SetBool("activated", false);

        // Reactivate the firetrap if not paused
        if (!paused)
        {
            ActivateFiretrap();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && active)
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
