using UnityEngine;
using UnityEngine.Playables;

public class GameObjectTweenMoveClip : PlayableAsset
{
	//Las referencias no primitivas deben encapsularse con el tipo de exposed reference.
	public ExposedReference<Transform> destination;

	/// <summary>
	/// Funci�n sobrecargada para crear el playable que es el la l�gica que se ejecuta en la duraci�n del clip
	/// </summary>
	/// <param name="graph">la clase que ejecuta los playables del track</param>
	/// <param name="owner">el objeto en el que el timeline de la track est� asignado.</param>
	/// <returns></returns>
	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		//Convertir el tipo Expose reference a una variable "default" de Unity.
		var resolvedDestination = destination.Resolve(graph.GetResolver());

		//Crear el playable y asignar sus par�metros (constructor).
		var behaviour = new GameObjectTweenMoveBehaviour
		{
			destination = resolvedDestination,
		};

		//Convertir el behaviour en un playable para que su l�gica puede ser ejecutada
		var playable = ScriptPlayable<GameObjectTweenMoveBehaviour>.Create(graph, behaviour);

		return playable;
	}
}
