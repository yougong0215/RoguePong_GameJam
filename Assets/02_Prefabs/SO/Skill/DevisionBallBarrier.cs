using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SO/Skill/DivisionBallBarrier")]
public class DevisionBallBarrier : SkillAbility
{
    [SerializeField] float _durationTime = 5f;
    BallSystem _bss = null;
    public override void SettingAction(ref Action<BallSystem> bss, ObjectSystem hit)
    {
        if (_bss != null && _bss.gameObject.activeSelf == false)
        {
            _bss = null;
        }
        bss += (ab) => 
        {
            if(ab._isReal && _bss == null)
            {
                BallSystem ballsys = PoolManager.Instance.Pop("GameBall") as BallSystem;
                ballsys.transform.position = ab.transform.position;
                _bss = ballsys;
                Vector3 dir = GameManager.Instance.Player.transform.forward;
                Quaternion rotation = Quaternion.Euler(0, UnityEngine.Random.Range(-30f, 30f), 0);

                ballsys._abilityStat = hit._abilityStat;// * (1 / 3);


                dir = rotation * dir;
                ballsys.Input(dir, BallOwner.Player, default, default, default, true, 0.2f);
                ballsys.TimeLimit(_durationTime);
            }

            


        };
    }



}
