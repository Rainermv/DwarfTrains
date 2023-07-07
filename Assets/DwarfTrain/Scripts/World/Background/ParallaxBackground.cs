using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public Transform[] layers;          // Array of background layers

    public float parallaxFactor;
    //public float[] parallaxFactors;     // Parallax factors for each layer
    public float smoothing = 1f;        // Smoothing factor for parallax effect

    public Transform camTransform;     // Reference to the main camera transform
    private Vector3 previousCamPosition; // Previous position of the camera

    private void Start()
    {
        //camTransform = Camera.main.transform;
        previousCamPosition = camTransform.position;
    }

    private void LateUpdate()
    {
        for (int i = 0; i < layers.Length; i++)
        {
            float parallaxOffset = (previousCamPosition.x - camTransform.position.x) * parallaxFactor * i;
            Vector3 layerTargetPosition = new Vector3(layers[i].position.x - parallaxOffset, layers[i].position.y, layers[i].position.z);

            // Check if the layer needs to wrap around
            if (ShouldWrapLayer(layerTargetPosition, layers[i].position))
            {
                WrapLayer(layers[i]);

            }

            // Smoothly move the layer towards the target position
            layers[i].position = Vector3.Lerp(layers[i].position, layerTargetPosition, smoothing * Time.deltaTime);
        }
        
        previousCamPosition = camTransform.position;
    }

    // Check if the layer needs to wrap around
    private bool ShouldWrapLayer(Vector3 targetPosition, Vector3 currentPosition)
    {
        // Assuming the layer's width is greater than the camera's width
        // Check if the layer's position has moved beyond the camera's bounds
        return targetPosition.x - currentPosition.x > camTransform.position.x;
    }

    // Wrap the layer to the opposite side
    private void WrapLayer(Transform layer)
    {
        // Assuming the layer's width is greater than the camera's width
        // Calculate the new position on the opposite side
        Vector3 wrapPosition = new Vector3(layer.position.x - GetLayerWidth(layer), 0, layer.position.z);
        layer.position = wrapPosition;
    }

    // Get the width of the layer by calculating the distance between the first and last child
    private float GetLayerWidth(Transform layer)
    {
        Transform firstChild = layer.GetChild(0);
        Transform lastChild = layer.GetChild(layer.childCount - 1);
        return Mathf.Abs(lastChild.position.x - firstChild.position.x);
    }
}
