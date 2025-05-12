using UnityEngine;
using UnityEngine.Playables;
using DG.Tweening;


/// <summary>
/// Clase que define el comportamiento que se ejecutará en el track
/// </summary>
public class GameObjectTweenMoveBehaviour : PlayableBehaviour
{
	public GameObject target;
	public Transform destination;
	public float tweenDuration;
	private Tween movementTween;

	/// <summary>
	/// Clase similar al Start pero para Playables, lógica que se ejecuta cuando este se ejecuta por primera vez
	/// </summary>
	public override void OnBehaviourPlay(Playable playable, FrameData info)
	{
		movementTween = null;
		tweenDuration = (float)playable.GetDuration();
	}


	/// <summary>
	/// Clase similar al Update pero para Playables, se ejecuta cada frame mientras esté siendo llamado en el proceso del timeline.
	/// </summary>
	/// <param name="playerData">el objeto que tiene referenciado el track del timeline</param>
	public override void ProcessFrame(Playable playable, FrameData info, object playerData)
	{
		target = playerData as GameObject;

		if(movementTween != null)
		{
			return;
		}

		movementTween = target.transform.DOMove(destination.position, tweenDuration);
	}

}
