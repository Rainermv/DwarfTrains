using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TrackNode : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

        TrackNodeEvent.Start(this);
        //ExecuteEvents.Execute<ITrackNodeEvent>()
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

internal static class TrackNodeEvent
{
    public static Action<TrackNode> OnTrackNodeEventStart;

    public static void Start(TrackNode node)
    {
        OnTrackNodeEventStart?.Invoke(node);
    }
}

