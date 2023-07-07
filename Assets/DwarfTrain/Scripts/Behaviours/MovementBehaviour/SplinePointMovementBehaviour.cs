using Assets.DwarfTrain.Scripts.PathMovement;
using UnityEngine;

namespace Assets.DwarfTrain.Scripts.Behaviours.MovementBehaviour
{
    class SplinePointMovementBehaviour : IMovementBehaviour, ISplineMovementBehaviour
    {
        public SplinePathMovementController PathMovementController { get; }

        public SplinePointMovementBehaviour(SplinePathMovementController splinePathMovementController)
        {
            PathMovementController = splinePathMovementController;
        }

        public void Update(float deltaTime)
        {

        }
    }

    internal interface ISplineMovementBehaviour
    {
        SplinePathMovementController PathMovementController { get; }
    }
}