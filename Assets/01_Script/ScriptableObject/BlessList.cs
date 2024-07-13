using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BlessList")]
public class BlessList : ScriptableObject
{
    public List<BlessData> GoldBlessList;
    public List<BlessData> HPBlessList;
    public List<BlessData> PlayerBuffList;
}