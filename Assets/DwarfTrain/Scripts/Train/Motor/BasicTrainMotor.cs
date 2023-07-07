using UnityEngine;

namespace Assets.DwarfTrain.Scripts.Train.Motor
{
    public class BasicTrainMotor : ITrainMotor
    {
        public float Power { get; set; } // How much weight it can pull, affects acceleration (a = power / weight)
        public float MinSpeed { get; set; }

        public float MaxSpeed { get; set; }
        public float AccelerationFactor { get; set; } // Adjusts the acceleration to proper values
        public float Traction { get; set; } // reduces the effective weight on ascending slopes
        public float SlopeCoeficient { get; private set; }


        public float HighSlopeAngle { get; set; }

        private float acceleration;
        private float slopeModifier;
        private float motorForce;
        private float frictionModifier;

        private float _currentPower;
        private float _currentMinSpeed;
        private int _direction;
        private bool _minSpeedReached;

        public float CurrentPower => _currentPower;


        public BasicTrainMotor()
        {
            Stop();
            
        }


        public float UpdateSpeed(float speed,
            float slopeAngle,
            float frictionCoeficient,
            float weight,
            float deltaTime)
        {
            
            // Power / weight 
            motorForce = CalculateMotorForce(_currentPower, weight);
            
            frictionModifier = motorForce * frictionCoeficient;

            SlopeCoeficient = CalculateSlopeCoeficient(slopeAngle, -HighSlopeAngle, HighSlopeAngle, 1, -1);
            slopeModifier = CalculateSlopeModifier(SlopeCoeficient, weight, Traction);

            acceleration = (motorForce - frictionModifier + slopeModifier) * AccelerationFactor;

            // Adjust acceleration using factor and time 
            speed += acceleration* deltaTime ;

            // Calculate the speed based on the power of the engine and the total weight pulled
            //speed += (Power / weight * Acceleration - attrition - (-SlopeFactor * Traction)) * deltaTime;

            if (speed > _currentMinSpeed)
            {
                _minSpeedReached = true;
            }

            return Mathf.Clamp(speed, _minSpeedReached?_currentMinSpeed : 0, MaxSpeed); 
        }

        public void Forward()
        {
            _currentPower = Power;
            _currentMinSpeed = +MinSpeed;
            _direction = +1;
            _minSpeedReached = false;
        }

        public void Stop()
        {
            _currentPower = 0f;
            _currentMinSpeed = 0f;
        }


        public static float CalculateMotorForce(float power, float weight)
        {
            return power / weight;
        }

        public static float CalculateSlopeCoeficient(float slopeAngle, float maxDescendingAngle, float maxAscendingAngle,
            float maxDescendingAngleNormalizedValue, float maxAscendingAngleNormalizedValue)
        {

            return (slopeAngle - maxDescendingAngle) /
                   (maxAscendingAngle - maxDescendingAngle) *
                   (maxAscendingAngleNormalizedValue - maxDescendingAngleNormalizedValue) +
                   maxDescendingAngleNormalizedValue;
        }

        public override string ToString()
        {
            var properties = "Force: " + motorForce.ToString("#.##") + "\n";
            properties += "Friction Mod.: " + frictionModifier.ToString("#.##") + "\n";
            properties += "Slope Coef.: " + SlopeCoeficient.ToString("#.##") + "\n";
            properties += "Slope Modifier: " + slopeModifier.ToString("#.##") + "\n";
            properties += "Acceleration: " + acceleration.ToString("#.##") + "\n";


            return properties;
        }

        
        public static float CalculateFrictionModifier(float slopeCoeficient, float frictionCoeficient, float weight,
            float traction)
        {
            // friction is impacted by the weight
            return weight * frictionCoeficient; ;
        }

        public static float CalculateSlopeModifier(float slopeCoeficient, float weight, float traction)
        {
            // Traction only applies on ascending slopes
            var tractionModifier = Mathf.Max(0f, traction * slopeCoeficient);

            // traction mitigates the weight on slopes
            var modifiedWeight = Mathf.Max(0f, weight - tractionModifier);

            // Faster when descending, slower when ascending
            return modifiedWeight * slopeCoeficient;
        }
    }
}