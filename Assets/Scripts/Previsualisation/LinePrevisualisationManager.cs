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
        _line.positionCount = _points.Count + 1;

        for(int _position = 0; _position < _points.Count - 1; _position++)
        {
            _line.SetPosition(_position,_points[_position].position);
        }
        _line.SetPosition(_line.positionCount, _points[_points.Count + 1].position);
        
    }
}
