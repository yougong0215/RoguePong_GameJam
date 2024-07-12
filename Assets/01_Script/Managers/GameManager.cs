using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private PlayerAbility _player;
    public int currentStage;
    public bool isCleared = true;

    public PlayerAbility Player
    {
        get
        {
            if (_player == null)
            {
                _player = GameObject.FindObjectOfType<PlayerAbility>();
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
