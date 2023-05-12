using System.Collections;
using UnityEngine;

public class FiretrapDelay : MonoBehaviour
{
    [SerializeField] private float damage;

    [Header("Firetrap Timers")]
    [SerializeField] private float initialDelay;
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator anim;
    private SpriteRenderer spriteRend;

    private float storedInitialDelay;

    private bool paused;

    private bool triggered; // when the trap gets triggered
    private bool active; // when the trap is active and can hurt the player

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        storedInitialDelay = initialDelay;
    }

    private void Start()
    {
        StartCoroutine(ActivateFiretrap());
    }

    public void PauseTrap()
    {
        paused = true;
        StopAllCoroutines();
        active = false;
        triggered = false;
        anim.SetBool("activated", false);
    }

    public void ResumeTrap()
    {
        if (paused)
        {
            StartCoroutine(ActivateFiretrap());
        }
        paused = false;
    }

    private IEnumerator ActivateFiretrap()
    {
        while (true)
        {
            // wait for the initial delay
            yield return new WaitForSeconds(initialDelay);

            // turn the sprite red to notify the player
            triggered = true;
            spriteRend.color = Color.red;

            // wait for delay, activate trap, turn on animation, return color back to normal
            yield return new WaitForSeconds(activationDelay);
            spriteRend.color = Color.white; // turn the sprite back to its initial color
            active = true;
            anim.SetBool("activated", true);

            // wait until X seconds, deactivate trap and reset all variables and animator
            yield return new WaitForSeconds(activeTime);
            active = false;
            triggered = false;
            anim.SetBool("activated", false);

            // reset the initial delay to the stored value so the firetrap keeps activating with the correct delay
            initialDelay = storedInitialDelay;

            if (paused)
            {
                break;
            }
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
