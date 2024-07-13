using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FloorTrailType
{
    Lava,
    Ice
}
public class FloorTrail : MonoBehaviour
{
    private BoxColliderCast boxcast;
    public FloorTrailType trailType;
    // Start is called before the first frame update
    void Start()
    {
        boxcast = GetComponent<BoxColliderCast>();
        boxcast.Now(transform, (a) => { TriggerEnter(a); });
    }

    public void FloorOff()
    {
        boxcast.End();
        StartCoroutine(StartAgin());
    }

    private void TriggerEnter(Collider other)
    {
        FloorOff();

        StartCoroutine(other.GetComponent<PlayerSystem>().FrameCharacterConoff());
        if (trailType == FloorTrailType.Lava)
        {
            //other.GetComponent<ObjectSystem>()._DebuffStat._addHP -= 10;
        }
        else if (trailType == FloorTrailType.Ice)
            throw new NotImplementedException(); //PlayerSystem에서 뭐 isOnIce??같은거 켜면 될려나
    }

    IEnumerator StartAgin()
    {
        yield return new WaitForSeconds(1f);
        boxcast.Now(transform, (a) => { TriggerEnter(a); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
