namespace Assets.DwarfTrain.Scripts.Train.Motor
{
    public interface ITrainMotor
    {
        float UpdateSpeed(float speed, float slopeAngle, float frictionCoeficient, float weight, float deltaTime);
        void Forward();
        void Stop();
        float CurrentPower { get; }
    }
}