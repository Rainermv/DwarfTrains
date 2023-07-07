using Assets.DwarfTrain.Scripts.Track;
using Dreamteck.Splines;
using UnityEngine;

namespace Assets.DwarfTrain.Scripts.PathMovement
{
    public class SplinePathMovementController : IPathMovementController
    {
        private readonly TrackCaster _trackCaster;
        private readonly SplineTracer _splineTracer;
        private readonly TracerSplinePosition _tracerSplinePosition;
        private readonly SplineRotation _splineRotation;
        private Vector2 _destinationVector2;
        public float SpeedOnSpline { get; set; }


        public SplinePathMovementController(TrackCaster trackCaster, 
            SplineTracer splineTracer, 
            float initialDistance,
            float rotationSpeed)
        {
            _trackCaster = trackCaster;
            _splineTracer = splineTracer;

            _tracerSplinePosition = new TracerSplinePosition(initialDistance);
            _splineRotation = new SplineRotation(rotationSpeed);

            _tracerSplinePosition.splineTracer = splineTracer;

            trackCaster.OnHitTrack += hit => // When raycast hit the track, update angle and rotation
            {
                _splineRotation.SetNormal(hit.normal);
                RotationAngle = Vector2.SignedAngle(Vector2.up, hit.normal);
            };

        }

        public float RotationAngle { get; set; }

        public float SplineDistance => _tracerSplinePosition.Distance;

        public Quaternion UpdateRotation(float deltaTime)
        {
            return _splineRotation.Update(deltaTime);
        }

        public Vector3 UpdatePosition(float deltaTime)
        {
            return _tracerSplinePosition.Update(SpeedOnSpline, deltaTime);
        }

    }
}