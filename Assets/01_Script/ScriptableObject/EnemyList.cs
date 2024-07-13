using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class EnemyInfo
{
    public string EnemyName;
    public GameObject EnemyPrefab;
}


[CreateAssetMenu(fileName = "EnemyData", menuName = "Assets/EnemyData")]
public class EnemyData : ScriptableObject
{
    public List<EnemyInfo> Data;
}
