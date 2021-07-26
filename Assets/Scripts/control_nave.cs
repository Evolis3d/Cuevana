﻿using System.Collections;
using UnityEngine;

public class control_nave : Interactivo
{
    private float rot;
    private Vector2 dir;
    public float thrust = 1.3f;
    public float vel;

    public bool landed; 
    
    private Rigidbody2D rb;
    private PolygonCollider2D col;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        vel = rb.velocity.magnitude;
        
        //aterrizar 
        if (vel > 0.1f)
        {
            landed = false;
        }
        else
        {
            var bottom = transform.position - new Vector3(0, 0.2f, 0);
            var hit = Physics2D.Raycast(bottom, -transform.up, 0.2f, LayerMask.GetMask("suelos"));
            Debug.DrawLine(bottom, bottom - new Vector3(0, 0.2f, 0), Color.magenta);
            landed = (hit.collider != null && vel < 0.1f);
            
        }
        //
        
        //metricas del modo de juego
        GameMode.PlayerLanded = landed;
        //

        if ((Input.GetKey(KeyCode.UpArrow)) || Input.GetButton("Thrust"))
        {
            rb.velocity += dir * (thrust * Time.deltaTime);
            
            //ñapa
            if (rb.angularVelocity!=0) rb.angularVelocity = 0f;
        }

        if ((Input.GetKey(KeyCode.Q)) || Input.GetAxisRaw("RCS") <= -1)
        {
            var corregido = Vector2.Perpendicular(dir) * -1f;
            rb.velocity += corregido * (thrust * 0.5f * Time.deltaTime);
            
            //ñapa
            if (rb.angularVelocity!=0) rb.angularVelocity = 0f;
        }
        
        if ((Input.GetKey(KeyCode.E)) || Input.GetAxisRaw("RCS") >= 1)
        {
            var corregido = Vector2.Perpendicular(dir);
            rb.velocity += corregido * (thrust * 0.5f * Time.deltaTime);
            
            //ñapa
            if (rb.angularVelocity!=0) rb.angularVelocity = 0f;
        }


        if (Input.GetAxis("Horizontal") != 0)
        {
            rb.rotation -= Input.GetAxis("Horizontal");
            
            //ñapa
            if (rb.angularVelocity!=0) rb.angularVelocity = 0f;
        }
        
        //pausa,break
        if (Input.GetKeyDown(KeyCode.P)) Debug.Break();
        
        rot = transform.eulerAngles.z;

        dir = new Vector2(Mathf.Cos((rot+90f) * Mathf.Deg2Rad), Mathf.Sin((rot+90f) * Mathf.Deg2Rad) );
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var tago = other.transform.tag;
        switch (tago)
        {
            case "base" : case "suelo":
                //
                if (vel > 1f)
                {
                    //se destruye la nave
                    GameMode.Lives--;
                    GameMode.PrisonersKilled += GameMode.PrisonersAboard;
                    GameMode.PrisonersAboard = 0;
                    Destroy(gameObject);
                } else if (vel > 0.5f)
                {
                    var choque = other.GetContact(0).normal; 
                    var rebote = Vector2.Reflect(rb.velocity,choque);
                    rb.velocity = rebote;

                    StartCoroutine(nameof(resetRebote));
                }
                //
                break;
        }
    }

    IEnumerator resetRebote()
    {
        yield return new WaitForSeconds(0.5f);
        rb.angularVelocity = 0f;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("0gravzone"))
        {
            foreach (var point in col.points)
            {
                var po = transform.TransformPoint(point);
                if (!other.OverlapPoint(po)) return;
            }
            
            if (rb.gravityScale !=0f) rb.gravityScale = 0f; //por defecto la nave tiene 0.1
        }

        if (other.CompareTag("invertgravzone"))
        {
            foreach (var point in col.points)
            {
                var po = transform.TransformPoint(point);
                if (!other.OverlapPoint(po)) return;
            }
            
            if (rb.gravityScale != -0.1f) rb.gravityScale = -0.1f; //por defecto la nave tiene 0.1
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("0gravzone"))
        {
            rb.gravityScale = 0.1f; //por defecto la nave tiene 0.1
        }

        if (other.CompareTag("invertgravzone"))
        {
            rb.gravityScale = 0.1f; //por defecto la nave tiene 0.1 y hacia abajo..
        }
    }
}
