using Dreamteck.Splines;
using UnityEngine;

namespace Assets.DwarfTrain.Scripts.PathMovement
{
    public class TracerSplinePosition : ISplinePosition
    {
        public float Distance { get; set; }

        public TracerSplinePosition(float initialDistance)
        {
            Distance = initialDistance;
        }
    
        public SplineTracer splineTracer;

        public Vector3 Update(float speed, float deltaTime)
        {
            if (splineTracer == null)
                return Vector3.zero;

            Distance += speed * deltaTime;
            splineTracer.SetDistance(Distance);

            return splineTracer.modifiedResult.position;
        }


    }

    public interface ISplinePosition
    {
        Vector3 Update(float speed, float deltaTime);
        float Distance { get; set; }
    }

    class ProjectorSplinePosition : ISplinePosition
    {
        public float Distance { get; set; }
        public SplineProjector splineProjector;


        public ProjectorSplinePosition(float initialDistance)
        {
            Distance = initialDistance;
        }


        public Vector3 Update(float speed, float deltaTime)
        {
            if (splineProjector == null)
                return Vector3.zero;

            Distance += speed * deltaTime;
            splineProjector.SetDistance(Distance);

            return splineProjector.modifiedResult.position;
        }


    }
}