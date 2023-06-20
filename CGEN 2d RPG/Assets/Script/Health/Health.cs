using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Health : MonoBehaviour, IDataPersistence
{

    public TestingRestart testingRestart;

    [Header("GameOver UI")]
    [SerializeField] private GameObject gameOverUI;


    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private float numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Player")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Rigidbody2D rb; // Add this line

    [Header("Immunity")]
    [SerializeField] private int numberOfImmunityFlashes = 25;



    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        HideGameOverUI();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Knockback(Vector2 direction, float force)
    {
        playerController.isKnockback = true;
        rb.AddForce(direction * force, ForceMode2D.Impulse);
        StartCoroutine(StopKnockback());
    }

    private IEnumerator StopKnockback()
    {
        yield return new WaitForSeconds(0.7f); // Adjust this delay as needed
        rb.velocity = Vector2.zero;
        playerController.isKnockback = false;
        playerController.isMoving = false; // Add this line
    }





    public void LoadData(GameData data, bool isRestarting)
    {
        this.startingHealth = data.maxHealth;
        if (!isRestarting)
        {
            this.currentHealth = data.currentHealth;

        }
        else
        {
            this.currentHealth = startingHealth;
        }
    }


    public void SaveData(ref GameData data)
    {
        data.currentHealth = this.currentHealth;
    }

    public void TakeDamage(float _damage, Vector2 knockbackDirection, float knockbackForce)
    {
        if (Physics2D.GetIgnoreLayerCollision(9, 10)) // Do not take damage if immune
        {
            return;
        }

        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            //player hurt
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
            Knockback(knockbackDirection, knockbackForce); // Add this line
        }
        else
        {
            //player dead
            if (!dead)
            {
                anim.SetTrigger("die");
                dead = true;
                ShowGameOverUI();
                playerController.isGameOver = true;
            }
        }
    }

    public void TakeDamage(float _damage)
    {
        TakeDamage(_damage, Vector2.zero, 0f);
    }


    private void ShowGameOverUI()
    {
        gameOverUI.SetActive(true);
        TestingRestart.instance.SetupRestartButtons();


    }



    private void HideGameOverUI()
    {
        gameOverUI.SetActive(false);
    }


    public void ResetHealthToFull()
    {
        currentHealth = startingHealth;
    }



    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(9, 10, true);
        //Invulnerability duration
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(9, 10, false);
    }

    public void CollectImmunity(float duration)
    {
        StartCoroutine(Immunity(duration));
    }

    private IEnumerator Immunity(float duration)
    {
        Physics2D.IgnoreLayerCollision(9, 10, true);

        float flashInterval = duration / numberOfImmunityFlashes;

        for (float i = 0; i < duration; i += flashInterval)
        {
            spriteRend.color = Color.black;
            yield return new WaitForSeconds(flashInterval / 2);
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(flashInterval / 2);
        }
        spriteRend.color = Color.white; // reset color to white after immunity

        Physics2D.IgnoreLayerCollision(9, 10, false);
    }


}
