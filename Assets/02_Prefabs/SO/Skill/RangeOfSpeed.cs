using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skill/RangeOfSpeed")]
public class RangeOfSpeed : SkillAbility
{
    Coroutine _co;
    int t = 1;
    public override void SettingAction(ref Action<BallSystem> bss, ObjectSystem hit)
    {
        bss += (ab) =>
        {
            ab._abilityStat._multySpeed += UnityEngine.Random.Range(-0.15f, 0.15f) * t;
            t++;
            if (t > 3)
            {
                t = 3;
            }
            if(_co != null)
            {
                ab.StopCoroutine(_co);
            }
            _co = ab.StartCoroutine(TimeStop());

            Debug.Log($"이걸보면 지우시오 {t}");
            ab.RefreshTime();
        };
    }

    IEnumerator TimeStop()
    {
        yield return new WaitForSeconds(3f);
        t = 1;
    }


}
