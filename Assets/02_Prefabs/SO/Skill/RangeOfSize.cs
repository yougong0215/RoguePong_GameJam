using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "SO/Skill/RangeOfSize")]
public class RangeOfSize : SkillAbility
{
    Coroutine _co = null;
    float t = 1f;
    public override void SettingAction(ref Action<BallSystem> bss, ObjectSystem hit)
    {
        t = 1;
        bss += (ab) =>
        {

            
            if (_co == null)
            {
                _co = ab.StartCoroutine(TimeStop(ab));
            }
        };
    }

    IEnumerator TimeStop(BallSystem ab)
    {
        if(t > 3f)
        {
            t = 3f;
        }
        float h = 3f / t;
        Debug.Log($"AAAA 3f / {t} = {h}");
        yield return new WaitForSeconds(h);
        t++;
        ab.RefreshTime();
        ab._abilityStat._multySize = 1 + ab._abilityStat._multySize * UnityEngine.Random.Range(-0.15f, 0.15f) * t;
        ab._abilityStat._multyATK = 1+ ab._abilityStat._multyATK * UnityEngine.Random.Range(-0.15f, 0.15f) * t;
        _co = null;
    }

}
