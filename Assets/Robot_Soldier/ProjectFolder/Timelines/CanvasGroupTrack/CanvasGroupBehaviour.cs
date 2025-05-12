using UnityEngine;
using UnityEngine.Playables;

public class CanvasGroupBehaviour : PlayableBehaviour
{
	private CanvasGroup canvasGroup;

	public override void ProcessFrame(Playable playable, FrameData info, object playerData)
	{
		canvasGroup = playerData as CanvasGroup;
		canvasGroup.alpha = info.weight;
	}
}
