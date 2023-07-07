using Assets.DwarfTrain.Scripts.Behaviours.MovementBehaviour;
using Assets.DwarfTrain.Scripts.PathMovement;
using UnityEngine;

namespace Assets.DwarfTrain.Scripts.Enemies
{
    public class EnemyController
    {
        public IMovementBehaviour MovementBehaviour { get; set; }
        public IPathMovementController PathMovementController { get; set; }
        
        public EnemyController(IMovementBehaviour movementBehaviour, IPathMovementController pathMovementController)
        {
            MovementBehaviour = movementBehaviour;
            PathMovementController = pathMovementController;

        }


        public Vector3 UpdatePosition(float deltaTime)
        {
            MovementBehaviour.Update(deltaTime);

            return PathMovementController.UpdatePosition(deltaTime);
        }

        public Quaternion UpdateRotation(float deltaTime)
        {
            return Quaternion.identity;
            
        }
    }
}