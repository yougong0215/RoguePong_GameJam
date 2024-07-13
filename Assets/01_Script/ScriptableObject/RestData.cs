using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class RestAsset
{
    public float recoveryRatio;
    public float Probability;
}

[CreateAssetMenu(fileName = "RestData")]
public class RestData : ScriptableObject
{
    public List<RestAsset> restAssets;
}
