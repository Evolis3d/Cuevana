using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class bullet : MonoBehaviour
{
    public float damage = 10f;
    public float speed = 10f;
    
    private Collider2D col;
    private SpriteRenderer rend;
    private bool isMoving = true;
    private Vector2 dir;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        rend = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Reset();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isMoving)
        {
            transform.Translate(dir * (speed * Time.deltaTime));
        }
    }

    private void OnBecameInvisible()
    {
        Reset();
    }

    public void Reset()
    {
        col.enabled = false;
        rend.enabled = false;
        col.transform.position = Vector3.zero;
        dir = Vector2.zero;
        isMoving = false;
    }

    public void Fire(Vector3 pos = new Vector3(), Vector2 direc = new Vector2())
    {
        col.enabled = true;
        rend.enabled = true;
        col.transform.position = pos;
        dir = direc;
        isMoving = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("suelo")) // por ejemplo
        {
            Reset();
        }
    }
}
