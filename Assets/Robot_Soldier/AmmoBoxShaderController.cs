using UnityEngine;
using DG.Tweening;

public class AmmoBoxShaderController : MonoBehaviour
{
    // el renderer  que tiene el material a modificar.
    public Renderer renderer;

    // el color de frensel a cambiar
    [ColorUsage(true, true)] //Anotación para visualizar la barra de transparencia y de intensidad del color
    public Color fresnelColor;

    public float tweenTime;

    //el nombre de la porpiedad a modificar, por regla general simpre empiezan con "_" (_Color, _BaseColor, etc)
    private string fresnelColorProperty = "_FresnelColor";

    void Start()
    {

        //material = es la instrance que se crea en el runtime.
        //sahredMaterial = es el material en sí que comparten todos.
        renderer.material.DOColor(fresnelColor, fresnelColorProperty, tweenTime).SetLoops(-1, LoopType.Yoyo);
    }

}
