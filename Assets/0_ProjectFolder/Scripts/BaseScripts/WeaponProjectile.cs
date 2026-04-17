using UnityEngine;

public class WeaponProjectile : Weapon
{
	const string CATEGORY = "WeaponProjectile";

	[Header(CATEGORY), SerializeField]
	private GameObject projectilePrefab;

	[SerializeField]
	private float shootForce;

	protected override void Shoot(Vector3 direction)
	{
		if(projectilePrefab == null)
		{
			return;
		}

		GameObject projectileClone = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
		Projectile projectile = projectileClone.GetComponent<Projectile>();
		projectile?.LaunchProjectile(direction.normalized * shootForce);
	}
}
