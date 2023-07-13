using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    public List<GameObject> _unitList;
    [SerializeField] float _spawnDelay;
    [SerializeField] float _boatSpeed;
    public GameObject _boat;
    [SerializeField] GameObject _spawnTile;
    [SerializeField] Transform _endPositon;
    Vector3 _ref = Vector3.zero;
    bool _inMovement = true;

    int _unitSpawned;
    void Start()
    {
        //Debug.Log(_unitList.Count);
        //Debug.Log((int)5 / 2);
    }

    void Update()
    {

        //Debug.Log(_inMovement);
        if (_inMovement)
        {
            Movement();
            UnitPrevisualisation();
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector3(_boat.transform.position.x, .25f, _boat.transform.position.z), new Vector3(_endPositon.position.x, .25f, _endPositon.position.z));
    }

    void Movement()
    {

        _boat.transform.LookAt(new Vector3(_endPositon.position.x, transform.position.y, _endPositon.position.z));
        _boat.transform.position = Vector3.SmoothDamp(_boat.transform.position, _endPositon.position, ref _ref, 1 / _boatSpeed * 10);
        //_rb.velocity = _boat.transform.forward * _boatSpeed;
        //_boatSpeed -= _boatSpeed / 5000;
        SpawnUnits();
    }
    public void EndOfMovement()
    {
        _inMovement = false;

    }

    void UnitPrevisualisation()
    {
        //il faut diviser l equipage en deux pour faire 50/50 autour du mat
        int _nbUnitsOnFirstSide = (int)_unitList.Count / 2;
        //on repartis les unités sur le premier bout
        for(int _loop = 0; _loop < _nbUnitsOnFirstSide; _loop++)
        {
            float _ratio = (float)_loop / _nbUnitsOnFirstSide;
            
            _unitList[_loop].transform.position = _boat.GetComponent<Boat>()._boatPoints[0].position * _ratio + _boat.GetComponent<Boat>()._boatPoints[1].position * (1 - _ratio);
        }
        for (int _loop = 0; _loop < _unitList.Count-_nbUnitsOnFirstSide; _loop++)
        {
            float _ratio = (float)_loop / _nbUnitsOnFirstSide;

            _unitList[_nbUnitsOnFirstSide + _loop].transform.position = _boat.GetComponent<Boat>()._boatPoints[2].position * _ratio + _boat.GetComponent<Boat>()._boatPoints[3].position * (1 - _ratio);
        }

    }
    void SpawnUnits()
    {
        for (int _loop = 0; _loop < _unitList.Count; _loop++)
        {
            StartCoroutine(SpawningUnit(_loop));
        }
    }

    IEnumerator SpawningUnit(int _id)
    {
        yield return new WaitForSeconds(_id * _spawnDelay);
        //spawn
    }
}
