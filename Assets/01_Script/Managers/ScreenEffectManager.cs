using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ScreenEffectManager : Singleton<ScreenEffectManager>
{

    public Volume GlobalVolume;

    private ChromaticAberration chromaticAberration;
    
    void Start()
    {
        GlobalVolume = GetComponentInChildren<Volume>();

        if(GlobalVolume.profile.TryGet(out chromaticAberration))
        {
            chromaticAberration.active = true;
            chromaticAberration.intensity = new ClampedFloatParameter(0.0f, chromaticAberration.intensity.min, chromaticAberration.intensity.max);
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
            Debug.LogError("Chromatic Aberration ¾ø¾î¿ë!");
        }
    }

    private IEnumerator ChromaticAberration(float Intensity, float Duration)
    {
        chromaticAberration.intensity = new ClampedFloatParameter(Intensity, chromaticAberration.intensity.min, chromaticAberration.intensity.max);
        yield return new WaitForSeconds(Duration);
        chromaticAberration.intensity = new ClampedFloatParameter(0.0f, chromaticAberration.intensity.min, chromaticAberration.intensity.max);
    }
}
