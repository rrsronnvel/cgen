using UnityEngine;
using System.Collections;

public class MoveSpeed : MonoBehaviour
{
    public float speedBoost = 5.0f;
    public float speedBoostDuration = 12f;
    public float numberOfSpeedBoostFlashes = 10f;

    private float normalSpeed;
    private bool speedBoosted = false;
    private SpriteRenderer spriteRend;

    private PlayerController playerController;

    private ParticleSystem particleSystem;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        spriteRend = GetComponent<SpriteRenderer>();
        particleSystem = GetComponent<ParticleSystem>();
        particleSystem.Stop(); // Ensure the particle system is off by default
        normalSpeed = playerController.moveSpeed;
    }

    public void BoostSpeed()
    {
        if (!speedBoosted)  // Only boost speed if not currently boosted
        {
            playerController.moveSpeed += speedBoost;
            speedBoosted = true;
            particleSystem.Play(); // Turn on the particle system when the speed boost is active

            // Call SpeedBoostFlash coroutine
            StartCoroutine(SpeedBoostFlash());

            StartCoroutine(RevertSpeedAfterDelay(speedBoostDuration));
        }
    }

    private IEnumerator SpeedBoostFlash()
    {
        for (int i = 0; i < numberOfSpeedBoostFlashes; i++)
        {
            spriteRend.color = Color.blue;
            yield return new WaitForSeconds(speedBoostDuration / (numberOfSpeedBoostFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(speedBoostDuration / (numberOfSpeedBoostFlashes * 2));
        }
        spriteRend.color = Color.white; // Ensure the color reverts to white after flashing
    }

    private IEnumerator RevertSpeedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        playerController.moveSpeed = normalSpeed;
        speedBoosted = false;
        particleSystem.Stop(); // Turn off the particle system when the speed boost ends
    }
}
