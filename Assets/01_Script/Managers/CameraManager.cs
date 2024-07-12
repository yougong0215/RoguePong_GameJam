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
            VirtualCamera.Follow = GameManager.Instance.Player.transform;
            GameObject obj = new GameObject("Zero Pos");
            obj.transform.parent = transform;
            obj.transform.position = Vector3.zero;
            VirtualCamera.LookAt = obj.transform;

            m_MultiChannelPerlin = VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            Shake(false);
        }
    }

    public bool Shake(bool Enable, float Intensity = 1.0f)
    {
        if(m_MultiChannelPerlin)
        {
            if(Enable)
            {
                m_MultiChannelPerlin.m_AmplitudeGain = Intensity;
                m_MultiChannelPerlin.m_FrequencyGain = Intensity;
            }
            else
            {
                m_MultiChannelPerlin.m_AmplitudeGain = 0.0f;
                m_MultiChannelPerlin.m_FrequencyGain = 0.0f;
            }
        }
        else
        {
            return false;
        }

        return true;
    }

}
