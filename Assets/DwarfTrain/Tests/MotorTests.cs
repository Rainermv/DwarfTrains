using System.Collections;
using Assets.DwarfTrain.Scripts.Train.Motor;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Assets.DwarfTrain.Tests
{
    public class MotorTests
    {



        [TestCase(-45F, -45F, 45F, 2F, 2F, 0F)]
        [TestCase(0F, -45F, 45F, 1F, 2F, 0F)]
        [TestCase(45F, -45F, 45F, 0F, 2F, 0F)]

        [TestCase(-45F, -45F, 45F, 1F, 1F, -1F)]
        [TestCase(0F, -45F, 45F, 0F, 1F, -1F)]
        [TestCase(45F, -45F, 45F, -1F, 1F, -1F)]
        public void CalculateSlopeCoeficientTest(float slopeAngle, float minAngle, float maxAngle, float expected,
            float maxDescendingAngleNormalizedValue, float maxAscendingAngleNormalizedValue)
        {
            var slopeFactor = BasicTrainMotor.CalculateSlopeCoeficient(slopeAngle, minAngle, maxAngle,  maxDescendingAngleNormalizedValue,  maxAscendingAngleNormalizedValue);
            Assert.AreEqual(expected, slopeFactor);
            // Use the Assert class to test conditions
        }

        [TestCase(10F, 10F, 1F)]
        [TestCase(20F, 10F, 2F)]
        [TestCase(5F, 10F, 0.5F)]
        public void CalculateMotorForceTest(float power, float weight, float expected)
        {
            var result = BasicTrainMotor.CalculateMotorForce(power, weight);
            Assert.AreEqual(expected, result);

        }
        [TestCase(10F, +0F, 0f, +00)] // no slope, no traction, 
        [TestCase(10F, +1F, 0f, +10)] // ascending, no traction, 
        [TestCase(10F, -1F, 0f, -10)] // descending, no traction,

        [TestCase(10F, +0F, 5f, +00)] // no slope, half-traction,
        [TestCase(10F, +1F, 5f, +05)] // ascending, half-traction, 
        [TestCase(10F, -1F, 5f, -10)] // descending, half-traction,

        [TestCase(10F, +0F, 10f, +00)] // no slope, full-traction, 
        [TestCase(10F, +1F, 10f, +00)] // ascending, full-traction, 
        [TestCase(10F, -1F, 10f, -10)] // descending, full-traction, 

        public void CalculateSlopeModifierTest(float weight, float slopeCoeficient, 
            float traction, double expected)
        {
            var result = BasicTrainMotor.CalculateSlopeModifier(slopeCoeficient, weight, traction);
            Assert.AreEqual(expected, result);

        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator MotorTestsWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
