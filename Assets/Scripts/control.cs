using System;
using System.Collections;
using UnityEngine;

public class control : MonoBehaviour
{
    private float rot;
    private Vector2 dir;
    public float thrust = 1.3f;
    public float vel;
    
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
        if ((Input.GetKey(KeyCode.UpArrow)) || Input.GetButton("Thrust"))
        {
            rb.velocity += dir * (thrust * Time.deltaTime);
            
            //ñapa
            if (rb.angularVelocity!=0) rb.angularVelocity = 0f;
        }

        if ((Input.GetKey(KeyCode.Q)) || Input.GetButton("LeftRCS"))
        {
            var corregido = Vector2.Perpendicular(dir) * -1f;
            rb.velocity += corregido * (thrust * 0.5f * Time.deltaTime);
            
            //ñapa
            if (rb.angularVelocity!=0) rb.angularVelocity = 0f;
        }
        
        if ((Input.GetKey(KeyCode.E)) || Input.GetButton("RightRCS"))
        {
            var corregido = Vector2.Perpendicular(dir);
            rb.velocity += corregido * (thrust * 0.5f * Time.deltaTime);
            
            //ñapa
            if (rb.angularVelocity!=0) rb.angularVelocity = 0f;
        }

        vel = rb.velocity.magnitude;


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
        if (other.transform.CompareTag("suelo"))
        {
            if (vel > 1f)
            {
                Destroy(gameObject);
            } else if (vel > 0.5f)
            {
                var choque = other.GetContact(0).normal; //.contacts[0].normal;
                var rebote = Vector2.Reflect(rb.velocity,choque);
                rb.velocity = rebote;

                StartCoroutine(nameof(resetRebote));
                //rb.angularVelocity = 0f;
            }
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
