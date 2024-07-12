using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillAbility : Ability
{
    public override void GetAbility(ref ObjectSystem ballStat)
    {
        //throw new System.NotImplementedException();
    }

    public abstract void SettingAction(ref Action<BallSystem> bss);
}
