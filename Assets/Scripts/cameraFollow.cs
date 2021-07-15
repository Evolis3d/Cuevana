using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform Target;
    public float factor = 2f;
    
    private void Update()
    {
        if (!Target) return;
        
        var newpos = new Vector3(Target.position.x, Target.position.y, -10f);
        var pos = Vector3.Lerp(transform.position, newpos, factor * Time.deltaTime);

        transform.position = pos;
    }

    public void SetTarget(Transform targ)
    {
        Target = targ;
    }
}
