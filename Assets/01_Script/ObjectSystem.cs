using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSystem : MonoBehaviour
{
    [Header("HP")]
    [SerializeField] protected float _originHP = 0;

    [Header("OriginStat")]
    [SerializeField] protected float _originATK = 10f;
    [SerializeField] protected float _originSize = 1f;
    [SerializeField] protected float _originSpeed = 10f;
    [SerializeField] protected float _originDurationTime = 3f;

    [Header("SystemStat")]
    public StatSystem _originStat = new();

    [Header("AbilityStat")]
    public StatSystem _abilityStat = new();

    public float GetATKValue() { return _abilityStat.ResultATK(_originStat.ResultATK(_originATK)); }
    public float GetSizeValue() { return _abilityStat.ResultSize(_originStat.ResultSize(_originSize)); }
    public float GetSpeedValue() { return _abilityStat.ResultSpeed(_originStat.ResultSpeed(_originSpeed)); }
    public float GetDurationValue() { return _abilityStat.ResultDuration(_originStat.ResultDuration(_originDurationTime)); }

    public float GetHPValue() { return _abilityStat.ResultHP(_originStat.ResultHP(_originHP)); }

}
