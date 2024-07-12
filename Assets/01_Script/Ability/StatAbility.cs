using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BallStatAbility
{
    DAMAGE = 0,
    SPEED = 1,
    SIZE = 2,
    DurationTime = 3,
    None,
}

public enum MathType
{
    Plus = 0,
    Multiply = 1,
}


[System.Serializable]
public class BallStatClass
{
    [SerializeField] public BallStatAbility AbilityID = BallStatAbility.None;
    [SerializeField] public MathType MathType = MathType.Plus;
    [SerializeField] public float Value;
}

public class StatAbility : Ability
{
    [SerializeField] List<BallStatClass> Ability;
    public override void GetAbility(BallSystem ballStat)
    {
        foreach (var stat in Ability)
        {
            switch (stat.AbilityID)
            {
                case BallStatAbility.DAMAGE:
                    break;
                case BallStatAbility.SPEED:
                    break;
                case BallStatAbility.SIZE:
                    break;
                case BallStatAbility.DurationTime:
                    break;
                case BallStatAbility.None:
                    Debug.LogError("지정되지 않은 스텟");
                    break;
            }
        }
    }
}
