using System.Collections;
using UnityEngine;

public class control_moto : Interactivo
{
    [Header("Motocicleta")] 
    private Vector2 dir;
    private float lado,lastlado;
    private bool stopped;
    
    public float thrust = 3f;
    public float vel;
    
    private Rigidbody2D _rb;
    
    [Header("Minion Playable")]
    public GameObject prefabMinion;
    private GameObject _miniYo;
    private cameraFollow _follow;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
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
        GameMode.PlayerLanded = stopped;
        
        Mechanics_ACCEL();

        Mechanics_MOVE();
    }

    private void Mechanics_MOVE()
    {
        dir = (transform.localScale == Vector3.one) ? Vector2.right : Vector2.left;
        var isMoving = Input.GetAxisRaw("Horizontal") != 0f;
        if (isMoving)
        {
            lado = Input.GetAxisRaw("Horizontal");

            if (lado != lastlado)
            {
                if (lado >= 1f) transform.localScale = Vector3.one;
                if (lado <= -1f) transform.localScale = Vector3.left + Vector3.up + Vector3.forward;
                lastlado = lado;
            }
        }
    }

    private void Mechanics_ACCEL()
    {
        if ((Input.GetKey(KeyCode.UpArrow)) || Input.GetButton("Thrust"))
        {
            _rb.velocity += dir * (thrust * Time.deltaTime);
        }

        vel = _rb.velocity.magnitude;

        stopped = (Mathf.Approximately(vel,0f));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("base")) // o choca contra otro tipo de edificios y cosas duras...
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
