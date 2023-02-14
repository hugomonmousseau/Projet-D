using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager : MonoBehaviour
{

    [Header("Points")]
    [SerializeField] GameObject _dicePoint;
    [SerializeField] GameObject _batimentPoint;
    public List<GameObject> _listPoints;

    [Space]
    [Header("Transforms")]
    [SerializeField] Transform _centre;
    [SerializeField] Transform _diceCote;
    [SerializeField] Transform _batCote;
    [SerializeField] Transform _pivot;

    private void Start()
    {
        PositionPoints();
    }
    public void PointsAppear()
    {
        int _nbConnexion = 0;
        for (int _loop = 0; _loop < _listPoints.Count ; _loop++)
        {
            _listPoints[_loop].GetComponent<PointID>().OnePointAppear();
            if (_listPoints[_loop].GetComponent<PointID>()._point._connecte)
                _nbConnexion++;
        }
        PositionPoints();
    }

    public void PointsDisappear()
    {
        for (int _loop = 0; _loop < _listPoints.Count ; _loop++)
        {
            _listPoints[_loop].GetComponent<PointID>().OnePointDisappear();
        }
    }

    public void PositionPoints()
    {
        if (_listPoints.Count > 0)//2 connexions
        {
            _dicePoint.transform.position = _diceCote.transform.position;
            _dicePoint.GetComponent<PointID>().PointUpdate();
            _batimentPoint.transform.position = _batCote.transform.position;
            _batimentPoint.GetComponent<PointID>().PointUpdate();
        }
    }
}
