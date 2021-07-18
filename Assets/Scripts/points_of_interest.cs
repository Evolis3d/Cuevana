using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class points_of_interest : MonoBehaviour
{
    [SerializeField] private List<Interactivo> puntosDeInteres;
    private int _currentPointed=0; 
        
    private void Awake()
    {
        RefreshPOI();
    }

    public Transform GiveNextPOI()
    {
        if (puntosDeInteres==null || puntosDeInteres.Count <1) 
        {
            RefreshPOI();
            return null;
        }
        
        var poi = puntosDeInteres[_currentPointed];
        if (!poi && _currentPointed == puntosDeInteres.Count- 1)
        {
            RefreshPOI();
            return null;
        } else if (!poi)
        {
            _currentPointed++;
            GiveNextPOI();
        }

        _currentPointed += 1 % puntosDeInteres.Count;
        
        return poi.transform;
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
