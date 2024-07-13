using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SO/BlessData/GOld")]
public class GoldBless : BlessData
{
    public override void BlessDoing(float val)
    {
        GameManager.Instance.Gold += (int)val;
    }
}
