using UnityEngine;

public class lifesystems : MonoBehaviour
{
    public GameObject prefabLifebar;
    public float health;
    private float _currentHealth; 

    private GameObject _bar;
    private info_lifebar _infoComp;
    
    private void Start()
    {
        if (!prefabLifebar) return;
        
        _bar = Instantiate(prefabLifebar, transform);
        _infoComp = _bar.GetComponent<info_lifebar>();

        _currentHealth = health;
    }

    private void Update()
    {
        if (!prefabLifebar) return;
        
        //testeo, quitar luego
        if (Input.GetKeyDown(KeyCode.L))
        {
            var healthLeft = _currentHealth / health;
            _infoComp.Show(healthLeft, Vector2.up);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!prefabLifebar) return;
        
        if (other.collider.CompareTag("bullet"))
        {
            var healthLeft = _currentHealth / health;
            _infoComp.Show(health, Vector2.up);
        }
    }
}
