using UnityEngine;
using UnityEngine.Events;

public class EnemyVision : MonoBehaviour
{
	[SerializeField]
	private LayerMask layerMask;

	[SerializeField]
	private Transform staringPoint;

	[SerializeField]
	private float range, visionRadius;

	[SerializeField]
	UnityEvent onSeeEvent;
	private Ray visionRay;

	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		Vision();
	}

	bool Vision()
	{
		visionRay = new Ray(staringPoint.position, staringPoint.forward);
		//Debug.DrawRay(visionRay.origin, visionRay.direction * range, Color.blue);
		RaycastHit hit;
		Physics.SphereCast(visionRay, visionRadius, out hit, range, layerMask);
		
		if(hit.transform.CompareTag("Player"))
		{
			onSeeEvent?.Invoke();
			return true;
		}

		return false;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Ray debugRay = new Ray(staringPoint.position, staringPoint.forward);

		Gizmos.DrawWireSphere(staringPoint.position, visionRadius);
		Gizmos.DrawRay(debugRay.origin, debugRay.direction * range);
		Gizmos.DrawWireSphere(debugRay.GetPoint(range), visionRadius);
	}
}
