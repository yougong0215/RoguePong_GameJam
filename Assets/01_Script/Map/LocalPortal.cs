using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPortal : MonoBehaviour
{
    public LocalPortal oppositePortal;
    private BoxColliderCast boxcast;
    // Start is called before the first frame update
    void Start()
    {
        boxcast = GetComponent<BoxColliderCast>();
        boxcast.Now(transform, (a) => { TriggerEnter(a); });
    }


    public void PortalOff()
    {
        boxcast.End();
        StartCoroutine(StartAgin());
    }


    // Update is called once per frame
    private void TriggerEnter(Collider other)
    {
        PortalOff();
        oppositePortal.PortalOff();
        
        Debug.LogError($"{other.gameObject.transform.position} / {oppositePortal.transform.position}");
        
        StartCoroutine(other.GetComponent<PlayerSystem>().FrameCharacterConoff());
        other.gameObject.transform.position = oppositePortal.transform.position;

    }

    IEnumerator StartAgin()
    {
        yield return new WaitForSeconds(1f);
        boxcast.Now(transform, (a) => { TriggerEnter(a); });
    }
}
