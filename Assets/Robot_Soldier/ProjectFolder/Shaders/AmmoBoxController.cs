using UnityEngine;

public class AmmoBocController : MonoBehaviour
{
    public Renderer boxRenderer;

    [ColorUsage(true, true)]
    public Color fresnelColor;
    public string fresnelColorReference = "_FresnelColor";
    public bool effectOn;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            effectOn = !effectOn;

            if(effectOn == true)
            {
                boxRenderer.material.SetColor(fresnelColorReference, fresnelColor);
            }
            else
            {
                boxRenderer.material.SetColor(fresnelColorReference, Color.black);
            }
        }
    }
}
