using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escaliator : MonoBehaviour
{
    private BoxColliderCast boxcast;
    // Start is called before the first frame update
    void Start()
    {
        boxcast = GetComponent<BoxColliderCast>();
        boxcast.Now(transform, (a) => { TriggerEnter(a); });
    }

    // Update is called once per frame
    private void TriggerEnter(Collider other)
    {
        StartCoroutine(other.GetComponent<PlayerSystem>().FrameCharacterConoff());

    }
}
