﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control_pod : MonoBehaviour
{
    [Header("escapePod")] 
    public GameObject prefabPod;

    [Header("Control Principal")] 
    public control cComp;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //escapePod
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (prefabPod) Instantiate(prefabPod, transform.position + new Vector3(0,0.5f,0), transform.rotation);
            //pierdo el control de la nave...
            Destroy(gameObject,3f);
            cComp.enabled = false;
        }
    }
}
