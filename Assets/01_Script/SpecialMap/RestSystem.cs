using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestSystem : SpecialMap
{
    public RestData restData;


    public override void Init()
    {
        float value = RandomRecoveryRate();

        float amount = GameManager.Instance.Player.GetHPValue() * value;
        GameManager.Instance.Player.HitEvent(-amount);
    }

    public float RandomRecoveryRate()
    {
        int randomIdx = Random.Range(0, 100);
        int result = 0;

        List<RestAsset> items = restData.restAssets;
        items.Sort((a,b)=>a.Probability.CompareTo(b.Probability));

        for(int i = 0; i < items.Count; i++)
        {
            print(items[i].Probability);
            if(randomIdx <= items[i].Probability)
            {
                result = i;
                break;
            }
        }

        return items[result].recoveryRatio;
    }
}
