using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SO/BlessData/HP")]
public class HPBless : BlessData
{
    public override void BlessDoing(float val)
    {
        GameManager.Instance.Player.HitEvent(-val);
    }
}
