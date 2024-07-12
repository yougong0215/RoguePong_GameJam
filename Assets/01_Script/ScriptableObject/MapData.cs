using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MapData", menuName = "Assets/MapData")]
public class MapData : ScriptableObject
{
    public List<GameObject> mapList;
}
