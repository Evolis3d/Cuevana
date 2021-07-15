using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(Collider2D))]
public class gravitable : MonoBehaviour
{
    //privadas
    private Rigidbody2D rb;
    private Collider2D col;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!rb || !col) return;
        //if (col is BoxCollider2D boxCollider2D) col = boxCollider2D;
        
        if (other.CompareTag("0gravzone"))
        {
            var puntos = new ContactPoint2D[0];
            col.GetContacts(puntos);
            foreach (var punto in puntos)
            {
                if (!other.OverlapPoint(punto.point)) return;
            }
            
            if (rb.gravityScale !=0f) rb.gravityScale = 0f; //por defecto la nave tiene 0.1
            if (rb.freezeRotation) rb.freezeRotation = false;
            rb.angularVelocity = 10f;  //gira por ahí...
            return;
        }

        if (other.CompareTag("invertgravzone"))
        {
            var puntos = new ContactPoint2D[0];
            col.GetContacts(puntos);
            foreach (var punto in puntos)
            {
                if (!other.OverlapPoint(punto.point)) return;
            }
            
            if (rb.gravityScale != -0.1f) rb.gravityScale = -0.1f; //por defecto la nave tiene 0.1

            if (rb.rotation!=180f) rb.rotation = Mathf.LerpAngle(rb.rotation, 180f, 5f * Time.deltaTime);
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!rb || !col) return;
        
        if (other.CompareTag("0gravzone"))
        {
            rb.gravityScale = 0.1f; //por defecto la nave tiene 0.1
            if (!rb.freezeRotation) rb.freezeRotation = true;
            rb.angularVelocity = 0f;
            rb.rotation = 0f;
            return;
        }

        if (other.CompareTag("invertgravzone"))
        {
            rb.gravityScale = 0.1f; //por defecto la nave tiene 0.1 y hacia abajo..

            if (rb.rotation != 0f) rb.rotation = 0f;
        }
    }
}
