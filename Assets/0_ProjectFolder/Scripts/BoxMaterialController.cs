using UnityEngine;
using DG.Tweening;// Importar DOTWeen

public class BoxMaterialController : MonoBehaviour
{
    public Renderer[] renderers;
    public Material originalMaterial;
    private Material runtimeMaterial;

    private Tween fresnelTween, flickerTween;

    public float tweenTime = 0.35f;
    public float flickerTime = 0.2f;

    public int flickerLoops = 4;

    private string fresnelMask = "_FresnelMask";
    private string tint = "_Tint";
    public KeyCode flickerKey = KeyCode.Tab;

	private void Start()
	{
        runtimeMaterial = new Material(originalMaterial);

        for(int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material = runtimeMaterial; 
        }
	}

	private void Update()
    {
        if (Input.GetKeyDown(flickerKey))
        {
            flickerTween?.Kill(true); //matar el tween con su valor final
			flickerTween = runtimeMaterial.DOColor(Color.red, tint, flickerTime).SetLoops(flickerLoops, LoopType.Yoyo);
		}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TweenFresnel(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TweenFresnel(false);
        }
    }

    private void TweenFresnel(bool active)
    {
        fresnelTween?.Kill();
        float targetValue = active ? 1f : 0f;
		fresnelTween = runtimeMaterial.DOFloat(targetValue, fresnelMask, tweenTime);
	}



}
