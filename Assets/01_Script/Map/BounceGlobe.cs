using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceGlobe : MonoBehaviour
{
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
        if (other.TryGetComponent<BallSystem>(out BallSystem bss))
        {
            bss._abilityStat._addSpeed = Mathf.Clamp(bss._abilityStat._addSpeed + 1, 0, 120);
            bss.SetttingDir(transform.forward);
            Debug.Log(transform.forward);
            //bss.WallCollisionItem(GetComponent<Collider>());
            bss.RefreshTime();
        }
    }
}
