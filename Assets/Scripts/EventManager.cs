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
        var index = UnityEngine.Random.Range(0, events.Count);
        events[index].gameObject.SetActive(true);
    }
}
