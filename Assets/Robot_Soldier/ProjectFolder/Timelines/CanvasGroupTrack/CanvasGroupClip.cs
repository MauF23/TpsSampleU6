using UnityEngine;
using UnityEngine.Playables;

public class CanvasGroupClip : PlayableAsset
{
	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		var behaviour = new CanvasGroupBehaviour();
		var playable = ScriptPlayable<CanvasGroupBehaviour>.Create(graph, behaviour);

		return playable;
	}
}
