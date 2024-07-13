using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceGlobe : MonoBehaviour, HitModule
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HitBall(BallSystem bss)
    {
        animator.SetTrigger("Piston");
        bss._abilityStat._addSpeed = Mathf.Clamp(bss._abilityStat._addSpeed + 1, 0, 120);
        bss.SetttingDir(transform.forward);
        //bss.WallCollisionItem(GetComponent<Collider>());
        bss.RefreshTime();
    }
}
