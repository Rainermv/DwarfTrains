using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCaster : MonoBehaviour
{
    public Action<RaycastHit2D> OnHitTrack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {

        // Cast a ray straight down.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
        Debug.DrawRay(transform.position, Vector2.down, Color.blue);

        // If it hits something...
        if (hit.collider != null)
        {
            OnHitTrack?.Invoke(hit);
        }
    }
}
