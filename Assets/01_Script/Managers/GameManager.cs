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

    private int _gold = 0;
    public int Gold
    {
        get => _gold;
        set
        {
            _gold = value;
            HUDCanvas.UpdateGoldText(Gold);
        }
    }

    private HUD _hud;
    public HUD HUDCanvas
    {
        get
        {
            if(_hud == null)
            {
                _hud = GameObject.FindObjectOfType<HUD>();
            }

            return _hud;
        }
    }
    private NavMeshSurface navMeshSurface;

    private int AllCnt;
    private int CurCnt;

    private GameObject warpPortal;

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
        warpPortal = null;
        navMeshSurface.BuildNavMesh();
    }

    public void AssignPortal(GameObject o)
    {
        warpPortal = o;
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
            warpPortal.SetActive(true);
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

        navMeshSurface.BuildNavMesh();
    }
}
