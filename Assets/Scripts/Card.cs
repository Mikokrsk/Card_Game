using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Card : MonoBehaviour
{
    [SerializeField] private int index;
    [SerializeField] private GameObject cardFrame;
    [SerializeField] private CardType cardType;
    public bool isActiveCard = false;
    // Start is called before the first frame update

    void Start()
    {
        //    cardFrame.active = !cardFrame.active;
    }

    // Update is called once per frame
    void Update()
    {

    }

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
enum CardType
{
    Attack,
    Protection,
    Medicine
}
