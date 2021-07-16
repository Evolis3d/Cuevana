using System.Net;
using UnityEngine;

public class control_minion : MonoBehaviour
{
    private SpriteRenderer _spr;
    private Animator _anim;
    private Rigidbody2D _rb;
   
    public float speed = 1f;
    public float jumpforce = 5f;

    private bool onGround;
    
    private void Awake()
    {
        _spr = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (onGround)
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                if (_anim.GetBool("isMoving")!=true) _anim.SetBool("isMoving",true);
            
                //muevo
                var lado = Input.GetAxisRaw("Horizontal");
                _spr.flipX = lado >= 1;  
                transform.localPosition += Vector3.left * (-lado * speed * Time.deltaTime);
            }
            else
            {
                if (_anim.GetBool("isMoving")!=false) _anim.SetBool("isMoving",false);
            }
        
            if ((Input.GetButtonDown("Thrust")) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (onGround != true) return;
                
                _rb.AddForce(transform.up * jumpforce, ForceMode2D.Force);
                onGround = false;
            }  
        }
        else
        {
            var layermask = LayerMask.GetMask("suelos");
            var ray = transform.TransformDirection(Vector2.down);
            var hit = Physics2D.Raycast(transform.position, ray,0.25f,layermask);
            if (hit.collider != null)
            {
                onGround = true;
            }
        }

    }
    
}
