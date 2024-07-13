using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private PlayerSystem _player;
    public int currentStage;
    public int currentFloor;
    public bool isCleared = true;

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
            print("COMPLETE");
            foreach(var w in warpPortalList)
            {
                w.SetActive(true);
            }
        }
    }

    private void Start()
    {
        currentStage = 1;
        navMeshSurface = GetComponent<NavMeshSurface>();

        navMeshSurface.BuildNavMesh();

    }
}
