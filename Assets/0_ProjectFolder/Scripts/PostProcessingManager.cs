using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingManager : MonoBehaviour
{
	public Volume postProcessVolume;
	private Vignette vignette;
	private Tween vignetteTween;

	private void Start()
	{
		postProcessVolume = GetComponent<Volume>();
		postProcessVolume.profile.TryGet(out vignette);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
			VignetteFx(Color.red, 1, 0.2f, 4);
		}
	}

	public void VignetteFx(Color color, float value, float tweenTime, int loops)
	{
		if(vignette == null)
		{
			return;
		}

		vignette.color.value = color;

		vignetteTween.Kill();
		vignetteTween = DOTween.To(()=>vignette.intensity.value, x => vignette.intensity.value = x, value, tweenTime).SetLoops(loops, LoopType.Yoyo);
	}
}
