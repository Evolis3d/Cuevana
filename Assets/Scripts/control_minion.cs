using UnityEngine;

public class control_minion : Interactivo
{
    private SpriteRenderer _spr;
    private Animator _anim;
    private Rigidbody2D _rb;
   
    public float speed = 1f;
    public float jumpforce = 5f;

    bool onGround;
    
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
                transform.localPosition += Vector3.left * (-lado * speed * Time.deltaTime);
                //el sprite se flipea
                _spr.flipX = lado >= 1;
                if (transform.localRotation.z == 1) _spr.flipX = !_spr.flipX;
            }
            else
            {
                if (_anim.GetBool("isMoving")!=false) _anim.SetBool("isMoving",false);
            }
            
            if ((Input.GetButtonDown("Thrust")) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                _rb.AddForce(transform.up * jumpforce, ForceMode2D.Force);
                onGround = false;
            } 
             
        }
        else
        {
            var layermask = LayerMask.GetMask("suelos");
            var ray = transform.TransformDirection(Vector2.down);

            //if (!(_rb.velocity.y < 0)) return;
            if (Vector2.Dot(_rb.velocity, ray ) > 0)
            {


                var hit = Physics2D.Raycast(transform.position, ray, 0.25f, layermask);
                Debug.DrawRay(transform.position, ray * 0.25f, Color.green);

                onGround = hit.collider != null;
            }
        }
    }
    
}
