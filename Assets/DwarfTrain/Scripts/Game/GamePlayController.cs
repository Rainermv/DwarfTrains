using System.Collections.Generic;
using Assets.DwarfTrain.Scripts.Track;
using Assets.DwarfTrain.Scripts.Train;
using UnityEngine;

namespace Assets.DwarfTrain.Scripts.Game
{
    internal class GamePlayController
    {
        private const string FOLLOW_OBJECT_PREFAB = "EnemyPrototype";

        private readonly Dictionary<string, FollowTarget> _followTargets = new();

        public GamePlayController()
        {
            TrainComponentEvent.OnCollisionEnterEvent += (trainComponent, collision) =>
            {
                //todo: handle other kinds of collisions
                var obstacleComponent = collision.gameObject.GetComponent<ObstacleComponent>();

                if (trainComponent.ParentTrainController.ImpactForce >= obstacleComponent.ImpactResistance)
                {
                    GameObject.Destroy(obstacleComponent.gameObject);
                    var speedMod = obstacleComponent.ImpactSpeedModifier / trainComponent.ParentTrainController.ImpactForce;
                    trainComponent.ParentTrainController.Speed -= speedMod;
                    return;
                }

                trainComponent.ParentTrainController.Stop();

            };
        }
        
    
        public void Update(float deltaTime)
        {
            

        }

        public void AddFollowTarget(string key, Transform followTargeTransform, Vector2 followOffset)
        {
            var followGameObject = GameObjectFactory.Instantiate<Transform>(FOLLOW_OBJECT_PREFAB);
            //followGameObject.position = followTargeTransform.position + followOffset;
            _followTargets.Add(key, new FollowTarget(followTargeTransform, followOffset));
        }

        
    }

    internal class FollowTarget
    {
        private readonly Transform _targetTransform;
        private readonly Vector2 _offset;

        public FollowTarget(Transform targetTransform, Vector2 offset)
        {
            _targetTransform = targetTransform;
            _offset = offset;
        }
    }
}