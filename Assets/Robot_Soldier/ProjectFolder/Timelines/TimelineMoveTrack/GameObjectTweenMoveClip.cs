using UnityEngine;
using UnityEngine.Playables;

public class GameObjectTweenMoveClip : PlayableAsset
{
	//Las referencias no primitivas deben encapsularse con el tipo de exposed reference.
	public ExposedReference<Transform> destination;

	/// <summary>
	/// Función sobrecargada para crear el playable que es el la lógica que se ejecuta en la duración del clip
	/// </summary>
	/// <param name="graph">la clase que ejecuta los playables del track</param>
	/// <param name="owner">el objeto en el que el timeline de la track está asignado.</param>
	/// <returns></returns>
	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		//Convertir el tipo Expose reference a una variable "default" de Unity.
		var resolvedDestination = destination.Resolve(graph.GetResolver());

		//Crear el playable y asignar sus parámetros (constructor).
		var behaviour = new GameObjectTweenMoveBehaviour
		{
			destination = resolvedDestination,
		};

		//Convertir el behaviour en un playable para que su lógica puede ser ejecutada
		var playable = ScriptPlayable<GameObjectTweenMoveBehaviour>.Create(graph, behaviour);

		return playable;
	}
}
