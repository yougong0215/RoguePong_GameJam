using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class BallAsset
{
    public BallEnum BallType;
    public GameObject BallMesh;

    public float MultySpeed = 1.0f;
    public float MultySize = 1.0f;
    public float MultyATK = 1.0f;

}

[CreateAssetMenu(fileName = "BallData")]
public class BallData : ScriptableObject
{
    public List<BallAsset> BallAssets = new List<BallAsset>();
}
