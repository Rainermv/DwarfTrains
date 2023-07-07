using System.Collections.Generic;
using Assets.DwarfTrain.Scripts.PathMovement;
using Assets.DwarfTrain.Scripts.Track;
using Dreamteck.Splines;
using UnityEngine;

namespace Assets.DwarfTrain.Scripts.Train
{
    public class TrainComponent : MonoBehaviour
    {
        public TrackCaster TrackCaster;

        public SplineTracer splineTracer;

        //private TrackRotation _trackRotation = new();
        //public TrackPosition TrackPosition = new();        


        public float Weight { get; set; }

        public float LinearSize { get; set; }
        public TrainController ParentTrainController { get; set; }

        public List<TrainModule> TrainModules;

        public SplinePathMovementController PathMovementController { get; private set; }

        public void Initialize(float initialDistance, float weight, float linearSize, TrainController parentTrainController)
        {
            PathMovementController = new SplinePathMovementController(TrackCaster, splineTracer, initialDistance, 0.5f);
            
            Weight = weight;
            LinearSize = linearSize;
            ParentTrainController = parentTrainController;
        }


        // Update is called once per frame
        void Update()
        {
            transform.rotation = PathMovementController.UpdateRotation(Time.deltaTime);
        }


        public void UpdatePosition(float speed)
        {
            PathMovementController.SpeedOnSpline = speed;
            transform.position = PathMovementController.UpdatePosition(Time.deltaTime);
            //SplineDistance = MotionController.SplineDistance;
        }


        void OnTriggerEnter2D(Collider2D collider)
        {
            TrainComponentEvent.OnCollisionEnterEvent?.Invoke(this, collider);
        }
    }
}