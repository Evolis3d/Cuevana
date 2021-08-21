using UnityEngine;

public class IA_disparable : MonoBehaviour
{
    private void Start()
    { }

    public void Shoot(Transform from)
    {
            GameSystems.bulletPool.Shoot(from);
    }
}
