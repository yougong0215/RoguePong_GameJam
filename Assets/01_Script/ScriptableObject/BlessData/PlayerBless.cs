using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SO/BlessData/Player")]
public class PlayerBless : BlessData
{
    public Ability _ability;
    public override void BlessDoing(float val)
    {
        GameManager.Instance.Player.AddAbility(AbilityEnums.PlayerStat, _ability);
    }
}
