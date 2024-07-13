using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private PlayerSystem _player;
    public int currentStage;
    public int currentFloor;
    public bool isCleared = true;

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

    private void Start()
    {
        currentStage = 1;

    }
}
