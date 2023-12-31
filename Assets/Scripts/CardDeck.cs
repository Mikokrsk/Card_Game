using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    [SerializeField] private float deckUp = 0f;
    [SerializeField] private float deckDown = -150f;
    public List<GameObject> cardsPref;
    [SerializeField] private Transform contentContainer;
    //[SerializeField] private GameObject cardPref;
    [SerializeField] public List<Card> cardsOnCardDeck;
    [SerializeField] public List<Card> activeCards;
    public static CardDeck Instance;
    public int maxActiveCard = 3;
    // public int activeCard = 0;

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

    void Start()
    {
        cardsOnCardDeck = new List<Card>();
        cardsPref.AddRange(Array.ConvertAll(Resources.LoadAll("Cards", typeof(GameObject)), assets => (GameObject)assets));
        // UpdateCardsOnCardDeck();
    }

    public void AddCardToCardDeck(GameObject cardPref)
    {
        var item_go = Instantiate(cardPref);
        item_go.transform.SetParent(contentContainer, false);
        UpdateCardsOnCardDeck();
        // item_go.GetComponent<Image>().color = cardsInHand.Count % 2 == 0 ? Color.yellow : Color.cyan;       
    }

    private void UpdateCardsOnCardDeck()
    {
        cardsOnCardDeck.Clear();
        cardsOnCardDeck.AddRange(contentContainer.GetComponentsInChildren<Card>());
    }

    public void OnMouseEnterCardDeck()
    {
        transform.position = new Vector3(transform.position.x, deckUp, transform.position.z);
    }
    public void OnMouseExitCardDeck()
    {
        transform.position = new Vector3(transform.position.x, deckDown, transform.position.z);
    }

    public void UpdateActiveCards(Card activeCard, bool isActiveCard)
    {
        if (isActiveCard)
        {
            activeCards.Add(activeCard);
        }
        else
        {
            activeCards.Remove(activeCard);
        }

    }

    public void AddRandomCardToCardDeck()
    {
        var index = UnityEngine.Random.Range(0, cardsPref.Count);
        AddCardToCardDeck(cardsPref[index]);
    }
}
