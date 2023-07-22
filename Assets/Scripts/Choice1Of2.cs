using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Choice1Of2 : MonoBehaviour
{
    [SerializeField] private List<GameObject> cards;
    [SerializeField] private Text eventText;
    [SerializeField] private CardDeck cardDeck;
    [SerializeField] private GameObject card_1;
    [SerializeField] private GameObject card_2;
    [SerializeField] private Transform positionCard_1;
    [SerializeField] private Transform positionCard_2;

    private void Awake()
    {
        cards.AddRange(Array.ConvertAll(Resources.LoadAll("Cards", typeof(GameObject)), assets => (GameObject)assets));
        eventText = GetComponentInChildren<Text>();
    }
    private void OnEnable()
    {
        card_1 = GetRandomCard();
        do
        {
            card_2 = GetRandomCard();
            Debug.Log($"{card_1.GetComponent<Card>().cardType} or {card_2.GetComponent<Card>().cardType}");
        }
        while (card_1.GetComponent<Card>().cardType == card_2.GetComponent<Card>().cardType);

        eventText.text = $"{card_1.GetComponent<Card>().cardType} OR {card_2.GetComponent<Card>().cardType}";
        card_1 = AddCard(card_1, positionCard_1);
        card_2 = AddCard(card_2, positionCard_2);
    }
    private void OnDisable()
    {
        Destroy(card_1);
        Destroy(card_2);
    }
    public void AddCardToCardDeck(int index)
    {
        if (index == 1)
        {
            cardDeck.AddCardToCardDeck(card_1);
        }
        if (index == 2)
        {
            cardDeck.AddCardToCardDeck(card_2);
        }
    }
    private GameObject AddCard(GameObject cardPref, Transform position)
    {
        var item_go = Instantiate(cardPref);
        item_go.transform.SetParent(gameObject.transform, false);
        item_go.transform.SetSiblingIndex(0);
        item_go.transform.position = position.position;
        return item_go;
    }
    private GameObject GetRandomCard()
    {
        return cards[UnityEngine.Random.Range(0, cards.Count)];
    }

}
