using UnityEngine;

public class WeaponRaycast : Weapon
{
	const string CATEGORY = "WeaponRaycast";

	[Header(CATEGORY), SerializeField]
	protected GameObject impactFXPrefab;

	[Header(CATEGORY), SerializeField]
	protected LayerMask layerMask;

	protected override void Shoot(Vector3 direction)
	{
		Ray ray = new Ray(firePoint.position, direction);
		Debug.DrawRay(firePoint.position, direction, Color.red, 2);

		if (Physics.Raycast(ray, out RaycastHit hit, weaponRange, layerMask))
		{
			if (hit.collider != null)
			{
				GameObject impactFX = Instantiate(impactFXPrefab, hit.point, Quaternion.Euler(hit.normal));
				DealDamage(hit.collider);
			}
		}
	}
}
