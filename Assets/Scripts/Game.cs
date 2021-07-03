using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject Player;
    private GameObject _player;

    private void Awake()
    {
        _player = FindObjectOfType<control>().gameObject;
    }

    void Update()
    {
        if (!_player && Input.GetKeyDown(KeyCode.F1))
        {
            _player = Instantiate(Player, Vector3.zero, Quaternion.identity);
        }
    }
}
