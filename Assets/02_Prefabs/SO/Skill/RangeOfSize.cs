using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "SO/Skill/RangeOfSize")]
public class RangeOfSize : SkillAbility
{
    Coroutine _co;
    int t = 0;
    public override void SettingAction(ref Action<BallSystem> bss, ObjectSystem hit)
    {
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
        yield return new WaitForSeconds(3f / t);
        ab.RefreshTime();
        ab._abilityStat._multySize += UnityEngine.Random.Range(-0.15f, 0.15f) * t;
        _co = null;
    }

}
