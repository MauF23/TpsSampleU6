using UnityEngine;

public class WeaponProjectile : Weapon
{
	const string CATEGORY = "WeaponProjectile";

	[Header(CATEGORY)]

	[SerializeField]
	private float shootForce;

	private GameObjectPool projectilePool;

	protected override void Start()
	{
		base.Start();
		projectilePool = PoolManager.instance.projectilePool;
	}

	protected override void Shoot(Vector3 direction)
	{
		if(projectilePool == null)
		{
			return;
		}

		GameObject projectileClone = projectilePool.GetGameObjectFromPool(firePoint.position);
		Projectile projectile = projectileClone.GetComponent<Projectile>();
		projectile?.LaunchProjectile(direction.normalized * shootForce);
	}
}
