using UnityEngine;

public class enemy_cannon : MonoBehaviour
{
    public GameObject prefabBullet;
    [SerializeField] private GameObject turret;
    [SerializeField] private Transform aiming;
    private float _freqshoot = 2f;
    private float _timershoot;
    private GameObject _bullet;

    private void Start()
    {
        _timershoot = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (aiming)
        {
            var dir = aiming.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            //Debug.Log(angle);
            
            turret.transform.right = aiming.position - turret.transform.position;

            if (_timershoot > _freqshoot && angle > 10f)
            {
                _timershoot = 0f;
                if (!prefabBullet) return;
                _bullet = Instantiate(prefabBullet, turret.transform.position, Quaternion.identity);
            }
            else
            {
                _timershoot += 1f * Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("nave"))
        {
            aiming = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("nave"))
        {
            aiming = null;
        }
    }
}
