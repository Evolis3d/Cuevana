using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control_heli : Interactivo
{
    private Rigidbody2D _rb;
    private BoxCollider2D _col;
    
    public float thrust = 2f;
    private Vector2 dir;
    public float maxsteer = 20f;
    private float caida;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _rb.velocity+= Vector2.up * (thrust * Time.deltaTime);
        }

        var _steer = Input.GetAxis("Horizontal");
        //dir.x = _steer * -maxsteer;
        dir.x = Mathf.Lerp(dir.x, (_steer * -maxsteer),  Time.deltaTime);
        _rb.rotation = dir.x;
        
        Bounds boxBounds = _col.bounds;
        Vector2 topRight = new Vector2(boxBounds.center.x + boxBounds.extents.x, boxBounds.center.y + boxBounds.extents.y);
        caida = transform.TransformPoint(topRight).magnitude;

        //if (_steer != 0) _rb.gravityScale = caida;
        
        _rb.velocity = new Vector2(-dir.x * 0.25f, _rb.velocity.y);
        Debug.Log(_rb.velocity.magnitude);
        
        //pausa,break
        if (Input.GetKeyDown(KeyCode.P)) Debug.Break();
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        var tago = other.transform.tag;
        switch (tago)
        {
            case "base" : case "suelo":
                //
                var vel = _rb.velocity.magnitude;
                if (vel > 1f)
                {
                    Destroy(gameObject);
                } else if (vel > 0.5f)
                {
                    var choque = other.GetContact(0).normal; 
                    var rebote = Vector2.Reflect(_rb.velocity,choque);
                    _rb.velocity = rebote;

                    StartCoroutine(nameof(resetRebote));
                }
                //
                break;
        }
    }
    
    IEnumerator resetRebote()
    {
        yield return new WaitForSeconds(0.5f);
        _rb.angularVelocity = 0f;
    }
    
}
