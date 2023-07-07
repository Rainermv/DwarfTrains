using System;
using UnityEngine;

namespace Assets.DwarfTrain.Scripts.Track
{
    public class TrackCaster : MonoBehaviour
    {
        public Action<RaycastHit2D> OnHitTrack;

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
}
