using UnityEngine;
using UnityEngine.Timeline;

[TrackColor(0.4f, 0.1f, 0.8f)]
[TrackBindingType(typeof(CanvasGroup))] //este objeto es el player data en el script del behaviour correspondiente
[TrackClipType(typeof(CanvasGroupClip))]

public class CanvasGroupTrack : TrackAsset
{
}
