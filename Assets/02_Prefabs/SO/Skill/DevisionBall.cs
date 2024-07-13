using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SO/Skill/DivisionBall")]
public class DevisionBall : SkillAbility
{
    [SerializeField] float _durationTime = 5f;
    public override void SettingAction(ref Action<BallSystem> bss, ObjectSystem hit)
    {
        
        bss += (ab) => 
        {
            if(ab._isReal)
            {
                BallSystem ballsys = PoolManager.Instance.Pop("GameBall") as BallSystem;
                ballsys.transform.position = ab.transform.position;
                Vector3 dir = GameManager.Instance.Player.transform.forward;
                Quaternion rotation = Quaternion.Euler(0, UnityEngine.Random.Range(-30f, 30f), 0);

                ballsys._abilityStat = hit._abilityStat;
                dir = rotation * dir;
                ballsys.Input(dir, BallOwner.Player, default, default, default, true, 0.2f);
                ballsys.TimeLimit(_durationTime);
            }

            


        };
    }



}
