using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;
    [SerializeField] Player player;
    public Enemy enemy;
    //[SerializeField] private bool isPlayerTurn;
    [SerializeField] Button endTurn;
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
        // enemy = GameObject.FindWithTag("Enemy");
    }
    public void EnemyTurn()
    {
        for (int i = 0; i < 3; i++)
        {
            int index = Random.Range(0,3);
            if (index == 0 && enemy.blockingPower <=enemy.minBlockingPower*2)
            {
                Protection(enemy, enemy.blockingPower);
                continue;
            }
            else if (index == 1&&enemy.health<=enemy.maxHealth - enemy.maxHealth/10)
            {
                Heal(enemy,enemy.maxHealth/10);
                continue;
            }
            else
            {
                TakeDamage(player, enemy.strength);
                continue;
            }        
        }
        player.UpdateHUD();
        enemy.UpdateHUD();
        PlayerTurn();
    }
    private void PlayerTurn()
    {
        player.blockingPower = player.minBlockingPower;
        endTurn.interactable = true;
    }
    public void EndPlayerTurn()
    {

        var activeCards = CardDeck.Instance.activeCards;
        foreach (var activeCard in activeCards.ToArray())
        {
            if (activeCard.cardType == CardType.Attack)
            {
                TakeDamage(enemy,player.strength);
            }
            if (activeCard.cardType == CardType.Medicine)
            {
                Heal(player, player.intelligence);
            }
            if (activeCard.cardType == CardType.Protection)
            {
                Protection(player, player.blockingPower);
            }
            CardDeck.Instance.activeCards.Remove(activeCard);
            Destroy(activeCard.gameObject);
        }
        endTurn.interactable = false;
        enemy.blockingPower = enemy.minBlockingPower;
        enemy.UpdateHUD();
        EnemyTurn();
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

    public void TakeDamage(Enemy enemy, int damage)
    {
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
            enemy.Death();
        }
        enemy.UpdateHUD();
        Debug.Log("Enemy Take Damage");
    }
    public void TakeDamage(Player player, int damage)
    {
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

        if (player.health <= 0)
        {
            player.Death();
        }
        player.UpdateHUD();
        Debug.Log("Player Take Damage");
    }   

    private void Heal(Player player, int healPower)
    {
        //Heal
        if (player.health + healPower >= player.maxHealth)
        {
            player.health = player.maxHealth;
        }
        else
        {
            player.health += healPower;
        }
        player.UpdateHUD();
        Debug.Log("Heal Player");
    }
    private void Heal(Enemy enemy, int healPower)
    {
        //Heal
        if (enemy.health + healPower >= enemy.maxHealth)
        {
            enemy.health = enemy.maxHealth;
        }
        else
        {
            enemy.health += healPower;
        }
        enemy.UpdateHUD();
        Debug.Log("Heal Enemy");
    }

    private void Protection(Player player , int blockingPower)
    {
        //Protection UP
        player.blockingPower += blockingPower;
        Debug.Log("Protection Player");
    }
    private void Protection(Enemy enemy , int blockingPower)
    {
        //Protection UP
        enemy.blockingPower += blockingPower;
        Debug.Log("Protection Enemy");
    }

}