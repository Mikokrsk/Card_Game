using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class EventManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> events;

    public void ActiveRandomEvent()
    {
        var index = UnityEngine.Random.Range(0, events.Count);
        events[index].gameObject.SetActive(true);
    }
}
