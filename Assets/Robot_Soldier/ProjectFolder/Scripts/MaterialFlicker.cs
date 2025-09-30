using UnityEngine;
using DG.Tweening;

public class MaterialFlicker : MonoBehaviour
{
    public Material material;
    public Color defaultColor, targetColor;
    public string colorProperty;
	public int loops;
	public float flickerTime;
	private Tween flickerTween;

	private void Start()
	{
		material.SetColor(colorProperty, defaultColor);
	}

	public void Flicker()
	{
		flickerTween?.Kill();
		flickerTween = material.DOColor(targetColor, colorProperty, flickerTime).SetLoops(loops, LoopType.Yoyo).SetEase(Ease.Linear);
	}
}
