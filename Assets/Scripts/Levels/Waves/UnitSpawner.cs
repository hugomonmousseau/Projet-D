using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    public List<GameObject> _unitList;
    [SerializeField] float _spawnDelay;
    [SerializeField] float _boatSpeed;
    [SerializeField] int _pathID;
    public GameObject _boat;
    [SerializeField] GameObject _spawnTile;
    [SerializeField] Transform _endPositon;
    [SerializeField] GameObject _hex;

    Vector3 _ref = Vector3.zero;
    public bool _inMovement;
    Animator _anim;

    void Start()
    {
        //Debug.Log(_unitList.Count);
        //Debug.Log((int)5 / 2);
        _anim = GetComponent<Animator>();
        _inMovement = false;

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
        
    }
    public void EndOfMovement()
    {
        _inMovement = false;
        SpawnUnits();
        StartCoroutine(DissolveAnimation());
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
        if(_unitList.Count > 1)
        {
            for (int _loop = 0; _loop < _unitList.Count - _nbUnitsOnFirstSide; _loop++)
            {
                float _ratio = (float)_loop / _nbUnitsOnFirstSide;

                _unitList[_nbUnitsOnFirstSide + _loop].transform.position = _boat.GetComponent<Boat>()._boatPoints[2].position * _ratio + _boat.GetComponent<Boat>()._boatPoints[3].position * (1 - _ratio);
            }
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

        _unitList[_id].GetComponent<Unit>()._pathID = _pathID;
        _unitList[_id].GetComponent<Unit>()._endTilePosition = new Vector2(GameObject.FindGameObjectWithTag("LevelManager").GetComponent<PathsManager>()._pathsList[_pathID]._path[0].transform.position.x, GameObject.FindGameObjectWithTag("LevelManager").GetComponent<PathsManager>()._pathsList[_pathID]._path[0].transform.position.z);
        _unitList[_id].GetComponent<Unit>().Spawn(_unitList[_id].transform.position,_spawnTile.transform.position,_unitList[_id].transform.localScale);
        StartCoroutine(WaitBeforegettingAttack(_unitList[_id]));
        //spawn
    }

    IEnumerator WaitBeforegettingAttack(GameObject _unit)
    {
        yield return new WaitForSeconds(_unit.GetComponent<Unit>()._spawnDuration);
        GameObject.FindGameObjectWithTag("LevelManager").GetComponent<WavesManager>()._unitsAlive.Add(_unit);

    }

    IEnumerator DissolveAnimation()
    {
        yield return new WaitForSeconds(_spawnDelay * (_unitList.Count));
        //_hex.transform.position = new Vector3( _boat.transform.position.x , _boat.transform.position.y + (3/4f) , _boat.transform.position.z);
        _anim.SetBool("Sink",true);
    }
}
