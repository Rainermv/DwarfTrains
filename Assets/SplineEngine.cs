using Dreamteck.Splines;
using UnityEngine;

public class SplineEngine : MonoBehaviour
{
    public Transform Caster;
    public SplineComputer computer;
    public SplineTracer splineTracer;
    //public ProjectedPlayer projectedPlayer;

    private float distance = 1f;

    // Start is called before the first frame update
    void Start()
    {
        splineTracer.SetDistance(distance);

        //splineTracer.applyDirectionRotation = true;
        //splineTracer.applyDirectionRotation = false;

    }

    private float t = 0;
    private Quaternion previousRotation;
    private Quaternion targetRotation;
    private float lerpRotation;
    private float angle;
    private float speed;

    private const float BASE_SPEED = 5;

    // Update is called once per frame
    void Update()
    {


        distance += Time.deltaTime * speed;
        splineTracer.SetDistance(distance);

        transform.rotation = Quaternion.Lerp(previousRotation, targetRotation, lerpRotation);
        lerpRotation += Time.deltaTime * 5f;

        speed = BASE_SPEED - angle * 0.1f;

        //computer.Evaluatez
        //var tracerSample = splineTracer.posi;

        //Rotatelevelspline(tracerSample);
    }

    void FixedUpdate()
    {
        // Cast a ray straight down.
        RaycastHit2D hit = Physics2D.Raycast(Caster.transform.position, Vector2.down);
        Debug.DrawRay(Caster.transform.position, Vector2.down, Color.blue);

        // If it hits something...
        if (hit.collider != null)
        {
            angle = Vector2.SignedAngle(Vector2.up, hit.normal);
            Debug.Log(angle);

            previousRotation = transform.rotation;
            targetRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            lerpRotation = 0;
        }
    }

    private void Rotatelevelspline(SplineSample splineSample)
    {
        //runner.result.
        //runner.segment.
        //var z = runner.result.rotation.z;
        var up = splineSample.up;
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
