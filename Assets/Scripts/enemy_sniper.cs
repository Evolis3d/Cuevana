using System;
using UnityEngine;

public class enemy_sniper : MonoBehaviour
{
    private bool _onSight;
    private Transform _target;
    private float _freqShoot = 3.5f;
    private float _timerShoot;
    private IA_disparable shotComp;

    private void Awake()
    {
        shotComp = GetComponent<IA_disparable>();
    }

    private void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (_onSight && _target)
        {
            var dir = _target.position - transform.position;
            transform.localScale = (dir.x > 0f) ? Vector3.one : Vector3.left + Vector3.up + Vector3.forward;
            
            var dist = Vector2.Distance(transform.position, _target.position);
            
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            angle = (transform.localScale.x == 1f) ? angle :  180f - angle;
            
            //apunto con el canyon
            if (angle < 85f)
            {
                //si está más lejos del mínimo, le dispara 
                if (dist > 1f)
                {
                    //disparo si puedo y cuando llega al timer
                    if (shotComp)
                    {
                        if (_timerShoot > _freqShoot)
                        {
                            _timerShoot = 0f;
                            shotComp.Shoot(transform,transform.right);
                        }
                        else
                        {
                            _timerShoot += 1f * Time.deltaTime;
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("moto") || other.CompareTag("lancha"))
        {
            _target = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("moto") || other.CompareTag("lancha"))
        {
            _target = null;
        }
    }
}
