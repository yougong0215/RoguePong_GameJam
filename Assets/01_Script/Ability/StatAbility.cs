using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatEnum
{
    DAMAGE = 0,
    SPEED = 1,
    SIZE = 2,
    DurationTime = 3,
    HP = 4,
    Reflect =5,
    None,
}

public enum MathType
{
    Plus = 0,
    Multiply = 1,
}


[System.Serializable]
public class StatSO
{
    [SerializeField] public StatEnum AbilityID = StatEnum.None;
    [SerializeField] public MathType MathType = MathType.Plus;
    [SerializeField] public float Value;
}


[System.Serializable]
public class StatSystem
{

    [Header("AddStat")]
    [SerializeField] public float _addATK = 0f;
    [SerializeField] public float _addSize = 0f;
    [SerializeField] public float _addSpeed = 0f;
    [SerializeField] public float _addDurationTime = 0f;
    [SerializeField] public float _addHP = 0;
    [SerializeField] public float _addReflect = 0;

    [Header("MultiplyStat")]
    [SerializeField] public float _multyATK = 1f;
    [SerializeField] public float _multySize = 1f;
    [SerializeField] public float _multySpeed = 1f;
    [SerializeField] public float _multyDurationTime = 1f;
    [SerializeField] public float _multyHP = 1f;
    [SerializeField] public float _multyReflect = 1f;

    public float ResultATK(float origin = 0)
    {
        return (origin + _addATK) * _multyATK;
    }
    public void ResetATK() { _addATK = 0; _multyATK = 1f; }
    public float ResultSize(float origin = 0)
    {
        return (origin + _addSize) * _multySize;
    }
    public void ResetSize() { _addSize = 0; _multySize = 1f; }
    public float ResultSpeed(float origin = 0)
    {
        return (origin + _addSpeed) * _multySpeed;
    }
    public void ResetSpeed() { _addSpeed = 0; _multySpeed = 1f; }
    public float ResultDuration(float origin = 0)
    {
        return (origin + _addDurationTime) * _multyDurationTime;
    }
    public void ResetDuration() { _addDurationTime = 0; _multyDurationTime = 1f; }

    public float ResultHP(float origin = 0)
    {
        return (origin + _addHP) * _multyHP;
    }
    public void ResetHP() { _addHP = 0; _multyHP = 1f; }

    public float ResultReflect (float origin = 0)
    {
        return (origin + _addHP) * _multyHP;
    }
    public void ResetReflect() { _addHP = 0; _multyHP = 1f; }

}

[CreateAssetMenu(menuName ="SO/StatAbility")]
public class StatAbility : Ability
{
    [SerializeField] List<StatSO> Ability;
    public override void GetAbility(ref ObjectSystem ballStat)
    {

        foreach (StatSO stat in Ability)
        {
            switch (stat.AbilityID)
            {
                case StatEnum.DAMAGE:
                    {
                        if (stat.MathType == MathType.Plus)
                        {
                            ballStat._abilityStat._addATK += stat.Value;
                        }
                        else if (stat.MathType == MathType.Multiply)
                        {
                            ballStat._abilityStat._multyATK += stat.Value;
                        }
                    }
                    break;
                case StatEnum.SPEED:
                    {
                        if (stat.MathType == MathType.Plus)
                        {
                            ballStat._abilityStat._addSpeed += stat.Value;
                        }
                        else if (stat.MathType == MathType.Multiply)
                        {
                            ballStat._abilityStat._multySpeed += stat.Value;
                        }
                    }
                    break;
                case StatEnum.SIZE:
                    {
                        if (stat.MathType == MathType.Plus)
                        {
                            ballStat._abilityStat._addSize += stat.Value;
                        }
                        else if (stat.MathType == MathType.Multiply)
                        {
                            ballStat._abilityStat._multySize += stat.Value;
                        }
                    }
                    break;
                case StatEnum.DurationTime:
                    {
                        if (stat.MathType == MathType.Plus)
                        {
                            ballStat._abilityStat._addDurationTime += stat.Value;
                        }
                        else if (stat.MathType == MathType.Multiply)
                        {
                            ballStat._abilityStat._multyDurationTime += stat.Value;
                        }
                    }
                    break;
                case StatEnum.HP:
                    {
                        if (stat.MathType == MathType.Plus)
                        {
                            ballStat._abilityStat._addHP += stat.Value;
                        }
                        else if (stat.MathType == MathType.Multiply)
                        {
                            ballStat._abilityStat._multyHP += stat.Value;
                        }
                    }

                    break;
                case StatEnum.None:
                    Debug.LogError("지정되지 않은 스텟");
                    break;
                case StatEnum.Reflect:
                    {
                        if (stat.MathType == MathType.Plus)
                        {
                            ballStat._abilityStat._addReflect += stat.Value;
                        }
                        else if (stat.MathType == MathType.Multiply)
                        {
                            ballStat._abilityStat._multyReflect += stat.Value;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
