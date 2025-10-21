using UnityEngine;
using DG.Tweening;// Importar DOTWeen

public class BoxMaterialController : MonoBehaviour
{
    public Renderer[] renderes;
    private Tween fresnelTween, flickerTween;

    public float tweenTime = 0.35f;
    public float flickerTime = 0.2f;

    public int flickerLoops = 4;

    private string fresnelMask = "_FresnelMask";
    private string tint = "_Tint";
    public KeyCode flickerKey = KeyCode.Tab;

    private void Update()
    {
        if (Input.GetKeyDown(flickerKey))
        {
            flickerTween?.Kill(true); //matar el tween con su valor final
            for(int i = 0; i < renderes.Length; i++)
            {
                //Setear tween de flicker con loops en modo yoyo
                flickerTween = renderes[i].material.DOColor(Color.red, tint, flickerTime).SetLoops(flickerLoops, LoopType.Yoyo);
            }
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

        /*float targetValue = 0;
        if (active)
        {
            targetValue = 1f;
        }
        else
        {
            targetValue = 0f;
        }*/

        float targetValue = active ? 1f : 0f;

        //Aplicar el tween a todos los renderers en el arreglo
        for (int i = 0; i < renderes.Length; i++)
        {
            fresnelTween = renderes[i].material.DOFloat(targetValue, fresnelMask, tweenTime);
        }

    }



}
