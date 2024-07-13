using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : ObjectSystem
{

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, _originSpeed * Time.deltaTime, 0);
    }
}
