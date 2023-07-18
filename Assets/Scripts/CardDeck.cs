using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    [SerializeField] private float deckUp = 0f;
    [SerializeField] private float deckDown = -150f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnMouseEnterCardDeck()
    {
        transform.position = new Vector3(transform.position.x,deckUp,transform.position.z);
    }
    public void OnMouseExitCardDeck()
    {
        transform.position = new Vector3(transform.position.x, deckDown, transform.position.z);
    }
}
