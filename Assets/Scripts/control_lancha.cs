using System.Collections;
using UnityEngine;

public class control_lancha : Interactivo
{
    [Header("Lancha")] 
    private Vector2 dir;
    private float lado,lastlado;
    public float thrust = 4f;
    public float vel;
    private bool canMove = false;
    
    private Rigidbody2D _rb;
    private BoxCollider2D _col;
    private SpriteRenderer _spr;
    
    [Header("Minion Playable")]
    public GameObject prefabMinion;
    private GameObject _miniYo;
    private cameraFollow _follow;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<BoxCollider2D>();
        _spr = GetComponent<SpriteRenderer>();
        _follow = Camera.main.GetComponent<cameraFollow>();
    }
    
    private void Start()
    {
        //timer = 0f;

        dir = transform.right; //por defecto mira a la derecha
        _rb.velocity = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove) return;
        
        if ((Input.GetKey(KeyCode.UpArrow)) || Input.GetButton("Thrust"))
        {
            _rb.velocity += dir * (thrust * Time.deltaTime);
        }

        vel = _rb.velocity.magnitude;

        dir = _spr.flipX ? Vector2.left : Vector2.right;

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            lado = Input.GetAxisRaw("Horizontal");

            if (lado != lastlado)
            {
                if (lado >= 1) _spr.flipX = false;
                if (lado <= -1) _spr.flipX = true;
                lastlado = lado;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("suelo"))
        {
            var choque = other.GetContact(0).normal;

            if (choque.y > 0)
            {
                canMove = false;
                return;
            }

            var rebote = Vector2.Reflect(_rb.velocity,choque);
            _rb.velocity = rebote;
            _rb.freezeRotation = false;
            StartCoroutine(nameof(resetRebote));
        }
    }
    
    IEnumerator resetRebote()
    {
        yield return new WaitForSeconds(0.5f);
        _rb.angularVelocity = 0f;
        _rb.freezeRotation = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("water"))
        {
            canMove = true;
        }
    }
}
