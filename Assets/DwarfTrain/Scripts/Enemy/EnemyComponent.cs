using System.Collections;
using System.Collections.Generic;
using Assets.DwarfTrain.Scripts.Enemies;
using UnityEngine;

public class EnemyComponent : MonoBehaviour
{
    private EnemyController _enemyController;

    public void Initialize(EnemyController enemyController)
    {
        _enemyController = enemyController;
    }

    public void Update()
    {
        transform.position = _enemyController.UpdatePosition(Time.deltaTime);
        transform.rotation = _enemyController.UpdateRotation(Time.deltaTime);
    }
}
