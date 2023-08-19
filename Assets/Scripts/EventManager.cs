using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    [SerializeField] private List<GameObject> events;
    [SerializeField] private Button nextEvent;

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

    public void ActiveRandomEvent()
    {
        nextEvent.interactable = false;
        bool isActiveEvent = true;
        foreach (var _event in events)
        {
            if (_event.gameObject.active == true)
            {
                isActiveEvent = false;
                break;
            }
        }
        if (isActiveEvent)
        {            
            var index = UnityEngine.Random.Range(0, events.Count);
            StartCoroutine(ActiveEvent(index));
        }
    }
    IEnumerator ActiveEvent(int index)
    {
        Player.Instance.animator.SetBool("MoveFWD",true);
        yield return new WaitForSeconds(2f);
        Player.Instance.animator.SetBool("MoveFWD", false);
        events[index].gameObject.SetActive(true);
        nextEvent.interactable = true;
        //Debug.Log($"Event {events[index]} active");
    }
}
