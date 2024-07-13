using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;




[CreateAssetMenu(fileName = "BallData")]
public class BallData : ScriptableObject
{
    public List<BallAsset> BallAssets = new List<BallAsset>();
}
