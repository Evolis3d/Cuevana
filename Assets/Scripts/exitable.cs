using UnityEngine;

public class exitable : MonoBehaviour
{
    public GameObject prefabPlayable;
    public GameObject prefabDummy;
    
    private bool _canLeave;

    private Game _gameComp;

    private void Awake()
    {
        _gameComp = FindObjectOfType<Game>();
    }

    void Update()
    {
        _canLeave = GameMode.PlayerLanded;

        if (_canLeave)
        {
            if (Input.GetAxisRaw("Enter/Exit Vehicle") <= -1)
            {
                if (!prefabPlayable || !prefabDummy) return;

                var navePos = transform.position;
                var dummy = Instantiate(prefabDummy, navePos, Quaternion.identity);
                Destroy(gameObject);
                
                var miniYo = Instantiate(prefabPlayable, navePos, Quaternion.identity);
                _gameComp.FocusOn(miniYo.transform);
                
                //Debug.Log("Sale del vehiculo!");
                //Debug.Break();
            }
        }
    }
}
