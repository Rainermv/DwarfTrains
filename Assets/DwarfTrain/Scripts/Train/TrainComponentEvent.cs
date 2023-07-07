using System;
using UnityEngine;

namespace Assets.DwarfTrain.Scripts.Train
{
    internal static class TrainComponentEvent
    {
        public static Action<TrainComponent, Collider2D> OnCollisionEnterEvent { get; set; }
    }
}