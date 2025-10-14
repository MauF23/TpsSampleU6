using UnityEngine;
using DG.Tweening;
using NUnit.Framework;
using System.Collections.Generic;

public class InteractableShaderController : MonoBehaviour
{
    public List<Renderer> renderers;
    public string propertyFresnelPower;
    public string propertyFresnelColor;
    public float startValue;
    public float targetValue;
    public LoopType loopType;
    private float tweenTime = 1;
    private float tweenDelay = 2f;
    private List<Tween> hintTweens = new List<Tween>();

    private void Start()
    {
        EndHint();
    }

    public void StartHint()
    {
        ToggleFx(true);

        for (int i = 0; i < renderers.Count; i++)
        {
            if (renderers[i] == null || !renderers[i].material.HasProperty(propertyFresnelPower))
            {
                return;
            }

            hintTweens.Add(renderers[i].material.DOFloat(targetValue, propertyFresnelPower, tweenTime).SetLoops(-1, LoopType.Yoyo));
        }
    }

    public void EndHint()
    {

        if (hintTweens.Count > 0)
        {
            for (int i = 0; i < renderers.Count; i++)
            {
                hintTweens[i]?.Kill(true);
            }

            hintTweens.Clear();
        }

        ToggleFx(false);
    }

    private void ToggleFx(bool toggle)
    {
        Color targetColor = toggle ? Color.white : Color.black;

        for (int i = 0; i < renderers.Count; i++)
        {
            renderers[i].material.SetColor(propertyFresnelColor, targetColor);
            renderers[i].material.SetFloat(propertyFresnelPower, startValue);
        }
    }
}
