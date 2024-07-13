using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlessSystem : SpecialMap
{
    public BlessList blessData;


    public override void Init()
    {
        int randomIdx = Random.Range(0, 100);
        int result = 0;

        List<BlessData> list = null;
        switch(Random.Range(0, 2))
        {
            case 0:
                list = blessData.GoldBlessList;
                break;
            case 1:
                list = blessData.PlayerBuffList;
                break;
            case 2:
                list = blessData.HPBlessList;
                break;
        }
        list.Sort((a, b) => a.Probability.CompareTo(b.Probability));

        for (int i = 0; i < list.Count; i++)
        {
            if (randomIdx <= list[i].Probability)
            {
                result = i;
                break;
            }
        }



        Debug.LogError(list[result].name);
        Debug.LogError(list[result].Value);
        list[result].BlessDoing(list[result].Value);

        GameManager.Instance.Player.RefreshStat();
    }
}
