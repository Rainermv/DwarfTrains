using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.DwarfTrain.Scripts.Train;
using Assets.DwarfTrain.Scripts.Train.Motor;
using Cinemachine;
using Dreamteck;
using Dreamteck.Forever;
using Dreamteck.Splines;
using TMPro;
using UnityEngine;

public class GameInit : MonoBehaviour
{
    public TextMeshProUGUI SpeedText;
    public TextMeshProUGUI SlopeText;
    public TextMeshProUGUI MotorText;


    public CarComponent EnginePrefab;
    public CarComponent WagonPrefab;
    
    public SplineComputer SplineComputer;
    public LevelGenerator levelGenerator;
    public CinemachineVirtualCamera camera;

    public Transform Actors;

    private TrainController _trainController;

    public int weightMultiplier;


    // Start is called before the first frame update

    void Awake()
    {
    }
    void Start()
    {
        levelGenerator.StartGeneration(() =>
        
        {
            _trainController = new TrainController
            {
                Cars = new LinkedList<CarComponent>(),
                TrackMotor = new BasicTrackMotor()
                {
                    AccelerationFactor = 0.2f,
                    Power = 8f,
                    MinVelocity = 0.1f,
                    MaxVelocity = 10f,
                    Traction = 0.1f,
                }
            };

            weightMultiplier = 1;
            var cars = 5;
            var baseDistance = 7.5;
            var distanceMod = -2;
            _trainController.Cars.AddFirst(MakeCar(10, EnginePrefab, 3 * weightMultiplier));
            for (var i = 0; i < cars; i++)
            {
                _trainController.Cars.AddLast(MakeCar((float)(baseDistance + distanceMod * i), WagonPrefab, 1 * weightMultiplier));
            }
            
            camera.Follow = _trainController.Cars.First().transform;

        });
            
    }

    private CarComponent MakeCar(float distance, CarComponent CarComponentPrefab, float weight)
    {
        var carComponent = Instantiate(CarComponentPrefab);
        carComponent.splineTracer.spline = SplineComputer;
  
        carComponent.Initialize(distance, weight);

        carComponent.transform.SetParent(Actors);
        
        return carComponent;
    }

    // Update is called once per frame
    void Update()
    {
        _trainController?.Update(Time.deltaTime, 0.1f);

        SpeedText.text = $"Speed " + _trainController?.Velocity.ToString("#.##");
        SlopeText.text = "Slope " + _trainController?.SlopeAngle.ToString("#.##");
        MotorText.text = _trainController?.TrackMotor?.ToString();

    }
}
