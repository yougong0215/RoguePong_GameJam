using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class SaleItem
{
    public Ability ability;
    public BallSkillEnum ballSkillEnum;
    public float minPrice;
    public float maxPrice;
}

[CreateAssetMenu(fileName = "StoreData")]
public class StoreData : ScriptableObject
{
    public List<SaleItem> saleItems;
}
