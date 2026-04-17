using DG.Tweening;
using StarterAssets;
using Unity.AppUI.UI;
using UnityEngine;
//using TMPro;
//using UnityEngine.Animations.Rigging;
public class Weapon : MonoBehaviour
{
	[SerializeField]
	protected StarterAssetsInputs _input;

	[SerializeField]
	protected ParticleSystem particleMuzzleFlash;

	[SerializeField]
	protected Transform firePoint;

	[SerializeField]
	protected Animator animator;

	[SerializeField]
	protected bool infiniteAmmo;

	[SerializeField, Range(10, 10000)]
	protected float weaponRange;

	public float spreadRadiusBuildUp, spreadMaxRadius, spreadResetTime;
	protected float currentSpreadRadius;
	protected Tween spreadRecovery;

	[SerializeField]
	protected int weaponDamage;

	[SerializeField, Range(0.1f, 5)]
	protected float fireRate;
	protected float nextTimeToFire = 0;

	public int currentAmmo;
	public int maxClipCapacity;
	public int currentReserveAmmo;
	public int maxAmmoCapacity;
	public bool reloading { get; protected set; }

	[SerializeField]
	protected SoundManager soundManager;

	protected CameraManager cameraManager;
	protected UiManager uiManager;
	protected const string ANIM_RELOAD_TRIGGER = "Reload";

	protected void Start()
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

	protected void Update()
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

				soundManager?.CloneAudioSource("Rifle");

				currentAmmo--;
				currentAmmo = Mathf.Clamp(currentAmmo, 0, maxAmmoCapacity);
				uiManager.SetAmmoCount(currentAmmo, currentReserveAmmo);

				Vector3 direction = cameraManager.Aim() - (firePoint.position + Spread(currentSpreadRadius));
				Debug.DrawRay(firePoint.transform.position, direction, Color.red, 2);
				Shoot(direction);

				cameraManager?.ShakeCam();
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

	protected virtual void Shoot(Vector3 direction)
	{

	}

	protected Vector3 Spread(float radius)
	{
		float clampRadius = Mathf.Clamp(radius, 0, spreadMaxRadius);
		Vector3 spreadPoint = Random.insideUnitSphere * clampRadius;
		return spreadPoint;
	}

	protected void DealDamage(Collider collider)
	{
		Hp hp = collider.GetComponent<Hp>();
		hp?.ReduceHp(weaponDamage);
	}

	public void ReloadAnim()
	{
		if (!HasAmmo())
		{
			return;
		}

		reloading = true;
		Debug.Log($"ReloadStart");
		//ToggleRigs(false);
		animator?.SetTrigger(ANIM_RELOAD_TRIGGER);
	}

	public void Reload()
	{
		//ToggleRigs(true);

		if (!HasAmmo())
		{
			return;
		}


		int ammoToReload = 0;
		int ammoToFillClip = maxClipCapacity - currentAmmo;

		if (currentReserveAmmo >= ammoToFillClip)
		{
			ammoToReload = ammoToFillClip;
		}
		else
		{
			ammoToReload = currentReserveAmmo;
		}

		currentAmmo += ammoToReload;
		currentReserveAmmo -= ammoToReload;


		if (currentReserveAmmo <= 0)
		{
			currentReserveAmmo = 0;
		}

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

	protected bool HasAmmo()
	{
		if (infiniteAmmo && currentReserveAmmo <= 0)
		{
			currentReserveAmmo = maxClipCapacity;
		}

		return currentReserveAmmo > 0;

	}
}
