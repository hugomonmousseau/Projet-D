using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePrevisualisationManager : MonoBehaviour
{
    [SerializeField] LineRenderer _line;
    public List<Transform> _points = new List<Transform>();

    public void RenderLine(List<Transform> _list)
    {
        _points = _list;
        _line.positionCount = _points.Count + 2;

        for(int _position = 0; _position < _points.Count ; _position++)
        {
            _line.SetPosition(_position,_points[_position].position);
        }
        
        //on copie les 2 dernieres pour bien boucler la ligne
        _line.SetPosition(_line.positionCount - 2, _points[0].position);
        _line.SetPosition(_line.positionCount - 1, _points[1].position);
        
    }

    public void DeathRattle()
    {
        Debug.Log("Destroy " + gameObject.name);
        Destroy(gameObject);
    }
}
