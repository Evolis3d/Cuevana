using System.Collections;
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
    private disparable_pool sender;

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

    public void Reset()
    {
        col.enabled = false;
        rend.enabled = false;
        col.transform.position = Vector3.zero;
        dir = Vector2.zero;
        isMoving = false;
    }

    public void Fire(disparable_pool author, Vector3 pos = new Vector3(), Vector2 direc = new Vector2() )
    {
        if (author.Equals(null)) return;

        sender = author;
        col.enabled = true;
        rend.enabled = true;
        col.transform.position = pos;
        dir = direc;
        isMoving = true;

        StartCoroutine(nameof(RecycleAtSecs), 10);
    }

    public void SetDir(Vector2 newDir)
    {
        dir = newDir;
    }

    public Vector2 GetDir()
    {
        return dir;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("suelo")) // por ejemplo
        {
            RecycleToPool();
        }
    }

    public void RecycleToPool()
    {
        if (!isMoving) return;
        if (sender) sender.Recycle(this.gameObject);
    }

    IEnumerator RecycleAtSecs(float secs = 10f)
    {
        yield return new WaitForSeconds(secs);
        RecycleToPool();
    }
}
