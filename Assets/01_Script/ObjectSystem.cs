using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSystem : MonoBehaviour
{
    [Header("OriginStat")]
    [SerializeField] protected float _originATK = 10f;
    [SerializeField] protected float _originSize = 1f;
    [SerializeField] protected float _originSpeed = 10f;
    [SerializeField] protected float _originDurationTime = 3f;

    [Header("SystemStat")]
    public BallStat _ballStat = new();

    [Header("AbilityStat")]
    public BallStat _abilityStat = new();

    public float GetATKValue() { return _abilityStat.ResultATK(_ballStat.ResultATK(_originATK)); }
    public float GetSizeValue() { return _abilityStat.ResultSize(_ballStat.ResultSize(_originSize)); }
    public float GetSpeedValue() { return _abilityStat.ResultSpeed(_ballStat.ResultSpeed(_originSpeed)); }
    public float GetDurationValue() { return _abilityStat.ResultDuration(_ballStat.ResultDuration(_originDurationTime)); }

}
