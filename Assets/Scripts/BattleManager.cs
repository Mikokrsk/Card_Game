using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] GameObject enemy;
   // [SerializeField] Button endTurn;

    private void OnEnable()
    {
        player = PlayerController.Instance;
        enemy = GameObject.FindWithTag("Enemy");
    }
    public void StartTurn()
    {
        player.blockingPower = player.minbBlockingPower;
    }
    public void EndTurn()
    {
        var activeCards = CardDeck.Instance.activeCards;
        foreach (var activeCard in activeCards.ToArray())
        {
            if (activeCard.cardType == CardType.Attack)
            {
                Attack(enemy, player.strength);
            }
            if (activeCard.cardType == CardType.Medicine)
            {
                Heal(player.intelligence);
            }
            if (activeCard.cardType == CardType.Protection)
            {
                Protection(player.blockingPower);
            }
            CardDeck.Instance.activeCards.Remove(activeCard);
            Destroy(activeCard.gameObject);
        }
        
       // CardDeck.Instance.activeCards.Clear();
    }
    private void Attack(GameObject target, int attackPower)
    {
        //Attack
        Debug.Log("Attack");
    }
    private void Heal(int healPower)
    {
        //Heal
        if (player.health + 10 * healPower >= player.maxHealth)
        {
            player.health = player.maxHealth;
        }
        else
        {
            player.health += 10 * healPower;
        }
        player.UpdateHUD();
        Debug.Log("Heal");
    }
    private void Protection(int blockingPower)
    {
        //Protection UP
        player.blockingPower += blockingPower;
        Debug.Log("Protection");
    }
}
