using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SO/BlessData/MaxHP")]
public class MaxHPBless : BlessData
{
    public override void BlessDoing(float val)
    {
        GameManager.Instance.Player._originStat._addHP += 1;
    }
}
