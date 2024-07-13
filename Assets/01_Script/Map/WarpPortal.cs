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
        Debug.LogError("µî·ÏµÊ");
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
            if (mapData.mapList.Count > GameManager.Instance.currentStage)
            {
                GameManager.Instance.currentStage++;
                print("Next Stage: " + GameManager.Instance.currentStage);
                var map = Instantiate(mapData.mapList[GameManager.Instance.currentStage]);
                map.name = "Map";
                map.transform.position = Vector3.zero;
                Destroy(GameObject.Find("Map"));
                var spw = GameObject.Find("SpawnPoint");
                StartCoroutine(GameManager.Instance.Player.FrameCharacterConoff());
                GameManager.Instance.Player.gameObject.transform.position = spw.transform.position;
                GameManager.Instance.isCleared = false;
                GameManager.Instance.ResetCnt();


                foreach (var item in GameObject.FindObjectsOfType<BallSystem>())
                {
                    item.DieObj();
                }
                BallSystem bs = PoolManager.Instance.Pop("GameBall") as BallSystem;
                bs.transform.position = spw.transform.position + new Vector3(0, 0, 5f);
                bs.Input(bs.transform.forward, BallOwner.Natural);

                gameObject.gameObject.SetActive(false);
            }
            else
            {
                print("Mapdata not exists (out of range)");
            }
        }
    }
}
