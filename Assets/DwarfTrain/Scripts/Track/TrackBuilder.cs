using System.Linq;
using Dreamteck.Splines;
using UnityEngine;

namespace Assets.DwarfTrain.Scripts.Track
{
    public class TrackBuilder
    {
      
        public SplinePoint BuildTrackPieceAt(SplineComputer trackSplineComputer, Vector3 position)
        {
            var points = trackSplineComputer.GetPoints().ToList();
            var splinePoint = new SplinePoint(position);
            points.Add(splinePoint);

            trackSplineComputer.SetPoints(points.ToArray());

            return splinePoint;
        }
    }
}
