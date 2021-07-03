using System.Collections;
using UnityEngine;

public class control : MonoBehaviour
{
    private float rot;
    private Vector2 dir;
    public float thrust = 1.3f;
    public float vel;
    
    private Rigidbody2D rb;

    [Header("sensores")] 
    public SpriteRenderer head;
    public SpriteRenderer leftRCS;
    public SpriteRenderer rightRCS;

    [Header("escapePod")] 
    public GameObject prefabPod;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey(KeyCode.UpArrow)) || Input.GetButton("Thrust"))
        {
            rb.velocity += dir * (thrust * Time.deltaTime);
        }

        if ((Input.GetKey(KeyCode.Q)) || Input.GetButton("LeftRCS"))
        {
            var corregido = Vector2.Perpendicular(dir) * -1f;
            rb.velocity += corregido * (thrust * 0.5f * Time.deltaTime);
        }
        
        if ((Input.GetKey(KeyCode.E)) || Input.GetButton("RightRCS"))
        {
            var corregido = Vector2.Perpendicular(dir);
            rb.velocity += corregido * (thrust * 0.5f * Time.deltaTime);
        }

        vel = rb.velocity.magnitude;


        if (Input.GetAxis("Horizontal") != 0)
        {
            rb.rotation -= Input.GetAxis("Horizontal");
        }
        
        //pausa,break
        if (Input.GetKeyDown(KeyCode.P)) Debug.Break();
        
        //escapePod
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (prefabPod) Instantiate(prefabPod, transform.position + new Vector3(0,0.5f,0), transform.rotation);
            //pierdo el control de la nave...
            Destroy(gameObject,3f);
            this.enabled = false;
        }

        //sensores
        head.enabled = Input.GetKey(KeyCode.UpArrow) || Input.GetButton("Thrust");
        leftRCS.enabled = Input.GetKey(KeyCode.Q) || Input.GetButton("LeftRCS");
        rightRCS.enabled = Input.GetKey(KeyCode.E) || Input.GetButton("RightRCS");


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
}
