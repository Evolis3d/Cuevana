using UnityEngine;

public class control_minion : MonoBehaviour
{
    private SpriteRenderer _spr;
    private Animator _anim;
    public float speed = 1f;

    private void Awake()
    {
        _spr = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
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
    }
    
}
