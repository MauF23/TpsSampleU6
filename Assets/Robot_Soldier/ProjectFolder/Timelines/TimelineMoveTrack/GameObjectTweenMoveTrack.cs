using UnityEngine;
using UnityEngine.Timeline;

//La clave de color del track
[TrackColor(0.2f, 0.5f, 0.4f)]
[TrackBindingType(typeof(GameObject))]

//El typo de TrackClipType que debe tener referenciado
[TrackClipType(typeof(GameObjectTweenMoveClip))]

//Clase Holder que solo especifica que tipo de clips se van a usar.
public class GameObjectTweenMoveTrack : TrackAsset
{

}
