using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    [SerializeField] private List<GameObject> events;

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
            events[index].gameObject.SetActive(true);
            Debug.Log($"Event {events[index]} active");
        }
    }
}
