using Assets.DwarfTrain.Scripts.Track;
using Dreamteck.Splines;
using UnityEngine;

namespace Assets.DwarfTrain.Scripts
{
    internal class WorldBuilder
    {
        private readonly TrackBuilder _trackBuilder;
        private readonly ObstacleBuilder _obstacleBuilder;

        public WorldBuilder(TrackBuilder trackBuilder, ObstacleBuilder obstacleBuilder,
            SplineComputer trackBaseSplineComputer, SplineComputer roadBaseSplineComputer)
        {
            _trackBuilder = trackBuilder;
            _obstacleBuilder = obstacleBuilder;

            TrackNodeEvent.OnTrackNodeStart += (trackNode) =>
            {
                var trackPoint = _trackBuilder.BuildTrackPieceAt(trackBaseSplineComputer, trackNode.transform.position);

                _trackBuilder.BuildTrackPieceAt(roadBaseSplineComputer, trackNode.transform.position + Vector3.down);

                if (Random.Range(0f, 1f) > 0.3f)
                    _obstacleBuilder.BuildObstacleAt(trackPoint);

            };
        }
    }
}