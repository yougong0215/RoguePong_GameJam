using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(menuName ="SO/BallInfo")]
public class BallAsset : Ability
{
    public BallEnum BallType;
    public GameObject BallMesh;

    public float MultySpeed = 1.0f;
    public float MultySize = 1.0f;
    public float MultyATK = 1.0f;

    public override void GetAbility(ref ObjectSystem ballStat)
    {
        ballStat._originStat = new();
        ballStat._originStat._multySpeed = MultySpeed;
        ballStat._originStat._multySize = MultySize;
        ballStat._originStat._multyATK = MultyATK;
        GameObject.FindObjectsOfType<BallSystem>().ToList().ForEach((b) => { b.SettingBallType(BallType); });
    }
}

[CreateAssetMenu(fileName = "BallData")]
public class BallData : ScriptableObject
{
    public List<BallAsset> BallAssets = new List<BallAsset>();
}
