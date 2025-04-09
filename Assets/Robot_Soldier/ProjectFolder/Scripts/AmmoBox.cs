using UnityEngine;
using DG.Tweening;

public class AmmoBox : MonoBehaviour
{
    public Transform boxLid;
    public Vector3 tweenDegrees;
    public float tweenTime;
    public Tween tweenOpenBox;

    void Start()
    {

    }


    void OpenBox()
    {
        tweenOpenBox?.Kill();
        tweenOpenBox = boxLid.DORotate(tweenDegrees, tweenTime);
    }
}
