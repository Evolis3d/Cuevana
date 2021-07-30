using UnityEngine;

public class exitable : MonoBehaviour
{
    public GameObject prefabPlayable;
    private control_nave _control;
    private bool _canLeave;

    private Game _gameComp;

    private void Awake()
    {
        _control = GetComponent<control_nave>();
        _gameComp = FindObjectOfType<Game>();
    }

    void Update()
    {
        _canLeave = _control.landed;

        if (_canLeave)
        {
            if (Input.GetAxisRaw("Enter/Exit Vehicle") <= -1)
            {
                if (!prefabPlayable) return;

                var navePos = transform.position;
                
                Destroy(gameObject.GetComponent<sensores>());
                Destroy(gameObject.GetComponent<control_nave>());
                Destroy(gameObject.GetComponent<control_pod>());
                Destroy(gameObject.GetComponent<exitable>());
                gameObject.GetComponent<Rigidbody2D>().simulated = false;
                gameObject.GetComponent<Collider2D>().isTrigger = true;
                //Destroy(gameObject);
                
                var miniYo = Instantiate(prefabPlayable, navePos, Quaternion.identity);
                _gameComp.FocusOn(miniYo.transform);
                
                //Debug.Log("Sale del vehiculo!");
                //Debug.Break();
            }
        }
    }
}
