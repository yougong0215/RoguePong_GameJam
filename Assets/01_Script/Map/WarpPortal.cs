using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WarpPortal : MonoBehaviour
{
    [SerializeField]
    protected MapData mapData;

    public BoxColliderCast colliderCast;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        colliderCast = GetComponent<BoxColliderCast>();
        GameManager.Instance.AssignPortal(gameObject);
        Debug.LogError("��ϵ�");
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

    protected virtual void ChangeMap()
    {
        if(GameManager.Instance.isCleared)
        {
            GameManager.Instance.isInSpeicalRoom = false;
            if (mapData.mapList.Count > GameManager.Instance.CurrentStage)
            {
                GameManager.Instance.CurrentStage++;
                print("Next Stage: " + GameManager.Instance.CurrentStage);
                Destroy(GameObject.Find("Map"));
                var map = Instantiate(mapData.mapList[GameManager.Instance.CurrentStage]);
                map.name = "Map";
                map.transform.position = Vector3.zero;
                GameManager.Instance.isCleared = false;
                GameManager.Instance.ResetCnt();


                foreach (var item in GameObject.FindObjectsOfType<BallSystem>())
                {
                    item.DieObj();
                }

                GameManager.Instance.Chk();

                gameObject.gameObject.SetActive(false);
            }
            else
            {
                print("Mapdata not exists (out of range)");
            }
        }
    }
}
