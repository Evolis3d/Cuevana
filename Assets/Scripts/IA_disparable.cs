﻿using UnityEngine;

public class IA_disparable : MonoBehaviour
{
    private void Start()
    { }

    public void Shoot(Transform from)
    {
        GameSystems.bulletPool.Shoot(from);
    }
    
    //variedad que le pasa también el Direction
    public void Shoot(Transform from, Vector3 direction)
    {
        GameSystems.bulletPool.Shoot(from, direction);
    }
}
