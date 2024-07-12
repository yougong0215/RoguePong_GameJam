using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpPortal : MonoBehaviour
{
    [SerializeField]
    MapData mapData;

    private BoxColliderCast colliderCast;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.gameObject.SetActive(false);
        colliderCast = GetComponent<BoxColliderCast>();
    }

    // Update is called once per frame
    void Update()
    {
        if(colliderCast.ReturnColliders().Length > 0)
        {
            if (colliderCast.ReturnColliders()[0].gameObject == GameManager.Instance.Player.gameObject)
            {
                ChangeMap();
            }
        }
    }

    private void ChangeMap()
    {
        print("ASDF");
        if(GameManager.Instance.isCleared)
        {
            var stage = GameManager.Instance.currentStage;
            if (mapData.mapList.Count > stage)
            {
                print("Next Stage: " + stage);
                var map = Instantiate(mapData.mapList[stage - 1]);
                map.name = "Map";
                Destroy(GameObject.Find("Map"));
            }
            else
            {
                print("Mapdata not exists (out of range)");
            }
        }
    }
}
