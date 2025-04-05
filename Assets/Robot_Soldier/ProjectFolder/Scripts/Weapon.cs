using UnityEngine;
using StarterAssets;
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
        if (_input.shoot && cameraManager != null)
        {
            if (Time.time >= nextTimeToFire)
            {
                Vector3 direction = cameraManager.Aim() - firePoint.position;
                Ray ray = new Ray(firePoint.position, direction);
                Debug.DrawRay(firePoint.position, direction, Color.red);

                if (Physics.Raycast(ray, out RaycastHit hit, weaponRange, layerMask))
                {
                    if (hit.collider != null)
                    {
                        GameObject impactFX = Instantiate(impactFXPrefab, hit.point, Quaternion.identity);
                    }
                }

                cameraManager?.ShakeCam();
                particleMuzzleFlash?.Play();
                nextTimeToFire = Time.time + fireRate;
            }
        }
    }
}
