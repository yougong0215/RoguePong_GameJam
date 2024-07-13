using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEditor;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private PlayerSystem _player;
    private int _currentStage = 1;
    public int CurrentStage
    {
        get => _currentStage;
        set
        {
            _currentStage = value;
            HUDCanvas.UpdateCurrentStageText(_currentStage + 1);
        }
    }

    public int currentFloor;
    public bool isCleared;
    public MapData mapData;

    private float currentTime = 0.0f;

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

    private int allCnt;
    public int AllCnt
    {
        get=> allCnt;
        set
        {
            allCnt = value;
            HUDCanvas.UpdateLastEnemyText(AllCnt - curCnt);
        }
    }
    private int curCnt;

    public int CurCnt
    {
        get => curCnt;
        set
        {
            curCnt = value;
            HUDCanvas.UpdateLastEnemyText(AllCnt - curCnt);
        }
    }


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
        CurrentStage = 0;
        var map = Instantiate(mapData.mapList[CurrentStage]);
        map.name = "Map";
        var spw = GameObject.Find("SpawnPoint");
        StartCoroutine(Player.FrameCharacterConoff());
        Player.gameObject.transform.position = spw.transform.position;
        navMeshSurface = GetComponent<NavMeshSurface>();

        navMeshSurface.BuildNavMesh();
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        HUDCanvas.UpdateCurrentTimeText(currentTime);
    }
}
