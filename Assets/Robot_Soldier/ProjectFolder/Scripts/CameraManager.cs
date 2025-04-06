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
    private const float DEFAULT_AIM_RANGE = 100;
    private const float SHAKE_DEFAULT_TIME = 0.5f;

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
        cameraShakeTweenSequence?.Kill();

		noise.m_AmplitudeGain = 1;
		noise.m_FrequencyGain = 2;

		cameraShakeTweenSequence = DOTween.Sequence();
		cameraShakeTweenSequence.Append(DOTween.To(() => noise.m_AmplitudeGain, x => noise.m_AmplitudeGain = x, 0, SHAKE_DEFAULT_TIME));
		cameraShakeTweenSequence.Join(DOTween.To(() => noise.m_FrequencyGain, x => noise.m_FrequencyGain = x, 0, SHAKE_DEFAULT_TIME));
		cameraShakeTweenSequence.Play();
	}
}
