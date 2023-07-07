using System;
using UnityEngine;

namespace Assets.DwarfTrain.Scripts
{
    internal class PlayerInputEvents
    {
        public static Action<Vector2> OnMove { get; set; }

        public static Action<float> OnZoom { get; set; }
    }
}