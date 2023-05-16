using System.Linq;
using Dreamteck.Splines;
using UnityEngine;

namespace Assets.DwarfTrain.Scripts.Track
{
    public class TrackController : MonoBehaviour
    {

        public SplineComputer splineComputer;
        public SplineRenderer trackRenderer;

        void Awake()
        {
            TrackNodeEvent.OnTrackNodeEventStart += (trackNode) =>
            {
                var points = splineComputer.GetPoints().ToList();
                points.Add(new SplinePoint(trackNode.transform.position));

            
                splineComputer.SetPoints(points.ToArray());
                /*
            var nodeGameObject = new GameObject();
            nodeGameObject.AddComponent<Node>();
            nodeGameObject.transform.position = trackNode.transform.position;
            nodeGameObject.transform.SetParent(splineComputer.transform);

            var n = nodeGameObject.AddComponent<Node>();

            splineComputer.ConnectNode(n, splineComputer.pointCount -1);
            */
                //trackRenderer.uvScale = new Vector2(1, points.Count);

                //var n = new Node(){}
                //splineComputer.SetPoint();

                //splineComputer.ConnectNode(node, splineComputer.pointCount -1);
            };
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
