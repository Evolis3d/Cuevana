using System.Collections.Generic;
using UnityEngine;

public class disparable_pool : MonoBehaviour
{
    public GameObject prefabBullet;
    public int poolSize = 15;
    
    [SerializeField] private List<GameObject> pool = new List<GameObject>();
    private GameObject PoolFolder;
    private int bulletsLeft;
    
    private void Start()
    {
        PoolFolder = new GameObject("Pool-Bullets");
        
        for (int i = 0; i < poolSize; i++)
        {
            var bullet = Instantiate(prefabBullet);
            pool.Add(bullet);
            //pool[i].GetComponent<bullet>()...
            pool[i].transform.SetParent(PoolFolder.transform);
            pool[i].transform.GetComponent<bullet>().Reset();
            bulletsLeft++;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //ñapa guarra
            pool[bulletsLeft-1].transform.SetParent(transform);
            pool[bulletsLeft-1].transform.position = transform.position;
            pool[bulletsLeft - 1].transform.rotation = transform.rotation;
            pool[bulletsLeft-1].transform.Translate(0f,0.6f,0f);
            
            var pos = pool[bulletsLeft - 1].transform.position;
            var rot = transform.rotation.z;
            var dir = new Vector2(Mathf.Cos((rot+90f) * Mathf.Deg2Rad), Mathf.Sin((rot+90f) * Mathf.Deg2Rad) );
            
            pool[bulletsLeft-1].GetComponent<bullet>().Fire(this, pos,dir);
            pool[bulletsLeft-1].transform.SetParent(PoolFolder.transform);
            bulletsLeft--;
        }
    }

    public void Recycle(GameObject bala)
    {
        pool[pool.IndexOf(bala)].transform.SetParent(PoolFolder.transform);
        pool[pool.IndexOf(bala)].transform.GetComponent<bullet>().Reset();
        bulletsLeft++;
    }
    
}
