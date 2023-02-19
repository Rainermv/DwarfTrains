using Dreamteck.Forever;
using UnityEngine;

namespace Assets.DwarfTrain.Scripts
{
    public class TrackPathGenerator : HighLevelPathGenerator
    {
        protected override void GeneratePoint(ref Point point, int pointIndex)
        {
            base.GeneratePoint(ref point, pointIndex);
            orientation += new Vector3(Random.Range(-10f, 10f), Random.Range(-
                10f, 10f), 0f);
            point.position = GetPointPosition();
        }


    }
}