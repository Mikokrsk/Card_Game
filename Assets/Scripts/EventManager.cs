using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class EventManager : MonoBehaviour
{
    [SerializeField] private List<Transform> events;
    [SerializeField] private Transform eventsCanvas;
    // Start is called before the first frame update
    void Start()
    {
        Profiler.BeginSample("11111111111111");
        // events.AddRange(Array.ConvertAll(Resources.LoadAll("Events", typeof(GameObject)), assets => (GameObject)assets));
        events.AddRange(eventsCanvas.GetComponentsInChildren<Transform>());
        Profiler.EndSample();

        /*Profiler.BeginSample("Instead Events");
        foreach (var _event in events)
        {
            var item_go = Instantiate(_event);
            item_go.transform.SetParent(eventsCanvas.transform);
        }
        Profiler.EndSample();*/
    }

    // Update is called once per frame
    void Update()
    {

    }
}
