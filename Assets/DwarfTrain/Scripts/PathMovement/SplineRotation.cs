using UnityEngine;

namespace Assets.DwarfTrain.Scripts.PathMovement
{
    internal class SplineRotation
    {
        private Quaternion previousRotation = Quaternion.identity;
        private Quaternion targetRotation = Quaternion.identity;
        private float lerpRotation;
        private float rotationSpeed;

        public SplineRotation(float rotationSpeed)
        {
            this.rotationSpeed = rotationSpeed;
        }

        public Quaternion Update(float deltaTime)
        {
            lerpRotation += deltaTime * rotationSpeed;
            return Quaternion.Lerp(previousRotation, targetRotation, lerpRotation);

        }

        public void SetNormal(Vector2 normal)
        {
            // var angle = Vector2.SignedAngle(Vector2.up, normal);
            previousRotation = targetRotation;
            targetRotation = Quaternion.FromToRotation(Vector2.up, normal);
            lerpRotation = 0;
        
        }
    }
}