using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField]
	private Rigidbody rigidBody;

	[SerializeField]
	private GameObject explosionPrefab;

	private int damage = 100;
	private float damageRadius = 7;

	private GameObjectPool explosionPool, decalPool;

	private void Start()
	{
		explosionPool = PoolManager.instance.explosionPool;
		decalPool = PoolManager.instance.decalPool;
    }

	private void OnCollisionEnter(Collision collision)
	{
		Vector3 hitPoint = collision.GetContact(0).point; // obtener el primer contacto y su pocisión (point)
		Vector3 normalPoint = collision.GetContact(0).normal;

		SpawnDecal(hitPoint, normalPoint);

		GameObject explosion = explosionPool.GetGameObjectFromPool(hitPoint);
		SplashDamage(hitPoint);
		gameObject.SetActive(false);
	}

	private void SpawnDecal(Vector3 position, Vector3 normal)
	{
		GameObject decal = decalPool.GetGameObjectFromPool(position);
		Quaternion decalRotation = Quaternion.LookRotation(-normal); //girar el decal a la dirección de la normal de la cara del mesh que haya golpeado.
		decal.transform.rotation = decalRotation;
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
