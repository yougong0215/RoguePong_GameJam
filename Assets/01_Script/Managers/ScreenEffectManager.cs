using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ScreenEffectManager : Singleton<ScreenEffectManager>
{

    public Volume GlobalVolume;

    private ChromaticAberration chromaticAberration;

    public bool renderFeature;

    void Start()
    {
        GlobalVolume = GetComponentInChildren<Volume>();

        if(GlobalVolume.profile.TryGet(out chromaticAberration))
        {
            chromaticAberration.active = true;
            chromaticAberration.intensity.Override(0.2f);

        }

    }

    public void SetChromaticAberration(float Intensity, float Duration)
    {
        if(chromaticAberration != null)
        {
            StartCoroutine(ChromaticAberration(Intensity, Duration));
        }
        else
        {
            Debug.LogError("Chromatic Aberration �����!");
        }

    }

    private IEnumerator ChromaticAberration(float Intensity, float Duration)
    {
        chromaticAberration.intensity.Override(Intensity);
        yield return new WaitForSeconds(Duration);
        chromaticAberration.intensity.Override(0.0f);
    }

}
