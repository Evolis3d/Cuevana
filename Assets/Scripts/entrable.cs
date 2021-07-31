using UnityEngine;

public class entrable : MonoBehaviour
{
    private GameObject prefabPlayable;
    private bool _canEnter;
    private Game _gameComp;

    private void Awake()
    {
        _gameComp = FindObjectOfType<Game>();
    }

    void Update()
    {
        var layerNaves = LayerMask.GetMask("naves");
        var hit = Physics2D.CircleCast(transform.position, 1f, Vector2.up,1f, layerNaves);
        _canEnter = (hit.collider != null);

        if (!_canEnter) return;
        
        if (Input.GetAxisRaw("Enter/Exit Vehicle") >= 1)
        {
            var vehic = hit.transform;
            prefabPlayable = vehic.GetComponent<info_playable>().prefabPlayable;

            if (!prefabPlayable) return;
            
            Destroy(vehic.gameObject);
            var newNave= Instantiate(prefabPlayable, vehic.position, Quaternion.identity);
            _gameComp.FocusOn(newNave.transform);
            Destroy(gameObject);
                
            //Debug.Log("Entra al vehiculo!");
            //Debug.Break();
        }
    }
    
    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position,1f);
    }
    */
}