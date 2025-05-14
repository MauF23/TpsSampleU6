using UnityEngine;
using UnityEngine.Playables;

public class SyncRootToMeshAfterTimeline : MonoBehaviour
{
	public PlayableDirector timeline;
	public Transform meshTransform;

	private Vector3 finalRootPosition;
	private Quaternion finalRootRotation;

	public void SyncPlayerRootToMesh()
	{
		//Guardar la pocisi�n y rotaci�n del mesh
		finalRootPosition = meshTransform.position;
		finalRootRotation = meshTransform.rotation;

		//Asignar a este transform (que debe ser el root) las pocisiones previamente guardadas
		transform.position = finalRootPosition;
		transform.rotation = finalRootRotation;

		//Resetear la posici�n y rotaci�n del mesh a zero, como el root, su objeto padre tiene su posici�n previa al
		//ser ambas zero el mesh toma como sus pocisiones locales las que ten�a previamente.
		meshTransform.localPosition = Vector3.zero;
		meshTransform.localRotation = Quaternion.identity;
	}
}
