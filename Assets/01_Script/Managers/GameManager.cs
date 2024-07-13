using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEditor;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private PlayerSystem _player;
    public int currentStage = 1;
    public int currentFloor;
    public bool isCleared;
    public MapData mapData;

    private NavMeshSurface navMeshSurface;

    private int AllCnt;
    private int CurCnt;

    private List<GameObject> warpPortalList = new List<GameObject>();

    public PlayerSystem Player
    {
        get
        {
            if (_player == null)
            {
                _player = GameObject.FindObjectOfType<PlayerSystem>();
                print(_player.name);
            }
            return _player;
        }
    }

    public void ResetCnt()
    {
        AllCnt = 0;
        CurCnt = 0;
        navMeshSurface.BuildNavMesh();
    }

    public void AssignPortal(GameObject o)
    {
        warpPortalList.Add(o);
        o.SetActive(false);
    }

    public void AssignSpawner(int cnt)
    {
        AllCnt += cnt;
    }

    public void AddDeath()
    {
        CurCnt++;
        if(CurCnt >= AllCnt)
        {
            isCleared = true;
            print("COMPLETE");
            foreach(var w in warpPortalList)
            {
                w.SetActive(true);
            }
        }
    }

    private void Start()
    {
        currentStage = 0;
        var map = Instantiate(mapData.mapList[currentStage]);
        map.name = "Map";
        var spw = GameObject.Find("SpawnPoint");
        StartCoroutine(Player.FrameCharacterConoff());
        Player.gameObject.transform.position = spw.transform.position;
        navMeshSurface = GetComponent<NavMeshSurface>();
    }
}
