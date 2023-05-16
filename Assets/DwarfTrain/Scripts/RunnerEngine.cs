using Dreamteck.Forever;
using UnityEngine;

namespace Assets.DwarfTrain.Scripts
{
    public class RunnerEngine : MonoBehaviour
    {

        public Runner runner;

        // Start is called before the first frame update
        void Start()
        {
       
        }

        // Update is called once per frame
        void Update()
        {
            //Rotatelevelspline();
        }

        private void Rotatelevelspline()
        {
            //runner.result.
            //runner.segment.
            //var z = runner.result.rotation.z;
            var up = runner.result.up;
            Debug.Log(up);
            var angle = Vector2.Angle(Vector2.up, up); // angle from Z
            //Debug.Log($"angle {angle}");
            //Debug.Log($"UP {up} - Z {z} - angle {angle}");

            transform.rotation = Quaternion.identity;
            transform.Rotate(new Vector3(0, 0, 1), -angle);
            //Debug.Log(runner.result.position);
            //var rot = runner.;
            //transform.rotation = rot;
        }
    }
}
