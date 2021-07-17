using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class points_of_interest : MonoBehaviour
{
    [SerializeField] private List<Interactivo> puntosDeInteres;
    private int _currentPointed; 
        
    private void Awake()
    {
        RefreshPOI();
    }

    public Transform GiveNextPOI()
    {
        if (!puntosDeInteres[_currentPointed]) RefreshPOI();
        
        var poi = puntosDeInteres[_currentPointed].transform;

        _currentPointed += 1 % puntosDeInteres.Count;
        
        return poi;
    }

    public void RefreshPOI()
    {
        var lista = FindObjectsOfType<Interactivo>();
        if (puntosDeInteres!= null && puntosDeInteres.Count >0) puntosDeInteres.Clear();
        puntosDeInteres = new List<Interactivo>();
        foreach (var el in lista)
        {
            puntosDeInteres.Add(el);
        }
        _currentPointed = 0;
    }

}
