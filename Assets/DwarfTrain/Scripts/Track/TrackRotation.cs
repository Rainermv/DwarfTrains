using UnityEngine;

namespace Assets.DwarfTrain.Scripts.Track
{
    internal class TrackRotation
    {
        private Quaternion previousRotation = Quaternion.identity;
        private Quaternion targetRotation = Quaternion.identity;
        private float lerpRotation;

        public Quaternion GetUpdated()
        {
            lerpRotation += Time.deltaTime * 0.5f;
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