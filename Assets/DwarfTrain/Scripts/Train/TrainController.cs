using System;
using System.Collections.Generic;
using System.Linq;
using Assets.DwarfTrain.Scripts.Train.Motor;
using Assets.DwarfTrain.Scripts.UI;
using Dreamteck.Splines;
using UnityEngine;

namespace Assets.DwarfTrain.Scripts.Train
{
    public class TrainController
    {

        public TrainController(LinkedList<TrainComponent> trainComponents,
            ITrainMotor trainMotor,
            SplineComputer splineComputer,
            Transform trainTransform,
            float linearPosition,
            float linearDistanceBetweenCars)
        {
            TrainComponents = trainComponents;
            TrainMotor = trainMotor;
            SplineComputer = splineComputer;
            TrainTransform = trainTransform;
            LinearPosition = linearPosition;
            LinearDistanceBetweenCars = linearDistanceBetweenCars;

            DebugText.Instance.Add(ref _onTrainUpdated);
        }

        private Action<string> _onTrainUpdated;

        public float Speed { get; set; }
        public float SlopeAngle { get; set; }

        public LinkedList<TrainComponent> TrainComponents { get; set; }

        public ITrainMotor TrainMotor { get; set; }
        public TrainComponent Locomotive => TrainComponents.FirstOrDefault();
        public SplineComputer SplineComputer { get; set; }
        public Transform TrainTransform { get; set; }
        public float LinearPosition { get; set; }
        public float LinearDistanceBetweenCars { get; set; }

        public float Weight => TrainComponents?.Sum(c => c.Weight) ?? 0f;
        public float ImpactForce => 0.5f * Weight * (Speed * Speed);

        public string StatusText
        {
            get
            {
                var lines = new List<string>
                {
                    $"Power " + TrainMotor.CurrentPower.ToString("#.##"),
                    $"Speed " + Speed.ToString("#.##"),
                    $"Impact Force " + ImpactForce.ToString("#.##"),
                    "Slope " + SlopeAngle.ToString("#.##")
                };

                return string.Join("\n", lines);
            }
        }

        public void Start()
        {

            TrainMotor.Forward();
        }
        public void Stop()
        {
            TrainMotor.Stop();
        }

        public void Update(float deltaTime, float frictionCoeficient)
        {
            SlopeAngle = Locomotive.PathMovementController.RotationAngle;
            Speed = TrainMotor.UpdateSpeed(Speed, SlopeAngle, frictionCoeficient, Weight, deltaTime);

            foreach (var carComponent in TrainComponents)
            {
                carComponent.UpdatePosition(Speed);
            }

            LinearPosition = Locomotive.PathMovementController.SplineDistance;

            _onTrainUpdated?.Invoke(StatusText);

        }

        public void AddComponent(TrainComponent trainComponent, float linearSize, float weight)
        {
            trainComponent.splineTracer.spline = SplineComputer;

            float trainSize = TrainComponents.Sum(car => car.LinearSize + LinearDistanceBetweenCars);
            float linearPosition = LinearPosition - trainSize;
            trainComponent.Initialize(linearPosition, weight, linearSize, this);

            TrainComponents.AddLast(trainComponent);
            trainComponent.transform.SetParent(TrainTransform);

        }


        
    }
}