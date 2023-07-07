namespace Assets.DwarfTrain.Scripts.Train.Motor
{
    class StationaryTrainMotor : ITrainMotor
    {
        public float UpdateSpeed(float speed, float slopeAngle, float frictionCoeficient, float weight, float deltaTime)
        {
            return 0;
        }

        public void Forward()
        {
            throw new System.NotImplementedException();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }

        public float CurrentPower { get; }
    }
}