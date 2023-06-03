using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
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

    public ComputerHistoryBattle quizBattle;

    public Button attackButton;

    public Button specialAttackButton; 
    private int specialAttackCount = 2; 
    public TextMeshProUGUI specialAttackText;


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
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated.";
        }
    }

    void PlayerTurn()
    {
        dialogueText.text = "Choose an action:";
        attackButton.interactable = true; // Enable the attack button
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
