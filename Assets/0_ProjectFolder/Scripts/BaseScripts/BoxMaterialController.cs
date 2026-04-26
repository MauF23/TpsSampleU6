using UnityEngine;
using DG.Tweening;
using System.Xml.Serialization;// Importar DOTWeen

public class BoxMaterialController : MonoBehaviour
{
    public Renderer[] renderers; //renderers a los que se les aplicará el material.
    public Material originalMaterial; //el material original que se clonará.
    private Material runtimeMaterial; //clon del material original que se le aplicará a todos los renderers.

    private Tween fresnelTween, flickerTween;

    public float tweenTime = 0.35f;
    public float flickerTime = 0.2f;

    public int flickerLoops = 4;

    private string fresnelMask = "_FresnelMask";
    private string tint = "_Tint";
    public KeyCode flickerKey = KeyCode.Tab;

	private void Start()
	{
        SetRuntimeMaterial();
	}

    /// <summary>
    /// En lugar de ciclar los renderers por tweens clonamos el material original y se lo aplicamos a todos los renderers solo una vez
    /// Así solamente referenciamos el material clon en los tweens y se le aplica a todos los renderers del objeto.
    /// </summary>
    private void SetRuntimeMaterial()
    {
		runtimeMaterial = new Material(originalMaterial);

		for (int i = 0; i < renderers.Length; i++)
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
