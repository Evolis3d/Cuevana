using System.Collections;
using UnityEngine;

public class control_moto : Interactivo
{
    [Header("Motocicleta")] 
    private Vector2 dir;
    private float lado,lastlado;
    public float thrust = 3f;
    public float vel;
    
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
                if (lado <= -1)_spr.flipX = true;
                lastlado = lado;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("base"))
        {
            if (vel > 1f)
            {
                Destroy(gameObject);
            } 
            else if (vel > 0.5f)
            {
                var choque = other.GetContact(0).normal; 
                var rebote = Vector2.Reflect(_rb.velocity,choque);
                _rb.velocity = rebote;

                StartCoroutine(nameof(resetRebote));
            }
        }
    }

    IEnumerator resetRebote()
    {
        yield return new WaitForSeconds(0.5f);
        _rb.angularVelocity = 0f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("water"))
        {
            //explota e instancia un Minion Playable.
            if (prefabMinion) _miniYo = Instantiate(prefabMinion, transform.position, Quaternion.identity);
            _follow.SetTarget(_miniYo.transform);
            Destroy(gameObject);
        }
    }
}
