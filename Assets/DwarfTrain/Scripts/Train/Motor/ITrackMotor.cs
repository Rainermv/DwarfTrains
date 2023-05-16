namespace Assets.DwarfTrain.Scripts.Train.Motor
{
    public interface ITrackMotor
    {
        float UpdateVelocity(float speed, float slopeAngle, float frictionCoeficient, float weight, float deltaTime);
    }

    class StationaryTrackMotor : ITrackMotor
    {
        public float UpdateVelocity(float speed, float slopeAngle, float frictionCoeficient, float weight, float deltaTime)
        {
            return 0;
        }
    }
}