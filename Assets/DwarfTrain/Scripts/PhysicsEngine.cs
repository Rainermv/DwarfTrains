using UnityEngine;

namespace Assets.DwarfTrain.Scripts
{
    public class PhysicsEngine : MonoBehaviour
    {
        public Rigidbody2D rigidbody2D;
        //public ConstantForce2D constantForce2D;
        public WheelJoint2D[] wheels;

        // Start is called before the first frame update
        void Start()
        {
            //rigidbody2D.isKinematic = true;
        }

        // Update is called once per frame
        void Update()
        {
            var speedMod = 100;
            if (Input.GetKeyDown(KeyCode.W ))
            {
            
                foreach (var wheelJoint2D in wheels)
                {
                    var motor = wheelJoint2D.motor;
                    motor.motorSpeed += speedMod;
                    wheelJoint2D.motor = motor;
                    Debug.Log(motor.motorSpeed);
                }
                //rigidbody2D.isKinematic = false;
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                foreach (var wheelJoint2D in wheels)
                {
                    var motor = wheelJoint2D.motor;
                    motor.motorSpeed -= speedMod;
                    wheelJoint2D.motor = motor;
                    Debug.Log(motor.motorSpeed);

                }
            }
        }
    }
}
