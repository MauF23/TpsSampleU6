using UnityEngine;
using StarterAssets;
public class Weapon : MonoBehaviour
{
    [SerializeField]
    private StarterAssetsInputs _input;

    [SerializeField]
    private Transform firePoint;

    [Range(10, 10000)]
    private float weaponRange;

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
            Vector3 direction = cameraManager.Aim() - firePoint.position;
            Ray ray = new Ray(firePoint.position, direction);
            Debug.DrawRay(firePoint.position, direction, Color.red, 4);
        }
    }
}
