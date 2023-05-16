using UnityEngine;

namespace Assets.DwarfTrain.Scripts.Train.Motor
{
    public class BasicTrackMotor : ITrackMotor
    {
        public float Power { get; set; } // How much weight it can pull, affects acceleration (a = power / weight)
        public float MinVelocity { get; set; }

        public float MaxVelocity { get; set; }
        public float AccelerationFactor { get; set; } // Adjusts the acceleration to proper values
        public float Traction { get; set; } // reduces the effective weight on ascending slopes
        public float SlopeCoeficient { get; private set; }


        float MAX_ANGLE = 45;
        float MIN_ANGLE = -45;

        private float acceleration;
        private float slopeModifier;
        private float motorForce;
        private float frictionModifier;


        public float UpdateVelocity(float velocity,
            float slopeAngle,
            float frictionCoeficient,
            float weight,
            float deltaTime)
        {
            
            // Power / weight 
            motorForce = CalculateMotorForce(Power, weight);
            
            frictionModifier = motorForce * frictionCoeficient;

            SlopeCoeficient = CalculateSlopeCoeficient(slopeAngle, MIN_ANGLE, MAX_ANGLE, 1, -1);
            slopeModifier = CalculateSlopeModifier(SlopeCoeficient, weight, Traction);

            acceleration = (motorForce - frictionModifier + slopeModifier) * AccelerationFactor;

            // Adjust acceleration using factor and time 
            velocity += acceleration* deltaTime ;

            // Calculate the speed based on the power of the engine and the total weight pulled
            //speed += (Power / weight * Acceleration - attrition - (-SlopeFactor * Traction)) * deltaTime;

            return Mathf.Clamp(velocity, MinVelocity, MaxVelocity); 
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