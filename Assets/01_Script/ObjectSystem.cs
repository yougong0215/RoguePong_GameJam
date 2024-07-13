using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ObjectSystem : PoolAble
{
    [Header("HP")]
    [SerializeField] protected float _originHP = 0;

    [Header("OriginStat")]
    [SerializeField] protected float _originATK = 10f;
    [SerializeField] protected float _originSize = 1f;
    [SerializeField] protected float _originSpeed = 10f;
    [SerializeField] protected float _originDurationTime = 3f;
    [SerializeField] protected float _originReflectRange = 15f;

    [Header("SystemStat")]
    public StatSystem _originStat = new();

    [Header("AbilityStat")]
    public StatSystem _abilityStat = new();

    [Header("AbilityStat")]
    public StatSystem _DebuffStat = new();

    public float GetATKValue() { return _abilityStat.ResultATK(_originStat.ResultATK(_DebuffStat.ResultATK(_originATK))); }
    public float GetSizeValue() { return _abilityStat.ResultSize(_originStat.ResultSize(_DebuffStat.ResultSize(_originSize))); }
    public float GetSpeedValue() { return _abilityStat.ResultSpeed(_originStat.ResultSpeed(_DebuffStat.ResultSpeed(_originSpeed))); }
    public float GetDurationValue() { return _abilityStat.ResultDuration(_originStat.ResultDuration(_DebuffStat.ResultDuration(_originDurationTime))); }

    public float GetHPValue() { return _abilityStat.ResultHP(_originStat.ResultHP(_DebuffStat.ResultHP(_originHP))); }
    public float GetReflectValue() { return _abilityStat.ResultReflect(_originStat.ResultReflect(_DebuffStat.ResultReflect(_originReflectRange))); }

    public void Debuff(StatEnum st,MathType math, float Value, float Time)
    {
        StartCoroutine(DebuffCoroutine(st, math, Value, Time));
    }

    IEnumerator DebuffCoroutine(StatEnum st, MathType math, float Value, float Time)
    {
        OperatedStat(ref _DebuffStat, st, math, -Value);
        yield return new WaitForSeconds(Time);
        OperatedStat(ref _DebuffStat, st, math, +Value);
    }

    public void OperatedStat(ref StatSystem TempStat, StatEnum st, MathType math, float _value)
    {
        switch (st)
        {
            case StatEnum.DAMAGE:
                {
                    switch (math)
                    {
                        case MathType.Plus:
                            TempStat._addATK += _value;
                            break;
                        case MathType.Multiply:
                            TempStat._multyATK += _value;
                            break;
                    }
                }
                break;
            case StatEnum.SPEED:
                {
                    switch (math)
                    {
                        case MathType.Plus:
                            TempStat._addSpeed += _value;
                            break;
                        case MathType.Multiply:
                            TempStat._multySpeed += _value;
                            break;
                    }
                }
                break;
            case StatEnum.SIZE:
                {
                    switch (math)
                    {
                        case MathType.Plus:
                            TempStat._addSize += _value;
                            break;
                        case MathType.Multiply:
                            TempStat._multySize += _value;
                            break;
                    }
                }
                break;
            case StatEnum.DurationTime:
                {
                    switch (math)
                    {
                        case MathType.Plus:
                            TempStat._addDurationTime += _value;
                            break;
                        case MathType.Multiply:
                            TempStat._multyDurationTime += _value;
                            break;
                    }
                }
                break;
            case StatEnum.HP:
                {
                    switch (math)
                    {
                        case MathType.Plus:
                            TempStat._addHP += _value;
                            break;
                        case MathType.Multiply:
                            TempStat._multyDurationTime += _value;
                            break;
                    }
                }
                break;
            case StatEnum.None:
                break;
        }
    }
}