using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SO/Skill/DivisionBall")]
public class DevisionBall : SkillAbility
{
    [SerializeField] float _durationTime = 5f;
    public override void SettingAction(ref Action<BallSystem> bss)
    {
        bss += (ab) => 
        {
            BallSystem ballsys = PoolManager.Instance.Pop("GameBall") as BallSystem;
            ballsys.transform.position = ab.transform.position;
            Vector3 dir = ab.Dir;
            
            Quaternion rotation = Quaternion.Euler(0, UnityEngine.Random.Range(-30f,30f), 0);

            dir = rotation * dir;
            ballsys.Input(dir, default, default, 0.3f);
            ballsys.TimeLimit(_durationTime);

        };
    }

}
