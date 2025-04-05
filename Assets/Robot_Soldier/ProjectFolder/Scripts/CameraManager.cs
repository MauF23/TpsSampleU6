using UnityEngine;
using DG.Tweening;
using Cinemachine;
using System.Collections;
using Unity.Collections;

public class CameraManager : MonoBehaviour
{
    public Camera playerCamera;
    public CinemachineVirtualCamera playerVirtualCamera;
    private CinemachineBasicMultiChannelPerlin noise;
    private Coroutine shakeRoutine;
    public Transform aimHelper;
    public LayerMask aimLayer;
    private const float DEFAULT_AIM_RANGE = 100;
    public static CameraManager instance;
    private Sequence cameraShakeTweenSequence;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        noise = playerVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    void Update()
    {
        aimHelper.transform.position = Aim();
    }

    public Vector3 Aim()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, DEFAULT_AIM_RANGE, aimLayer))
        {
            //Debug.Log($"RayHitted: {hit.transform.name}");
            return hit.point;
        }
        else
        {
            return ray.direction * DEFAULT_AIM_RANGE;
        }
    }

    public void ShakeCam()
    {
        if (shakeRoutine != null)
        {
            StopCoroutine(shakeRoutine);
        }

        shakeRoutine = StartCoroutine(ShakeCamCoroutine());
    }

    IEnumerator ShakeCamCoroutine()
    {


        noise.m_AmplitudeGain = 0.5f;
        noise.m_FrequencyGain = 0.25f;

        yield return new WaitForSeconds(0.25f);

        cameraShakeTweenSequence = DOTween.Sequence();
        cameraShakeTweenSequence.Append(DOTween.To(() => noise.m_AmplitudeGain, x => noise.m_AmplitudeGain = x, 0, 0));
        cameraShakeTweenSequence.Append(DOTween.To(() => noise.m_FrequencyGain, x => noise.m_FrequencyGain = x, 0, 0));
        cameraShakeTweenSequence.Play();
    }
}
