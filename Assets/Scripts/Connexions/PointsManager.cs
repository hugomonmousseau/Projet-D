using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager : MonoBehaviour
{

    [Header("Points")]
    public GameObject _dicePoint;
    public GameObject _batimentPoint;
    public GameObject _hud;
    public List<GameObject> _listPoints;
    public int _nbPointsConnectes;

    [Space]
    [Header("Transforms")]
    [SerializeField] Transform _centre;
    [SerializeField] Transform _diceCote;
    [SerializeField] Transform _batCote;
    [SerializeField] Transform _pivot;
    [SerializeField] Transform _free;

    [SerializeField] Vector3 _freeDice;
    [SerializeField] Vector3 _freeBat;
    private void Start()
    {
        PositionPoints();
    }
    

    public void PositionPoints()
    {
        if (_listPoints.Count > 1 && GetComponent<BatimentManager>()._type != Batiment.Dé)//2 connexions
        {
            if (_dicePoint != null)
            {
                _dicePoint.transform.position = _diceCote.transform.position;
                _dicePoint.GetComponent<PointID>().PointUpdate();
            }

            if (_batimentPoint != null)
            {
                _batimentPoint.transform.position = _batCote.transform.position;
                _batimentPoint.GetComponent<PointID>().PointUpdate();
            }
        }


    }

}
