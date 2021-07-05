using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disparable_pool : MonoBehaviour
{
    public GameObject prefabBullet;
    public int poolSize = 15;

    [SerializeField] private List<GameObject> pool = new List<GameObject>();
    private int bulletsLeft;
    
    private void Start()
    {
        var PoolFolder = new GameObject("Pool-Bullets");
        
        for (int i = 0; i < poolSize; i++)
        {
            var bullet = Instantiate(prefabBullet);
            pool.Add(bullet);
            //pool[i].GetComponent<bullet>()...
            pool[i].transform.SetParent(PoolFolder.transform);
            bulletsLeft++;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            pool[bulletsLeft-1].GetComponent<bullet>().Fire();
            bulletsLeft--;
        }
    }
}
