using System.Collections;
using UnityEngine;

public class MoveSpeed : MonoBehaviour
{
    public float speedBoost = 4.0f;
    public float speedBoostDuration = 10f;
    public float numberOfSpeedBoostFlashes = 20f;

    private float normalSpeed;
    private bool speedBoosted = false;
    private SpriteRenderer spriteRend;

    private PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        spriteRend = GetComponent<SpriteRenderer>();
        normalSpeed = playerController.moveSpeed;
    }

    public void BoostSpeed()
    {
        if (!speedBoosted)  // Only boost speed if not currently boosted
        {
            playerController.moveSpeed += speedBoost;
            speedBoosted = true;
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
    }
}
