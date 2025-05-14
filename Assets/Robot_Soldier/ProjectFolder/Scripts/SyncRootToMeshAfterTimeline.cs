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
		//Guardar la pocisión y rotación del mesh
		finalRootPosition = meshTransform.position;
		finalRootRotation = meshTransform.rotation;

		//Asignar a este transform (que debe ser el root) las pocisiones previamente guardadas
		transform.position = finalRootPosition;
		transform.rotation = finalRootRotation;

		//Resetear la posición y rotación del mesh a zero, como el root, su objeto padre tiene su posición previa al
		//ser ambas zero el mesh toma como sus pocisiones locales las que tenía previamente.
		meshTransform.localPosition = Vector3.zero;
		meshTransform.localRotation = Quaternion.identity;
	}
}
