using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager : MonoBehaviour
{

    [Header("Points")]
    public GameObject _dicePoint;
    public GameObject _batimentPoint;
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
        if (_listPoints.Count > 1)//2 connexions
        {
            _dicePoint.transform.position = _diceCote.transform.position;
            _dicePoint.GetComponent<PointID>().PointUpdate();
            _batimentPoint.transform.position = _batCote.transform.position;
            _batimentPoint.GetComponent<PointID>().PointUpdate();
        }
    }
    private void Update()
    {
        //DeplacementsDesPoints();

        /*
        if (_listPoints.Count == 1)//2 connexions (0 & 1)

        {
            //comptons le nombre de pts connectés 
            _nbPointsConnectes = 0;
            for (int _loop = 0; _loop < _listPoints.Count; _loop++)
            {
                if (_listPoints[_loop].GetComponent<PointID>()._point._connecte)
                    _nbPointsConnectes++;
            }


            switch (_nbPointsConnectes)
            {
                case 0:
                    //aucune connexion

                    break;
                case 1:
                    // 1 connexion
                    break;
                case 2:
                    // 2 connexions
                    break;
            }
        }
        */

        //on essaie de faire une fonction adaptative
    }

    void DeplacementsDesPoints()
    {
        //Debug.Log(_freeBat);
        if (_listPoints.Count == 2)//2 connexions
        {


            //dice 

            if (_dicePoint.GetComponent<PointID>()._point._connecte)
                _freeDice = new Vector3(GameManager._instance._allPoints[_dicePoint.GetComponent<PointID>()._point._intID]._coordonnees.x, GameManager._instance._pointHeight, GameManager._instance._allPoints[_dicePoint.GetComponent<PointID>()._point._intID]._coordonnees.y);
            else
                _freeDice = new Vector3(_dicePoint.transform.position.x, transform.position.y, transform.position.z);

            //bat

            if (_batimentPoint.GetComponent<PointID>()._point._connecte)
                _freeBat = new Vector3(GameManager._instance._allPoints[_batimentPoint.GetComponent<PointID>()._point._intID]._coordonnees.x, GameManager._instance._pointHeight, GameManager._instance._allPoints[_batimentPoint.GetComponent<PointID>()._point._intID]._coordonnees.y);
            else
                _freeBat = new Vector3(_batimentPoint.transform.position.x, transform.position.y, transform.position.z);

            
            _free.position = _freeDice - _freeBat;

            _pivot.LookAt(_free);//free est un pts libre

            
        }
    }
}
