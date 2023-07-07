using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Assets.DwarfTrain.Scripts.Track
{
    public class TrackNode : MonoBehaviour
    {
        public bool HasObstacle;


        // Start is called before the first frame update
        void Start()
        {
            TrackNodeEvent.OnStart(this);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}