using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    LineRenderer _lineRenderer;
    [HideInInspector] public Vector2 _startPoint;
    public bool _isEnd;
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.SetPosition(0, new Vector3(_startPoint.x,.3f,_startPoint.y));
    }

    void Update()
    {
        if(!_isEnd)
            _lineRenderer.SetPosition(1,new Vector3(GameManager._instance._selectionConnexionCoordonnees.x,.3f, GameManager._instance._selectionConnexionCoordonnees.y));

        if (Input.GetMouseButtonUp(0) && !_isEnd)
        {
            int _idPoint = GameManager._instance.IdPointLocalisation();
            Debug.Log(_idPoint);
            if (_idPoint >= 0)
            {
                _isEnd = true;
                _lineRenderer.SetPosition(1,new Vector3(GameManager._instance._allPoints[_idPoint]._coordonnees.x, .3f, GameManager._instance._allPoints[_idPoint]._coordonnees.y));
            }
            else
                Destroy(gameObject);
        }
    }


}
