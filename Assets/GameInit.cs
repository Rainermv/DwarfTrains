using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using Dreamteck;
using Dreamteck.Forever;
using Dreamteck.Splines;
using UnityEngine;

public class GameInit : MonoBehaviour
{
    public CarComponent EnginePrefab;
    public CarComponent WagonPrefab;
    
    public SplineComputer SplineComputer;
    public LevelGenerator levelGenerator;
    public CinemachineVirtualCamera camera;

    public Transform Actors;


    private List<CarComponent> carComponents = new();

    // Start is called before the first frame update

    void Awake()
    {
    }
    void Start()
    {
        var distance = 3;
        levelGenerator.StartGeneration(() =>
        {
            MakeCar(10, EnginePrefab);
            MakeCar(7.5f, WagonPrefab);
            MakeCar(5.5f, WagonPrefab);
            
            camera.Follow = carComponents.First().transform;

        });
            
    }

    private void MakeCar(float distance, CarComponent CarComponentPrefab)
    {
        var carComponent = Instantiate(CarComponentPrefab);
        carComponent.splineTracer.spline = SplineComputer;
        carComponent.AttachedTo = carComponents.LastOrDefault();
        carComponents.Add(carComponent);

        carComponent.Initialize(distance);

        carComponent.transform.SetParent(Actors);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
