using UnityEngine;

public class control_minion : MonoBehaviour
{
    private SpriteRenderer _spr;
    private Animator _anim;
    private Rigidbody2D _rb;
    public float speed = 1f;
    public float jumpforce = 5f;
    private void Awake()
    {
        _spr = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
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
           _rb.AddForce(transform.up * jumpforce,ForceMode2D.Force); 
        }
    }
    
}
