using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class Card : MonoBehaviour
{
    [SerializeField] private int number;
    [SerializeField] private GameObject cardFrame;
    [SerializeField] private TMP_Text cardPowerText;
    [SerializeField] private TMP_Text manaCostText;
    [SerializeField] private TMP_Text cardNameText;
    [SerializeField] private TMP_Text cardDescriptonText;
    public CardType cardType;
    public int price;
    public int cardPower;
    public int manaCost;
    public bool isActiveCard = false;
    // Start is called before the first frame update

    private void Awake()
    {
        if (cardPower == 0)
        {
            cardPower = SetCardPower();
        }
        cardDescriptonText.text = SetCardDescription();

        cardPowerText.text = cardPower.ToString();
        manaCostText.text = manaCost.ToString();
        cardNameText.text = cardType.ToString();

    }

    private string SetCardDescription()
    {
        var description = "Description";
        if (cardType == CardType.Attack)
        {
            description = $"The player deals {cardPower} HP damage to the enemy";
        }
        else if (cardType == CardType.Medicine)
        {
            description = $"Åhe player heals himself for {cardPower} HP";
        }
        else if (cardType == CardType.Protection)
        {
            description = $"The player increases his defense by {cardPower} units";
        }
        return description;
    }

    private int SetCardPower()
    {
        var power = 1;

        if (cardType == CardType.Attack)
        {
            power = Random.Range(15, 36);
        }
        else if (cardType == CardType.Medicine)
        {
            power = Random.Range(10, 31);
        }
        else if (cardType == CardType.Protection)
        {
            power = Random.Range(1, 3);
        }
        return power;
    }

    public void ActivateCard()
    {
        if (isActiveCard)
        {
            DeactivateCard();
        }
        else
        {
            if (CardDeck.Instance.activeCards.Count < CardDeck.Instance.maxActiveCard)
            {
                isActiveCard = true;
                cardFrame.SetActive(true);
                CardDeck.Instance.UpdateActiveCards(this, true);
            }
        }

    }

    public void DeactivateCard()
    {
        isActiveCard = false;
        cardFrame.SetActive(false);
        CardDeck.Instance.UpdateActiveCards(this, false);
    }
}
public enum CardType
{
    Attack,
    Protection,
    Medicine
}
