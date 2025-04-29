using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using StarterAssets;
using TMPro;

public class UiManager : MonoBehaviour
{
	public CanvasGroup canvasGroup;
	public CanvasGroup[] uiElements;
	public Button buttonResume;
	public Image aimImage;
	public Sequence tweenUiSequence;
	public PlayerInput playerInput;
	public ThirdPersonController thirdPersonController;
	public Ease easeType;

	[Header("WeponUI")]
	public TextMeshProUGUI textAmmoCount;


	public bool paused
	{
		get
		{
			return _paused;
		}
		set
		{
			_paused = value;
			thirdPersonController?.ToggleInputBlock(value);
		}
	}
	private bool _paused;
	private const float OFFSET = -550;
	private const float TWEEN_TIME = 0.15f;

	Tween aimTween;
	public static UiManager instance;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	void Start()
	{
		canvasGroup.alpha = 0;
		canvasGroup.interactable = false;
		canvasGroup.blocksRaycasts = false;
		aimImage.DOFade(0, 0);
		HideUiElements();
		buttonResume?.onClick.AddListener(() => { DisplayPause(false); });
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			DisplayPause(!paused);
		}
	}

	public void ToggleAim(bool displayAim)
	{
		aimTween?.Kill();
		float endValue = displayAim ? 1 : 0;
		aimTween = aimImage.DOFade(endValue, TWEEN_TIME);
	}

	public void DisplayPause(bool revealed)
	{
		HideUiElements();

		paused = revealed;
		float endValue = revealed ? 1 : 0;
		Time.timeScale = paused ? 0 : 1;
		Cursor.lockState = paused ? CursorLockMode.None : CursorLockMode.Locked;

		canvasGroup.DOFade(endValue, TWEEN_TIME).OnComplete(() =>
		{
			if (revealed)
			{
				TweenUI();
			}
		}).SetUpdate(true);

		canvasGroup.interactable = revealed;
		canvasGroup.blocksRaycasts = revealed;
	}

	private void TweenUI()
	{
		tweenUiSequence = DOTween.Sequence();

		for (int i = 0; i < uiElements.Length; i++)
		{
			uiElements[i].transform.DOLocalMoveX(OFFSET, 0).SetUpdate(true);
			tweenUiSequence.Append(uiElements[i].DOFade(1, TWEEN_TIME / 2).SetEase(easeType).SetUpdate(true));
			tweenUiSequence.Append(uiElements[i].transform.DOLocalMoveX(0, TWEEN_TIME).SetEase(easeType).SetUpdate(true));

		}
		tweenUiSequence.Play().SetUpdate(true);
	}

	private void HideUiElements()
	{
		for (int i = 0; i < uiElements.Length; i++)
		{
			uiElements[i].alpha = 0;
		}
	}

	public void SetAmmoCount(int currentAmmo, int reserveAmmo)
	{
		textAmmoCount.text = $"{currentAmmo}/{reserveAmmo}";
	}
}
