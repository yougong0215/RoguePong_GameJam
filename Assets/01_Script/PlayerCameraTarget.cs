using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraTarget : MonoBehaviour
{
    private Transform playerTr;

    private void Awake()
    {
        playerTr = GameManager.Instance.Player.transform;
    }

    void Update()
    {
        transform.position = playerTr.position;
    }
}
