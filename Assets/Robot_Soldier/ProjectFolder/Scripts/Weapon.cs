using UnityEngine;
using StarterAssets;
public class Weapon : MonoBehaviour
{
    [SerializeField]
    private StarterAssetsInputs _input;

    [SerializeField]
    private Transform firePoint;

    [SerializeField, Range(10, 10000)]
    private float weaponRange;

    [SerializeField, Range(0.1f, 5)]
    private float fireRate;
    private float nextTimeToFire = 0;

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

                nextTimeToFire = Time.time + fireRate;
            }
        }
    }
}
