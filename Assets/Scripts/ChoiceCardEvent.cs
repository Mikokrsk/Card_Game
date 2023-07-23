using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class ChoiceCardEvent : MonoBehaviour
{
    [SerializeField] private List<GameObject> cards;
    [SerializeField] private Text eventText;
    [SerializeField] private CardDeck cardDeck;
    [SerializeField] private List<GameObject> cardsEvent;
    [SerializeField] private Transform[] cardPosition;
    [SerializeField] private GameObject cardsTriggersObject;
    [SerializeField] private Button[] buttonsTrigger;
    [SerializeField] private GameObject buttonPref;
    [SerializeField] private GameObject cardsObject;
    [SerializeField] private int cardsCount;

    private void Awake()
    {
        cardDeck = CardDeck.Instance;
        cards.AddRange(Array.ConvertAll(Resources.LoadAll("Cards", typeof(GameObject)), assets => (GameObject)assets));
        eventText = GetComponentInChildren<Text>();
    }
    private void buttonCallBack(Button buttonPressed)
    {
        var index = 0;
        for (int i = 0; i < buttonsTrigger.Length; i++)
        {
            if (buttonsTrigger[i] == buttonPressed)
            {
                index = i;
                break;
            }
        }
        cardDeck.AddCardToCardDeck(cardsEvent[index]);
        Debug.Log($"index = {index}");
    }
    private void OnEnable()
    {
        buttonsTrigger = new Button[cardsCount];
        for (int i = 0; i < cardsCount; i++)
        {
            buttonsTrigger[i] = AddCard(buttonPref, gameObject.transform, cardsTriggersObject.transform).GetComponent<Button>();
        }
        cardPosition = cardsTriggersObject.GetComponentsInChildren<Transform>().Skip(1).ToArray();
        Debug.Log(cardPosition[0].transform.position);
        // buttonsTrigger = cardsTriggersObject.GetComponentsInChildren<Button>();
        foreach (var button in buttonsTrigger)
        {
            button.onClick.AddListener(() => buttonCallBack(button));
        }
        eventText.text = " ";
        cardsEvent.Clear();
        for (int i = 0; i < cardPosition.Length; i++)
        {
            cardsEvent.Add(GetRandomCard());
            cardsEvent[i] = AddCard(cardsEvent[i], cardsObject.transform, cardsObject.transform);
           
            if (i + 1 >= cardPosition.Length)
            {
                eventText.text += $"{cardsEvent.Last().GetComponent<Card>().cardType}";
            }
            else
            {
                eventText.text += $"{cardsEvent.Last().GetComponent<Card>().cardType} or ";
            }
        }
/*        for (int i = 0;i < cardsEvent.Count;i++)
        {
            cardsEvent[i].transform.position = cardPosition[i].transform.position;
            Debug.Log(cardPosition[i].transform.position);
        }*/
        /*        card_1 = GetRandomCard();
                do
                {
                    card_2 = GetRandomCard();
                    Debug.Log($"{card_1.GetComponent<Card>().cardType} or {card_2.GetComponent<Card>().cardType}");
                }
                while (card_1.GetComponent<Card>().cardType == card_2.GetComponent<Card>().cardType);

                eventText.text = $"{card_1.GetComponent<Card>().cardType} OR {card_2.GetComponent<Card>().cardType}";
                card_1 = AddCard(card_1, positionCard_1);
                card_2 = AddCard(card_2, positionCard_2);*/
    }

    private void OnDisable()
    {
        foreach (var card in cardsEvent)
        {
            Destroy(card);
        }
        foreach (var button in buttonsTrigger)
        {
            button.onClick.RemoveAllListeners();
        }
    }
    public void AddCardToCardDeck(Button button)
    {

        cardDeck.AddCardToCardDeck(cardsEvent[0]);

    }

    private GameObject AddCard(GameObject cardPref, Transform position, Transform parent)
    {
        var item_go = Instantiate(cardPref);
        item_go.transform.SetParent(parent, false);
        item_go.transform.SetSiblingIndex(0);
        item_go.transform.position = position.position;
        return item_go;
    }
    private GameObject GetRandomCard()
    {
        return cards[UnityEngine.Random.Range(0, cards.Count)];
    }
}
