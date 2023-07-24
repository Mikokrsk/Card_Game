using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Card : MonoBehaviour
{
    [SerializeField] private int index;
    [SerializeField] private GameObject cardFrame;
    public CardType cardType ;
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
            if (CardDeck.Instance.activeCard < CardDeck.Instance.maxActiveCard)
            {
                isActiveCard = true;
                cardFrame.SetActive(true);
                CardDeck.Instance.activeCard++;
            }
        }

    }

    public void DeactivateCard()
    {
        isActiveCard = false;
        cardFrame.SetActive(false);
        CardDeck.Instance.activeCard--;
    }
}
public enum CardType
{
    Attack,
    Protection,
    Medicine
}
