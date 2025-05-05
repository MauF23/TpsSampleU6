using UnityEngine;
using StarterAssets;
using DG.Tweening;
using TMPro;
using UnityEngine.Animations.Rigging;
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

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Rig rigIdle, aimIdle;

    [SerializeField]
    private SoundManager soundManagerWeapon;

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

    public int currentAmmo;
    public int maxClipCapacity;
    public int currentReserveAmmo;
    public int maxAmmoCapacity;
    public bool reloading { get; private set; }

    [SerializeField]
    private LayerMask layerMask;

    private CameraManager cameraManager;
    private UiManager uiManager;
    private const string ANIM_RELOAD_TRIGGER = "Reload";

    private void Start()
    {
        if (CameraManager.instance != null)
        {
            cameraManager = CameraManager.instance;
        }

        if (UiManager.instance != null)
        {
            uiManager = UiManager.instance;
        }

        uiManager?.SetAmmoCount(currentAmmo, currentReserveAmmo);

    }

    private void Update()
    {
        if (reloading)
        {
            return;
        }

        if (_input.aim && _input.shoot && cameraManager != null)
        {

            if (currentAmmo <= 0)
            {
                return;
            }

            if (Time.time >= nextTimeToFire)
            {

                currentAmmo--;
                currentAmmo = Mathf.Clamp(currentAmmo, 0, maxAmmoCapacity);
                uiManager.SetAmmoCount(currentAmmo, currentReserveAmmo);

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

                soundManagerWeapon.PlaySoundRandomPitch(SoundEnums.Shoot, 0.75f, 1.30f);

				particleMuzzleFlash?.Play();
                nextTimeToFire = Time.time + fireRate;
                currentSpreadRadius += spreadRadiusBuildUp;
            }

            if (currentSpreadRadius > 0)
            {
                spreadRecovery?.Kill();
                spreadRecovery = DOTween.To(() => currentSpreadRadius, x => currentSpreadRadius = x, 0, spreadResetTime);
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && !reloading)
        {
            ReloadAnim();
        }
    }

    private Vector3 Spread(float radius)
    {
        float clampRadius = Mathf.Clamp(radius, 0, spreadMaxRadius);
        Vector3 spreadPoint = Random.insideUnitSphere * clampRadius;
        return spreadPoint;
    }

    private void DealDamage(Collider collider)
    {
        Hp hp = collider.GetComponent<Hp>();
        hp?.ReduceHp(weaponDamage);
    }

    public void ReloadAnim()
    {
        reloading = true;
        Debug.Log($"ReloadStart");
        ToggleRigs(false);
        animator?.SetTrigger(ANIM_RELOAD_TRIGGER);
	}

    public void Reload()
    {
        ToggleRigs(true);

        if (currentReserveAmmo <= 0 || currentAmmo >= maxAmmoCapacity)
        {
            return;
        }

        int ammoToReload = maxClipCapacity - currentAmmo;

        currentAmmo += ammoToReload;
        currentReserveAmmo -= ammoToReload;

        reloading = false;

        uiManager?.SetAmmoCount(currentAmmo, currentReserveAmmo);

        Debug.Log($"ReloadEnd");
    }

    public void AddReserveAmmo(int amount)
    {
        currentReserveAmmo += amount;
        currentReserveAmmo = Mathf.Clamp(currentReserveAmmo, 0, maxAmmoCapacity);
        uiManager?.SetAmmoCount(currentAmmo, currentReserveAmmo);
    }

    private void ToggleRigs(bool toggle)
    {
        float endValue = toggle ? 1 : 0;
        rigIdle.weight = endValue;
        aimIdle.weight = endValue;
    }
}
