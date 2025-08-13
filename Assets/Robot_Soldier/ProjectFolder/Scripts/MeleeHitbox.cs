using UnityEngine;

public class MeleeHitbox : MonoBehaviour
{
	public float hitRadius;
	public LayerMask hitMask;

	public void EnableHitBox()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, hitRadius, hitMask);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].transform.CompareTag("Player"))
			{
				CameraManager.instance?.ShakeCam();
			}
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, hitRadius);
	}
}
