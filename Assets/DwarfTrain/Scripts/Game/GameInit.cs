using System.Collections.Generic;
using System.Linq;
using Assets.DwarfTrain.Scripts.Track;
using Assets.DwarfTrain.Scripts.Train;
using Assets.DwarfTrain.Scripts.Train.Motor;
using Assets.DwarfTrain.Scripts.UI;
using Cinemachine;
using Dreamteck.Forever;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.DwarfTrain.Scripts.Game
{
    public class GameInit : MonoBehaviour
    {
        public bool GenerateTracks;
        public int WagonsToGenerate;


        public CameraController.CameraControllerConfig cameraControllerConfig;

        // Scene Objects
        public Transform actorsTransform;
        public Transform worldTransform;


        //public SplineTracer TrackSplineTracer;
        //public SplineComputer TrackSplineComputer;
        
        public LevelGenerator levelGenerator;

        public CinemachineVirtualCamera gameCamera;
        public CinemachineVirtualCamera backgroundCamera;

        
        // UI
        //public TextMeshProUGUI SpeedText;
        public ScrollingBackground[] scrollingBackgrounds; 

        // Input
        public PlayerInput playerInput;

        // Prefabs
        public TrainComponent EnginePrefab;
        public TrainComponent WagonPrefab;
        public TrackBaseComponent TrackBaseComponentPrefab;
        public Transform followObjectTransform;
        
        private TrainController _trainController;
        private WorldBuilder _worldBuilder;
        private CameraController _cameraController;
        private PlayerInputController _playerInputController;
        private GamePlayController _gamePlayController;

        void Awake()
        {
            DebugText.InitializeInstance();
            
            var trackBase = Instantiate(TrackBaseComponentPrefab, worldTransform);
            var roadBase = Instantiate(TrackBaseComponentPrefab, worldTransform);
            roadBase.transform.position = new Vector3(0, -1f);
            
            var trackBuilder = new TrackBuilder();
            var obstacleBuilder = new ObstacleBuilder(trackBase.SplineTracer, worldTransform);
            _worldBuilder = new WorldBuilder(trackBuilder, obstacleBuilder, trackBase.SplineComputer, roadBase.SplineComputer);
            
            var trainTransform = new GameObject("Train").transform;
            trainTransform.SetParent(actorsTransform);

            _trainController = new TrainController(
                trainComponents: new LinkedList<TrainComponent>(),
                trainMotor: new BasicTrainMotor()
                {
                    AccelerationFactor = 0.2f,
                    Power = 50f,
                    MinSpeed = 1f,
                    MaxSpeed = 50f,
                    Traction = 50f,
                    HighSlopeAngle = 90f
                }, splineComputer: trackBase.SplineComputer,
                trainTransform: trainTransform,
                linearPosition: 5f,
                linearDistanceBetweenCars: 0.5f);
            
            _trainController.AddComponent(Instantiate(EnginePrefab), 2f, 3f);

            WagonsToGenerate = Mathf.Max(0, WagonsToGenerate);

            for (var i = 0; i < WagonsToGenerate; i++)
            {
                _trainController.AddComponent(Instantiate(WagonPrefab), 1.5f, 1f);
            }

            gameCamera.Follow = _trainController.TrainComponents.First().transform;

            _gamePlayController = new GamePlayController();
            _gamePlayController.AddFollowTarget("fpp", _trainController.Locomotive.transform, new Vector2(0f,-1f));
            
            _playerInputController = new PlayerInputController(playerInput);
            _cameraController = new CameraController(gameCamera, cameraControllerConfig);

            foreach (var scrollingBackground in scrollingBackgrounds)
            {
                scrollingBackground.Initialize(gameCamera.transform, _cameraController.CameraOffsetPosition);
            }

            if (!GenerateTracks) 
                return;

            levelGenerator.StartGeneration(
                () => _trainController.Start());

        }

        // Update is called once per frame
        void Update()
        {
            _gamePlayController.Update(Time.deltaTime);
            _trainController.Update(Time.deltaTime, 0.1f);
            _cameraController.Update(Time.deltaTime);
            
            foreach (var scrollingBackground in scrollingBackgrounds)
            {
                scrollingBackground.CameraOffset = _cameraController.CameraOffsetPosition;
            }

            //SpeedText.text = _trainController?.StatusText;
          

        }
    }
}