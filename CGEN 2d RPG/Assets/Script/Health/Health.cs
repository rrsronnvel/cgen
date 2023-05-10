using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Health : MonoBehaviour, IDataPersistence
{
    //Might delete this later
    /* [Header("Game Management")]
     [SerializeField] private GameManager gameManager;*/
    public TestingRestart testingRestart;

    [Header("GameOver UI")]
    [SerializeField] private GameObject gameOverUI;


    [Header ("Health")]
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

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        HideGameOverUI();
    }

    public void LoadData(GameData data, bool isRestarting)
    {
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

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            //player hurt
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
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

    private void ShowGameOverUI()
    {
        gameOverUI.SetActive(true);
        TestingRestart.instance.SetupRestartButtons();

     /*   if (TestingRestart.instance.button2 != null)
        {
            // Add this line
            TestingRestart.instance.button2.onClick.AddListener(TestingRestart.instance.RestartGame);
        }
        else
        {
            Debug.LogError("Button2 is not found.");
        }*/
    }



    private void HideGameOverUI()
    {
        gameOverUI.SetActive(false);
    }

    //Might delete this later.
    /* public void RestartGame()
     {
         gameManager.RestartGame();
     }*/


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
            spriteRend.color = new Color(1,0,0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(9, 10, false);
    }
}
