using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;
    [SerializeField] Player player;
    public Enemy enemy;
    [SerializeField] private SpawnEnemyManager spawnEnemyManager;
    //[SerializeField] private bool isPlayerTurn;
    [SerializeField] Button endTurn;
    public List<Enemy> enemies;
    [SerializeField] GameObject enemyObject;
    [SerializeField] private Animator playerAnimator;
    //[SerializeField] private Animator enemyAnimator;
    //[SerializeField] private ColorBlock newColor;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        player = Player.Instance;
        playerAnimator = Player.Instance.playerAnimator;
        //playerAnimator.SetBool("MoveFWD",false);
        spawnEnemyManager.SpawnRandomEnemy();
        //endTurn.gameObject.SetActive(true);
        gameManager.Instance.BattleState(true);
        PlayerTurn();
    }

    public void UpdateEnemyList()
    {
        //enemies.Clear();
        //enemies.AddRange(enemyObject.GetComponentsInChildren<Enemy>());
        Debug.Log($"Enemies Count = {enemies.Count}");
        if (enemies.Count > 0)
        {
            foreach (var enemy in enemies)
            {
                enemy.gameObject.SetActive(false);
            }
            enemies.First().gameObject.SetActive(true);
            enemy = enemies.First().GetComponent<Enemy>();
            //enemyAnimator = enemy.animator;
            //enemyAnimator.SetBool("Walk",false);
        }
        else
        {
            gameObject.gameObject.SetActive(false);
        }
    }
    public void AddEnemyInList(Enemy addEnemy)
    {
        enemies.Add(addEnemy);
        UpdateEnemyList();
    }
    public void RemoveEnemyFromList(Enemy removeEnemy)
    {
        enemies.Remove(removeEnemy);
        UpdateEnemyList();
    }
    IEnumerator EnemyTurn()
    {
        for (int i = 0; i < 3; i++)
        {
            int index = Random.Range(0, 3);
            if (index == 0 && enemy.blockingPower <= enemy.minBlockingPower * 2)
            {
                EnemyProtection(enemy, enemy.blockingPower);
            }
            else if (index == 1 && enemy.health <= enemy.maxHealth - enemy.maxHealth / 10)
            {
                EnemyHeal(enemy, enemy.maxHealth / 10);
            }
            else
            {
                PlayerTakeDamage(player, enemy.strength);                
            }
            yield return new WaitForSeconds(3f);
        }
        player.UpdateHUD();
        enemy.UpdateHUD();
        PlayerTurn();
    }
    private void PlayerTurn()
    {
        if (player.isAlive)
        {
            player.blockingPower = player.minBlockingPower;
            player.UpdateHUD();
            endTurn.interactable = true;
        }
    }
    public void EndPlayerTurn()
    {
        endTurn.interactable = false;
        //endTurn.colors.normalColor = Color.black;
        StartCoroutine(EndPlayerTurnCoroutine());
    }
    IEnumerator EndPlayerTurnCoroutine()
    {
        
        var activeCards = CardDeck.Instance.activeCards;
        foreach (var activeCard in activeCards.ToArray())
        {
            if (activeCard.cardType == CardType.Attack)
            {
                PlayerAttack(enemy, player.strength + activeCard.cardPower);
            }
            if (activeCard.cardType == CardType.Medicine)
            {
                PlayerHeal(player, player.intelligence + activeCard.cardPower);
            }
            if (activeCard.cardType == CardType.Protection)
            {
                PlayerProtection(player, player.blockingPower + activeCard.cardPower);
            }
            CardDeck.Instance.activeCards.Remove(activeCard);
            CardDeck.Instance.cardsOnCardDeck.Remove(activeCard);
            Destroy(activeCard.gameObject);
            //StartCoroutine(StartAnimation());
            //Invoke("EndBattle",5f);
            player.UpdateHUD();
            yield return new WaitForSeconds(3f);
        }
        // endTurn.interactable = false;
        enemy.blockingPower = enemy.minBlockingPower;
        enemy.UpdateHUD();
        StartCoroutine(EnemyTurn());
    }

    /*private void PlayerAttack()
    {
        TakeDamage(enemy, player.strength);
        Debug.Log("Attack");
    }
    private void EnemyAttack()
    {
        TakeDamage(player, enemy.strength);
        Debug.Log("Enemy Attack");
    }

    private void PlayerHeal()
    {
        Heal(player,player.intelligence);
    }*/

    public void PlayerAttack(Enemy enemy, int damage)
    {
        playerAnimator.SetTrigger("Attack");
        enemy.animator.SetTrigger("GetDamage");
        damage /= enemy.blockingPower;
        if (damage <= 0)
        {
            damage = 1;
        }
        if (enemy.armor > 0)
        {
            enemy.armor -= damage;
            if (enemy.armor < 0)
            {
                enemy.health -= -enemy.armor;
                enemy.armor = 0;
            }

        }
        else
        {
            enemy.health -= damage;
        }

        if (enemy.health <= 0)
        {
            Death(enemy);

            // enemy.Death();
        }
        enemy.UpdateHUD();
        // Debug.Log("Enemy Take Damage");
    }
    public void PlayerTakeDamage(Player player, int damage)
    {
        enemy.animator.SetTrigger("Attack");
        damage /= player.blockingPower;
        if (damage <= 0)
        {
            damage = 1;
        }
        if (player.armor > 0)
        {
            player.armor -= damage;
            if (player.armor < 0)
            {
                player.health -= -player.armor;
                player.armor = 0;
            }

        }
        else
        {
            player.health -= damage;
        }

        player.UpdateHUD();
        playerAnimator.SetTrigger("GetDamage");
        // Debug.Log("Player Take Damage");

        if (player.health <= 0)
        {
            // player.Death();
            StartCoroutine(PlayerDeath());
        }
    }

    private void PlayerHeal(Player player, int healPower)
    {
        //Heal
        playerAnimator.SetTrigger("Heal");
        if (player.health + healPower >= player.maxHealth)
        {
            player.health = player.maxHealth;
        }
        else
        {
            player.health += healPower;
        }
        player.UpdateHUD();
        //Debug.Log("Heal Player");
    }
    private void EnemyHeal(Enemy enemy, int healPower)
    {
        //Heal
        enemy.animator.SetTrigger("Heal");
        if (enemy.health + healPower >= enemy.maxHealth)
        {
            enemy.health = enemy.maxHealth;
        }
        else
        {
            enemy.health += healPower;
        }
        enemy.UpdateHUD();
        //  Debug.Log("Heal Enemy");
    }

    private void PlayerProtection(Player player, int blockingPower)
    {
        //Protection UP
        playerAnimator.SetTrigger("Protection");
        player.blockingPower = blockingPower;
        //  Debug.Log("Protection Player");
    }
    private void EnemyProtection(Enemy enemy, int blockingPower)
    {
        //Protection UP
        enemy.animator.SetTrigger("Protection");
        enemy.blockingPower += blockingPower;
        //   Debug.Log("Protection Enemy");
    }

    IEnumerator PlayerDeath()
    {
        Debug.Log("Player is dead");
        //player.Death();
        player.isAlive = false;
        playerAnimator.SetBool("Dead", true);
        yield return new WaitForSeconds(3f);
        gameManager.Instance.GameOverState();
    }
    private void Death(Enemy enemy)
    {
        // Debug.Log($"Enemy death : {enemy.name}");
        /*Destroy(enemy.gameObject);
        RemoveEnemyFromList(enemy);
        if (enemies.Count <= 0)
        {
            EndBattle();
        }*/
        StartCoroutine(EnemyDeath());
    }
    IEnumerator EnemyDeath()
    {
        enemy.animator.SetBool("Death",true);
        yield return new WaitForSeconds(3f);
        Destroy(enemy.gameObject);
        RemoveEnemyFromList(enemy);
        if (enemies.Count <= 0)
        {
            EndBattle();
        }
    }

    private void EndBattle()
    {
       // playerAnimator.SetBool("MoveFWD", true);
        gameManager.Instance.BattleState(false);
    }
}