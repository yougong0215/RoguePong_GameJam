using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/BlessData/Lacket")]
public class LacketBless : BlessData
{
    public Ability _ability;
    public override void BlessDoing(float val)
    {
        GameManager.Instance.Player.AddAbility(BallSkillEnum.LacketStat, _ability);
    }
}
