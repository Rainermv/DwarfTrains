using System.Collections.Generic;
using System.Linq;
using Assets.DwarfTrain.Scripts.Train.Motor;

namespace Assets.DwarfTrain.Scripts.Train
{
    public class TrainController
    {
        public float Velocity { get; private set; }
        public float SlopeAngle { get; set; }

        public LinkedList<CarComponent> Cars { get; set; }

        public ITrackMotor TrackMotor { get; set; }
        public CarComponent Locomotive => Cars.FirstOrDefault();

        public void Update(float deltaTime, float frictionCoeficient)
        {
            var weight = Cars.Sum(c => c.Weight); //todo: calculate this when we change the list

            SlopeAngle = Locomotive.Angle;
            Velocity = TrackMotor.UpdateVelocity(Velocity, SlopeAngle, frictionCoeficient, weight, deltaTime);

            foreach (var carComponent in Cars)
            {
                carComponent.UpdatePosition(Velocity);
            }

        }

    }
}