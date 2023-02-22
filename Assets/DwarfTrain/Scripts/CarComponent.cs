using Dreamteck.Splines;
using UnityEngine;

public class CarComponent : MonoBehaviour
{
    public CarComponent AttachedTo;
    public TrackCaster TrackCaster;

    public SplineTracer splineTracer;
    
    private TrackRotation _trackRotation = new();
    private TrackPosition _trackPosition = new();

    public float Speed;

    private float _signedAngle;

    private float _baseSpeed = 10f;

    public void Initialize(float initialDistance)
    {
        _trackPosition.SplineTracer = splineTracer;
        _trackPosition.Distance = initialDistance;


        TrackCaster.OnHitTrack += hit => // When raycast hit the track, update angle and rotation
        {
            _trackRotation.SetNormal(hit.normal);
            _signedAngle = Vector2.SignedAngle(Vector2.up, hit.normal);
        };
    }

    // Update is called once per frame
    void Update()
    {
        Speed = AttachedTo != null
            ? AttachedTo.Speed
            : _baseSpeed - _signedAngle * 0.1f;

        transform.position = _trackPosition.GetUpdated(Speed);
        transform.rotation = _trackRotation.GetUpdated();
    }

    
}