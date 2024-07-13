using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialWarpPortal : WarpPortal
{

    protected override void ChangeMap()
    {
        print("ASDF");
        if (GameManager.Instance.isCleared)
        {
            var stage = GameManager.Instance.currentStage;
            if (mapData.SpecialMapList.Count > stage)
            {
                print("Next Stage: " + stage);
                var map = Instantiate(mapData.SpecialMapList[stage - 1]);
                map.name = "Map";
                Destroy(GameObject.Find("Map"));
                gameObject.gameObject.SetActive(false);
                GameManager.Instance.isCleared = false;
            }
            else
            {
                print("Mapdata not exists (out of range)");
            }
        }
    }

}
