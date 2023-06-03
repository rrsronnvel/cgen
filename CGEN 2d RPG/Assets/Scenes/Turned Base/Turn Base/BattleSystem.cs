using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public enum RewardType { Health, Speed }
public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Unit playerUnit;
    Unit enemyUnit;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public Text dialogueText;
    public TextMeshProUGUI rewardText;

    public ComputerHistoryBattle quizBattle;

    public Button attackButton;

    public Button specialAttackButton; 
    private int specialAttackCount = 2;
    public TextMeshProUGUI specialAttackText;
    public GameObject rewardPopPanel;
    public GameObject lostPopPanel;

    public RewardType rewardType;
    public BattleState state;


    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
        UpdateSpecialAttackText();
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();
        playerUnit.LoadHealth();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text = "A wild " + enemyUnit.unitName + " approaches...";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        // Disable the attack button
        attackButton.interactable = false;
        specialAttackButton.interactable = false;

        // Get the Animator component from the player
        Animator animator = playerUnit.GetComponent<Animator>();

        // Set the "attacking" parameter to true to start the attack animation
        animator.SetBool("attacking", true);
        yield return new WaitForSeconds(1.5f);

        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "The attack is successful!";

        yield return new WaitForSeconds(2f);

        // Set the "attacking" parameter back to false to return to the idle animation
        animator.SetBool("attacking", false);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }

        // Re-enable the attack button
        attackButton.interactable = true;
        specialAttackButton.interactable = true;
    }



    public IEnumerator EnemyTurn()
    {
        
        yield return new WaitForSeconds(2f);
        dialogueText.text = enemyUnit.unitName + " attacks!";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }

    }


    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You won the battle!";
            rewardPopPanel.SetActive(true);
            if (DataPersistenceManager.instance != null)
            {
                GameData gameData = DataPersistenceManager.instance.GetGameData();
                if (gameData != null)
                {
                    if (rewardType == RewardType.Health)
                    {
                        // Increase the player's maximum health
                        playerUnit.maxHP += 0.5f;
                        gameData.maxHealth = playerUnit.maxHP;
                        rewardText.text = "Your Max Health is increased!";
                    }
                    else if (rewardType == RewardType.Speed)
                    {
                        // Increase the player's speed
                        gameData.playerSpeed += 0.5f;
                        rewardText.text = "Your Move Speed is increased!";
                    }
                    gameData.currentHealth = playerUnit.currentHP; // Add this line

                    // Set the TBC as won
                    if (gameData.TBCWon.ContainsKey(gameData.activeTBC))
                    {
                        gameData.TBCWon[gameData.activeTBC] = true;
                    }
                    else
                    {
                        gameData.TBCWon.Add(gameData.activeTBC, true);
                    }
                }
            }
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated.";
            lostPopPanel.SetActive(true); // Show the "Lost" panel
        }
    }



    public void OnOkButton()
    {


        // Load the previous scene
        if (DataPersistenceManager.instance != null)
        {
            GameData gameData = DataPersistenceManager.instance.GetGameData();
            if (gameData != null && !string.IsNullOrEmpty(gameData.previousScene))
            {
                SceneManager.LoadScene(gameData.previousScene);
            }
        }

        rewardPopPanel.SetActive(false);
    }

    public void OnLostOkButton()
    {
        // Load the previous scene
        if (DataPersistenceManager.instance != null)
        {
            GameData gameData = DataPersistenceManager.instance.GetGameData();
            if (gameData != null && !string.IsNullOrEmpty(gameData.previousScene))
            {
                TestingRestart.instance.RestartGame();
                SceneManager.LoadScene(gameData.previousScene);
            }
        }

        lostPopPanel.SetActive(false); // Hide the "Lost" panel
    }



    void PlayerTurn()
    {
        dialogueText.text = "Choose an action:";
        attackButton.interactable = true; // Enable the attack button
        specialAttackButton.interactable = true;
        quizBattle.StartQuiz();
    }




    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerSpecialAttack() // Add this
    {

        // Disable the attack button
        attackButton.interactable = false;
        specialAttackButton.interactable = false;

        if (specialAttackCount <= 0)
        {
            dialogueText.text = "You have no special attacks left!";
            yield break;
        }

        // Get the Animator component from the player
        Animator animator = playerUnit.GetComponent<Animator>();

        // Set the "attacking" parameter to true to start the attack animation
        animator.SetBool("attacking2", true);
        yield return new WaitForSeconds(1.5f);

        // Deal more damage for the special attack
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage + .5f);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "The special attack is successful!";

        yield return new WaitForSeconds(2f);

        // Set the "attacking" parameter back to false to return to the idle animation
        animator.SetBool("attacking2", false);

        // Decrease the special attack count
        specialAttackCount--;

        UpdateSpecialAttackText();
        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        attackButton.interactable = true;
        specialAttackButton.interactable = true;
    }

    public void OnSpecialAttackButton() // Add this
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerSpecialAttack());
    }

    void UpdateSpecialAttackText()
    {
        specialAttackText.text = $"SS<sup>{specialAttackCount}/2</sup>";
    }



}
