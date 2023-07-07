using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{ //public Text DebugText;

    public float parallaxFactorX;
    public float parallaxFactorY;
    public float MaxVerticalDisplacement = 5;
    public float VerticalFactor;

    private float _textureRepeatLengthX;
    private float _textureRepeatLengthY;

    private Transform _cameraTransform;     // Reference to the main camera transform

    private Vector2 _savedOffset;
    private Renderer _renderer;

    private Vector3 _previousCameraPosition; // Previous position of the camera
    private Vector3 _initialPosition;
    private float _initial_y;
    private float _verticalDisplacement;

    public Vector2 CameraOffset { get; set; }


    private void LateUpdate()
    {
        if (_cameraTransform == null)
            return;


        // Calculate the camera displacement
        var cameraDisplacement = (_cameraTransform.position - _previousCameraPosition);

        // Update the previous position
        _previousCameraPosition = _cameraTransform.position;

        var repeatTextureOffset_X = Mathf.Repeat(_renderer.material.mainTextureOffset.x + cameraDisplacement.x * parallaxFactorX * Time.deltaTime, _textureRepeatLengthX);
        //var repeatTextureOffset_Y = Mathf.Clamp(_renderer.material.mainTextureOffset.y + cameraDisplacement.y * parallaxFactorY * Time.deltaTime, 0, _textureRepeatLengthY);

        _renderer.material.mainTextureOffset = new Vector2(repeatTextureOffset_X, _renderer.material.mainTextureOffset.y);

        //_verticalDisplacement = Mathf.Lerp(-MaxVerticalDisplacement, MaxVerticalDisplacement,
        //   Mathf.Clamp(CameraOffset.y * parallaxFactorY * Time.deltaTime, 0f, 1f));
        // _verticalDisplacement = 0;// fuck it

        //var target = new Vector3(CameraTransform.position.x, CameraTransform.position.y + VerticalDisplacement, _initialPosition.z);
        //transform.position = Vector3.Lerp(transform.position, target, 0.8f * Time.deltaTime);
        //transform.position = new Vector3(CameraTransform.position.x, CameraTransform.position.y + _initial_y + _verticalDisplacement, _initialPosition.z);
    }

    private void OnDisable()
    {
        //_renderer.material.mainTextureOffset = _savedOffset;
    }

    public void Initialize(Transform cameraTransform, Vector2 cameraControllerOffsetMovement)
    {
        _cameraTransform = cameraTransform;
        CameraOffset = cameraControllerOffsetMovement;

        _renderer = GetComponent<Renderer>();
        _savedOffset = _renderer.material.mainTextureOffset;
        _initialPosition = transform.position;
        _initial_y = transform.localPosition.y;
        _previousCameraPosition = _cameraTransform.position;
        
        _textureRepeatLengthX = 1;
        _textureRepeatLengthY = 1;

        //_renderer.material.SetTextureScale("_MainTex", new Vector2(2,5) );

    }
}