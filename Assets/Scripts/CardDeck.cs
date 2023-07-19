using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    [SerializeField] private float deckUp = 0f;
    [SerializeField] private float deckDown = -150f;
    [SerializeField] private Transform contentContainer;
    //[SerializeField] private GameObject cardPref;
    [SerializeField] private List<Card> cardsOnCardDeck;
    public static CardDeck Instance;
    public int maxActiveCard = 3;
    public int activeCard = 0;

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
        // UpdateCardsOnCardDeck();
    }

    public void AddCardToCardDeck(GameObject cardPref)
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

    public void OnMouseEnterCardDeck()
    {
        transform.position = new Vector3(transform.position.x, deckUp, transform.position.z);
    }
    public void OnMouseExitCardDeck()
    {
        transform.position = new Vector3(transform.position.x, deckDown, transform.position.z);
    }
}
