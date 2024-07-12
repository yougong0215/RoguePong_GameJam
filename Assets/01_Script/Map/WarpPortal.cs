using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpPortal : MonoBehaviour
{
    [SerializeField]
    MapData mapData;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        print("ASDF");
        if(other.gameObject == GameManager.Instance.Player && GameManager.Instance.isCleared)
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
