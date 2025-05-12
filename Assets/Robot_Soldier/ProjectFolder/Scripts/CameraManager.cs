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
    public Transform aimHelper;
    public LayerMask aimLayer;
    private Vector3 aimDirection;
    private const float DEFAULT_AIM_RANGE = 100;
    private const float SHAKE_DEFAULT_TIME = 0.5f;
    private const float DEFAULT_SHAKE_AMP = 0.5f;
    private const float DEFAULT_SHAKE_GAIN = 2;

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
        aimHelper.transform.position = aimDirection;
    }

    public Vector3 Aim()
    {
        if (!Application.isPlaying)
        {
            return Vector3.zero;
        }

        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(ray.origin, ray.direction.normalized * Vector3.Distance(playerCamera.transform.position, transform.position), Color.blue);
        aimDirection = ray.GetPoint(DEFAULT_AIM_RANGE);

        if (Physics.Raycast(ray, out RaycastHit hit, DEFAULT_AIM_RANGE, aimLayer))
        {
            //Debug.Log($"RayHitted: {hit.transform.name}");
            return hit.point;
        }
        else
        {
            return aimDirection;
        }
    }

    public void ShakeCam()
    {
        cameraShakeTweenSequence?.Kill();

        noise.m_AmplitudeGain = DEFAULT_SHAKE_AMP;
        noise.m_FrequencyGain = DEFAULT_SHAKE_GAIN;

        cameraShakeTweenSequence = DOTween.Sequence();
        cameraShakeTweenSequence.Append(DOTween.To(() => noise.m_AmplitudeGain, x => noise.m_AmplitudeGain = x, 0, SHAKE_DEFAULT_TIME));
        cameraShakeTweenSequence.Join(DOTween.To(() => noise.m_FrequencyGain, x => noise.m_FrequencyGain = x, 0, SHAKE_DEFAULT_TIME));
        cameraShakeTweenSequence.Play();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(Aim(), 1);
    }
}
