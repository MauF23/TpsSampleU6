using UnityEngine;
using StarterAssets;
using DG.Tweening;
public class Weapon : MonoBehaviour
{
    [SerializeField]
    private StarterAssetsInputs _input;

    [SerializeField]
    private ParticleSystem particleMuzzleFlash;

    [SerializeField]
    private GameObject impactFXPrefab;

    [SerializeField]
    private Transform firePoint;

    [SerializeField, Range(10, 10000)]
    private float weaponRange;

    public float spreadRadiusBuildUp, spreadMaxRadius, spreadResetTime;
    private float currentSpreadRadius;
    private Tween spreadRecovery;

    [SerializeField]
    private int weaponDamage;

	[SerializeField, Range(0.1f, 5)]
    private float fireRate;
    private float nextTimeToFire = 0;

    [SerializeField]
    private LayerMask layerMask;

    private CameraManager cameraManager;

    void Start()
    {
        if (CameraManager.instance != null)
        {
            cameraManager = CameraManager.instance;
        }
    }

    void Update()
    {
        if (_input.aim && _input.shoot && cameraManager != null)
        {
            if (Time.time >= nextTimeToFire)
            {
                Vector3 direction = cameraManager.Aim() - (firePoint.position + Spread(currentSpreadRadius));
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

                cameraManager?.ShakeCam();
                particleMuzzleFlash?.Play();
                nextTimeToFire = Time.time + fireRate;
                currentSpreadRadius += spreadRadiusBuildUp;
			}

			if(currentSpreadRadius > 0)
            {
                spreadRecovery?.Kill();
				spreadRecovery = DOTween.To(()=>currentSpreadRadius, x => currentSpreadRadius = x, 0, spreadResetTime);
            }

		}
    }

    private Vector3 Spread(float radius)
    {
        float clampRadius = Mathf.Clamp(radius, 0, spreadMaxRadius);
        Vector3 spreadPoint = Random.insideUnitSphere * clampRadius;
        return spreadPoint;
    }

    void DealDamage(Collider collider)
    {
        Hp hp = collider.GetComponent<Hp>();
        hp?.ReduceHp(weaponDamage);
    }
}
