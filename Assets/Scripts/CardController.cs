using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class CardController : MonoBehaviour
{
    [SerializeField] private Transform contentContainer;
    [SerializeField] private GameObject cardPref;
    [SerializeField] private List<Card> cardsOnCardDeck;


    void Start()
    {
        cardsOnCardDeck = new List<Card>();
       // UpdateCardsOnCardDeck();
    }

    public void AddCardToCardDeck()
    {
        var item_go = Instantiate(cardPref);
        item_go.transform.SetParent(contentContainer);
        UpdateCardsOnCardDeck();
        // item_go.GetComponent<Image>().color = cardsInHand.Count % 2 == 0 ? Color.yellow : Color.cyan;       
    } 
    
    private void UpdateCardsOnCardDeck()
    {
        cardsOnCardDeck.Clear();
        cardsOnCardDeck.AddRange(contentContainer.GetComponentsInChildren<Card>());
    }
}
