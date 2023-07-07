using Assets.DwarfTrain.Scripts.Track;
using Dreamteck.Splines;
using UnityEngine;

namespace Assets.DwarfTrain.Scripts
{
    internal class ObstacleBuilder
    {
        private readonly SplineTracer _trackSplineTracer;
        private readonly Transform _worldTransform;

        public ObstacleBuilder(SplineTracer trackSplineTracer, Transform worldTransform)
        {
            _trackSplineTracer = trackSplineTracer;
            _worldTransform = worldTransform;
        }

        public void BuildObstacleAt(SplinePoint trackPoint)
        {
            // instantiate obstacle
            var obstacle = GameObjectFactory.Instantiate<ObstacleComponent>("Obstacle");
            obstacle.transform.position = trackPoint.position;
            var scale = Random.Range(0.2f, 2.5f);
            obstacle.transform.localScale = new Vector3(scale, scale, 1);
            obstacle.ImpactSpeedModifier *= scale;
            obstacle.transform.SetParent(_worldTransform, true);

            // Find out the position on the spline
            //var projectResult = new SplineSample();
            //_trackSplineTracer.Project(trackNode.transform.position, ref projectResult);


            // set obstacle position
            //obstacle.transform.position = projectResult.position;
            // todo: probably fix position so it's standing on the track (bounds)
        }
    }
}