using System;

namespace Assets.DwarfTrain.Scripts.Track
{
    internal static class TrackNodeEvent
    {
        public static Action<TrackNode> OnTrackNodeStart;

        public static void OnStart(TrackNode node)
        {
            OnTrackNodeStart?.Invoke(node);
        }
    }
}