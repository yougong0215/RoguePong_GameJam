using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[Serializable]
public class RestAsset
{
    public int recoveryRatio;
    public float Probability;
}

[CreateAssetMenu(fileName = "RestData")]
public class RestData : ScriptableObject
{
    public List<RestAsset> restAssets;
}
