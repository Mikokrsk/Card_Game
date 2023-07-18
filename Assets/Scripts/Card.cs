using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Card : MonoBehaviour
{
    [SerializeField] private int index;
    [SerializeField] private GameObject cardFrame;
    // Start is called before the first frame update
    void Start()
    {
    //    cardFrame.active = !cardFrame.active;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CardFrame()
    {
        cardFrame.active = !cardFrame.active;
    }

}
