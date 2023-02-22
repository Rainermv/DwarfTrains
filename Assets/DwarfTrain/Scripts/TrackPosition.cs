using Dreamteck.Splines;
using UnityEngine;

internal class TrackPosition
{
    public float Distance;
    
    public SplineTracer SplineTracer;
    private const float BASE_SPEED = 5;

    public Vector3 GetUpdated(float speed)
    {
        if (SplineTracer == null)
            return Vector3.zero;

        Distance += Time.deltaTime * speed;
        SplineTracer.SetDistance(Distance);

        return SplineTracer.modifiedResult.position;
    }

}