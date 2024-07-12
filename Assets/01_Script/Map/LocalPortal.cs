using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPortal : MonoBehaviour
{
    public GameObject oppositePortal;
    private BoxColliderCast boxcast;
    // Start is called before the first frame update
    void Start()
    {
        boxcast = GetComponent<BoxColliderCast>();
    }

    // Update is called once per frame
    void Update()
    {
        if (boxcast.ReturnColliders().Length > 0)
        {
            TriggerEnter(boxcast.ReturnColliders()[0]);
        }
    }

    private void TriggerEnter(Collider other)
    {
        print("AAAAAA");
        other.gameObject.transform.parent.position = oppositePortal.transform.position;
    }
}
