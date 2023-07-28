using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] public int cardsCount;
    [SerializeField] private List<Text> pricesList;
    [SerializeField] private Text eventText;
    [SerializeField] private GameObject cardsTriggersObject;
    [SerializeField] private GameObject cardsEventObject;
    [SerializeField] private GameObject buttonPref;
    [SerializeField] private List<GameObject> cards;
    [SerializeField] private List<GameObject> cardsEvent;
    [SerializeField] private List<Button> buttonsTrigger;
    private void Awake()
    {
        cards.AddRange(Array.ConvertAll(Resources.LoadAll("Cards", typeof(GameObject)), assets => (GameObject)assets));
        eventText = GetComponentInChildren<Text>();
    }
    private void buttonCallBack(Button buttonPressed)
    {
        Debug.Log("Button presed");
        var index = 0;
        for (int i = 0; i < buttonsTrigger.Count; i++)
        {
            if (buttonsTrigger[i] == buttonPressed)
            {
                index = i;
                break;
            }
        }
        var money = Player.Instance.money - Convert.ToInt32(pricesList[index].text);
        if (money >=0)
        {
            buttonPressed.gameObject.transform.Find("SoldCardFrame").gameObject.SetActive(true);
            CardDeck.Instance.AddCardToCardDeck(cardsEvent[index]);
            buttonPressed.interactable = false;
            Player.Instance.money = money;
        }

        // gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        cardsEvent.Clear();
        pricesList.Clear();
        buttonsTrigger.Clear();
        if (cardsCount < 3) cardsCount = 3;

        eventText.text = "Shop";
        CreateButton();

        for (int i = 0; i < cardsCount; i++)
        {
            cardsEvent.Add(GetRandomCard());
            var card = AddCard(cardsEvent[i], cardsEventObject.transform);
            pricesList[i].text = card.GetComponent<Card>().price.ToString();
            cardsEvent[i] = card;
        }

    }

    private void CreateButton()
    {

        for (int i = 0; i < cardsCount; i++)
        {
            var button = AddCard(buttonPref, cardsTriggersObject.transform);
            pricesList.Add(button.GetComponentInChildren<Text>());
            buttonsTrigger.Add(button.GetComponent<Button>());
        }

        foreach (var button in buttonsTrigger)
        {
            button.onClick.AddListener(() => buttonCallBack(button));
        }
    }

    private void OnDisable()
    {
        foreach (var card in cardsEvent)
        {
            Destroy(card);
        }
        foreach (var button in buttonsTrigger)
        {
            Destroy(button.gameObject);
        }
    }

    private GameObject AddCard(GameObject cardPref, Transform parent)
    {
        var item_go = Instantiate(cardPref);
        item_go.transform.SetParent(parent, false);
        item_go.transform.SetSiblingIndex(0);
        return item_go;
    }

    private GameObject GetRandomCard()
    {
        return cards[UnityEngine.Random.Range(0, cards.Count)];
    }
}
