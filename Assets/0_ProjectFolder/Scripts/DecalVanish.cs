using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.Universal;// importar para referenciar el proyector

public class DecalVanish : MonoBehaviour
{
    public DecalProjector decalProjector;
    private Tween vanishTween;

    private float lifetime = 3;
    private float vanishTime = 0.25f;

    private void OnEnable()
    {
        decalProjector.fadeFactor = 1.0f; //fadeFactor es el parámetro opacity en el inspector.
        vanishTween?.Kill();

        vanishTween = DOTween.To(()=> decalProjector.fadeFactor, x => decalProjector.fadeFactor = x, 0, vanishTime)
            .SetDelay(lifetime)
            .OnComplete(()=>gameObject.SetActive(false));
    }

    //()=> decalProjector.fadeFactor
    private float RetrunFadeFactor()
    {
        return decalProjector.fadeFactor;
    }

    //newFadeValue => decalProjector.fadeFactor = x
    private float SetFadeFactor(float newFadeValue)
    {
        decalProjector.fadeFactor = newFadeValue;
        return newFadeValue;
    }
}
