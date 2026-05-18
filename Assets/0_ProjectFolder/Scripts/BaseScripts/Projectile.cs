using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField]
	private Rigidbody rigidBody;

	[SerializeField]
	private GameObject explosionPrefab;

	private int damage = 100;
	private float damageRadius = 7;

	private GameObjectPool explosionPool;

	private void Start()
	{
		explosionPool = PoolManager.instance.explosionPool;
	}

	private void OnCollisionEnter(Collision collision)
	{
		Vector3 hitPoint = collision.GetContact(0).point;

		GameObject explosion = explosionPool.GetGameObjectFromPool(hitPoint);
		SplashDamage(hitPoint);
		gameObject.SetActive(false);
	}

	private void SplashDamage(Vector3 position)
	{
		Collider[] colliders = Physics.OverlapSphere(position, damageRadius);
		for(int i = 0; i < colliders.Length; i++)
		{
			EnemyHp hp = colliders[i].GetComponent<EnemyHp>();
			hp?.ReduceHp(damage);
		}
	}

	public void LaunchProjectile(Vector3 direction)
	{
		rigidBody.linearVelocity = Vector3.zero;
		rigidBody.AddForce(direction);
	}
}
