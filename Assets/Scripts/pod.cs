using UnityEngine;

public class pod : MonoBehaviour
{
    public GameObject prefabMinion;
    
    public float speed = 1f;
    private Rigidbody2D rb;
    private float timer;
    private float timelimit = 0.5f;
    
    private float rot;
    private Vector2 dir;

    private GameObject miniYo;
    private cameraFollow _follow;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _follow = Camera.main.GetComponent<cameraFollow>();
    }

    private void Start()
    {
        timer = 0f;

        rot = transform.eulerAngles.z;
        dir = new Vector2(Mathf.Cos((rot+90f) * Mathf.Deg2Rad), Mathf.Sin((rot+90f) * Mathf.Deg2Rad) );
        
        rb.velocity = Vector2.up * (speed );
    }

    private void Update()
    {
        if (timer > timelimit)
        {
            rb.velocity = Vector2.zero;
            
            //explota e instancia un paracaidista...
            if (prefabMinion) miniYo = Instantiate(prefabMinion, transform.position, Quaternion.identity);
            _follow.SetTarget(miniYo.transform);
            Destroy(gameObject);
        }
        else
        {
            //rb.velocity += dir * (speed * Time.deltaTime);
            //rb.velocity += Vector2.up * (speed * Time.deltaTime);

            rb.velocity *= 0.98f;
            timer+= 1f* Time.deltaTime;
        }
    }
    
}
