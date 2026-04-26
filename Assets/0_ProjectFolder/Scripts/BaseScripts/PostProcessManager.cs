using UnityEngine;
using UnityEngine.Rendering;//Importar librería de rendering
using UnityEngine.Rendering.Universal;//Importar librería de rendering de URP
using DG.Tweening; //Importar Tweens

public class PostProcessManager : MonoBehaviour
{
    public Volume volume;
    public Vignette vigentte;
    private Tween vignetteTween;
    public float time;
        

    private void Start()
    {
        //out asigna el valor automáticamente si se emcuentra el parámetro.
        volume.profile.TryGet(out vigentte);
        vigentte.intensity.value = 0;
    }

    public void VignetteFx(Color vignetteColor, float intensity, float tweenTime, int loops)
    {
        vigentte.color.value = vignetteColor;
        //vigentte.intensity.value = intensity;

        /*if(vignetteTween != null)
        {
            vignetteTween.Kill();
        }*/

        vignetteTween?.Kill();

        //()=>vigentte.intensity.value significa obtener el valor actual de la intensidad
        //x => vigentte.intensity.value = x singifica que el valor obtenido lo va a igualar al valor que le pasemos como objetivo
        vignetteTween = DOTween.To(() => vigentte.intensity.value, x => vigentte.intensity.value = x, intensity, tweenTime).SetLoops(loops, LoopType.Yoyo);
    }
}
