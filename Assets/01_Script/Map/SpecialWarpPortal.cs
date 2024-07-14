using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpecialWarpPortal : WarpPortal
{
    protected override void Start()
    {
        colliderCast = GetComponent<BoxColliderCast>();
        GameManager.Instance.AssignSpeicalPortal(gameObject);
        Debug.LogError("½ºÆä¼È Æ÷Å» µî·ÏµÊ");
    }
    protected override void ChangeMap()
    {
        if (GameManager.Instance.isCleared)
        {
            Destroy(GameObject.Find("Map"));
            var map = Instantiate(mapData.SpecialMapList[Random.Range(0, mapData.SpecialMapList.Count)]);
            print("½ºÆä¼È Æ÷Å» ÀÔÀå: " + map.name);
            var spw = GameObject.Find(map.name + "/SpawnPoint");
            StartCoroutine(GameManager.Instance.Player.FrameCharacterConoff());
            GameManager.Instance.Player.gameObject.transform.position = spw.transform.position;
            GameManager.Instance.isCleared = false;
            GameManager.Instance.ResetCnt();
            GameManager.Instance.isInSpeicalRoom = true;
            map.name = "Map";
        }
    }

}
