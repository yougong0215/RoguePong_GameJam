using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : Singleton<CameraManager>
{

    public CinemachineVirtualCamera VirtualCamera;
    private CinemachineBasicMultiChannelPerlin m_MultiChannelPerlin;


    private void Awake()
    {
        VirtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();

        if(VirtualCamera)
        {
            PlayerCameraTarget target = GameObject.FindObjectOfType<PlayerCameraTarget>();

            VirtualCamera.Follow = target.transform;
            GameObject obj = new GameObject("Zero Pos");
            obj.transform.parent = transform;
            obj.transform.position = Vector3.zero;
            //VirtualCamera.LookAt = obj.transform;
            VirtualCamera.LookAt = target.transform;

            m_MultiChannelPerlin = VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            StartCoroutine(ShakeCO(0, 0));
        }
    }

    public void Shake(float Intensity = 1.0f, float duration = 0.2f)
    {
        StartCoroutine(ShakeCO(Intensity, duration));
    }

    IEnumerator ShakeCO(float Intensity = 1.0f, float duration = 0.2f)
    {
        if (m_MultiChannelPerlin)
        {
            m_MultiChannelPerlin.m_AmplitudeGain = Intensity;
            m_MultiChannelPerlin.m_FrequencyGain = Intensity;
            yield return new WaitForSeconds(duration);
            
                m_MultiChannelPerlin.m_AmplitudeGain = 0.0f;
                m_MultiChannelPerlin.m_FrequencyGain = 0.0f;
            
        }

    }

}
