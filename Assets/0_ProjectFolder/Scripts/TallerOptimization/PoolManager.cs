using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;
    public GameObjectPool projectilePool, explosionPool;

	private void Awake()
	{
		instance = this;
	}
}
