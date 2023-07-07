using UnityEngine;

namespace Assets.DwarfTrain.Scripts.PathMovement
{
    public interface IPathMovementController
    {
        Quaternion UpdateRotation(float deltaTime);
        Vector3 UpdatePosition(float deltaTime);

        float RotationAngle { get; }
    }
}