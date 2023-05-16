using Dreamteck.Splines;
using UnityEngine;

namespace Assets.DwarfTrain.Scripts.Track
{
    internal class TrackPosition
    {
        public float Distance;
    
        public SplineTracer SplineTracer;
        private const float BASE_SPEED = 5;

        public Vector3 GetUpdated(float speed)
        {
            if (SplineTracer == null)
                return Vector3.zero;

            Distance += speed * Time.deltaTime;
            SplineTracer.SetDistance(Distance);

            return SplineTracer.modifiedResult.position;
        }

    }
}