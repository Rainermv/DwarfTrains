using System;
using Assets.DwarfTrain.Scripts.Train;
using Cinemachine;
using UnityEngine;

namespace Assets.DwarfTrain.Scripts
{
    public class CameraController
    {
 
        private readonly CinemachineVirtualCamera _camera;
        private readonly CameraControllerConfig _cameraControllerConfig;
        private readonly CinemachineFramingTransposer _framingTransposer;
        public Vector3 CameraOffsetPosition => _framingTransposer.m_TrackedObjectOffset;

        private float _zoomVelocity;
        private float _zoomTarget;

        private Vector3 _offsetTarget;
        private Vector2 _offsetVelocity;


        public CameraController(CinemachineVirtualCamera gameCamera, CameraControllerConfig cameraControllerConfig)
        {
            _camera = gameCamera;
            _cameraControllerConfig = cameraControllerConfig;
            _framingTransposer = gameCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            _cameraControllerConfig = cameraControllerConfig;

            _offsetTarget = _framingTransposer.m_TrackedObjectOffset;
            _offsetVelocity = Vector3.zero;

            _zoomTarget = _camera.m_Lens.OrthographicSize;

            cameraControllerConfig.MinZoom =
                Mathf.Max(1, cameraControllerConfig.MinZoom); // MinZoom can't be less than 1

            cameraControllerConfig.MaxZoom =
                Mathf.Max(cameraControllerConfig.MinZoom +1,
                    cameraControllerConfig.MaxZoom ); //MaxZoom can't be less or equal than MinZoom

            PlayerInputEvents.OnMove += SetCameraOffsetTarget;
            PlayerInputEvents.OnZoom += SetCameraZoomTarget;
        }

        private void SetCameraZoomTarget(float value)
        {
            if (value == 0) return;

            _zoomTarget = Mathf.Clamp(_camera.m_Lens.OrthographicSize + value * _cameraControllerConfig.ZoomFactor,
                _cameraControllerConfig.MinZoom,
                _cameraControllerConfig.MaxZoom);
        }

        private void SetCameraOffsetTarget(Vector2 movementVector)
        {
            if (movementVector == Vector2.zero) return;

            var offsetMovement = movementVector 
                                 * _cameraControllerConfig.MovementSpeed 
                                 * _camera.m_Lens.OrthographicSize; // multiply by orthographic size so it's faster when it's far away

            // clamp offset to the offset boundaries
            _offsetTarget = new Vector2(Mathf.Clamp(_framingTransposer.m_TrackedObjectOffset.x + offsetMovement.x,
                    _cameraControllerConfig.OffsetBoundaryMin.x,
                    _cameraControllerConfig.OffsetBoundaryMax.x),

                Mathf.Clamp(_framingTransposer.m_TrackedObjectOffset.y + offsetMovement.y,
                    _cameraControllerConfig.OffsetBoundaryMin.y,
                    _cameraControllerConfig.OffsetBoundaryMax.y));
        }

        [Serializable]
        public class CameraControllerConfig
        {
            public float MovementSpeed;
            public Vector2 OffsetBoundaryMin;
            public Vector2 OffsetBoundaryMax;
            public float ZoomFactor;
            public float MinZoom;
            public float MaxZoom;
        }

        public void Update(float deltaTime)
        {
            // smoothly update the offset of the camera respecting the boundaries
            _framingTransposer.m_TrackedObjectOffset = Vector2.SmoothDamp(
                _framingTransposer.m_TrackedObjectOffset, _offsetTarget, 
                ref _offsetVelocity, 0.2f);

            // smoothly update the orthographic size of the camera to simulate a zoom of the camera
            _camera.m_Lens.OrthographicSize = Mathf.SmoothDamp(
                _camera.m_Lens.OrthographicSize, _zoomTarget,
                ref _zoomVelocity, 0.2f); 
        }

    
    }
}