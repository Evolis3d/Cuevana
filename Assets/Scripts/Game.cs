using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject Player;
    private GameObject _player;

    //para que la camara siga siempre a una nave...
    private cameraFollow _camComp;

    private points_of_interest _poiComp;
    
    private void Awake()
    {
        var nao = FindObjectOfType<control_nave>();
        if (nao) _player = nao.gameObject;
        _camComp = FindObjectOfType<cameraFollow>();
        _poiComp = GetComponent<points_of_interest>();
    }

    void Start()
    {
        if (_player) _camComp.SetTarget(_player.transform);
    }

    void Update()
    {
        if (!_player && Input.GetKeyDown(KeyCode.F1))
        {
            _poiComp.RefreshPOI();
            
            var playerito = FindObjectOfType<control_minion>();
            if (playerito)
            {
                GameMode.Lives--; 
                Destroy(playerito.gameObject);
            }
            
            _player = Instantiate(Player, Vector3.zero, Quaternion.identity);
            _camComp.SetTarget(_player.transform);
        }
    }
}
