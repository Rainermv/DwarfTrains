using System;
using Assets.DwarfTrain.Scripts.Track;
using Dreamteck.Splines;
using UnityEngine;

namespace Assets.DwarfTrain.Scripts.Train
{
    public class CarComponent : MonoBehaviour
    {
        public CarComponent AttachedFront;
        public CarComponent AttachedRear;
    
        //public IMotionProvider MotionProvider;
        public TrackCaster TrackCaster;

        public SplineTracer splineTracer;
    
        private TrackRotation _trackRotation = new();
        private TrackPosition _trackPosition = new();        
        
        public float Weight;

        public Action OnUpdate;

        public float Angle;

        //private float _baseSpeed = 10f;

        public void Initialize(float initialDistance, float weight)
        {
            _trackPosition.SplineTracer = splineTracer;
            _trackPosition.Distance = initialDistance;
        
            TrackCaster.OnHitTrack += hit => // When raycast hit the track, update angle and rotation
            {
                _trackRotation.SetNormal(hit.normal);
                Angle = Vector2.SignedAngle(Vector2.up, hit.normal);
            };

            Weight = weight;
        }

        // Update is called once per frame
        void Update()
        {
            OnUpdate?.Invoke();
            transform.rotation = _trackRotation.GetUpdated();
        }


        public void UpdatePosition(float speed)
        {
            transform.position = _trackPosition.GetUpdated(speed);
        }
    }
}