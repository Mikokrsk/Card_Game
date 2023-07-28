using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Card : MonoBehaviour
{
    [SerializeField] private int number;
    [SerializeField] private GameObject cardFrame;
    public CardType cardType ;
    public int price;
    public bool isActiveCard = false;
    // Start is called before the first frame update


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
                CardDeck.Instance.UpdateActiveCards(this,true);
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
