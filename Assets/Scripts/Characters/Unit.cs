using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int _unitSpeed;
    public int _hp = 3;
    public float _distanceFromCrystal;
    public float _size = .25f;
    public bool _canMove = true;
    [HideInInspector] public float _spawnDuration = .5f;
    [SerializeField] AnimationCurve _curve;
    [HideInInspector] public Vector2 _startTilePosition;
    [HideInInspector] public Vector2 _endTilePosition;

    [HideInInspector] public int _pathID;
    [HideInInspector] public int _actualTile;

    [Header("Visual")]
    [SerializeField] Gradient _hitColor;
    [SerializeField] float _hitDuration = 1;
    [SerializeField] Gradient _deathColor;
    [SerializeField] float _deathDuration = 1;
    [SerializeField] GameObject _deathParticles;

    [HideInInspector] public Transform _children;
    void Start()
    {
        //StartCoroutine(NextStep());
        GetComponentInChildren<MaterialAnimator>()._actualState = EtatPersonnage.Idle;
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator NextStep()
    {
        int _actualStep = 0;
        int _maxStep = 10000 / _unitSpeed;
        _startTilePosition = _endTilePosition;
        _actualTile++;
        if(_actualTile < GameObject.FindGameObjectWithTag("LevelManager").GetComponent<PathsManager>()._pathsList[_pathID]._path.Count)
            _endTilePosition = new Vector2(GameObject.FindGameObjectWithTag("LevelManager").GetComponent<PathsManager>()._pathsList[_pathID]._path[_actualTile].transform.position.x, GameObject.FindGameObjectWithTag("LevelManager").GetComponent<PathsManager>()._pathsList[_pathID]._path[_actualTile].transform.position.z);
        transform.LookAt(new Vector3(_endTilePosition.x, transform.position.y, _endTilePosition.y));
        while (_actualStep < _maxStep && _canMove)
        {
            float _ratio = (float)_actualStep / _maxStep;
            _actualStep++;
            //Debug.Log("ratio : " + _ratio + " actual step " +  _actualStep);
            transform.position = new Vector3(_endTilePosition.x * _ratio + _startTilePosition.x * (1 - _ratio), transform.position.y, _endTilePosition.y * _ratio + _startTilePosition.y * (1 - _ratio));
            yield return null;
        }
        if(_canMove)
            StartCoroutine(NextStep());
    }
    public void Spawn(Vector3 _origin, Vector3 _tile, Vector3 _scale)
    {
        StartCoroutine(SpawnCoroutine(_origin,_tile,_scale));
    }
    IEnumerator SpawnCoroutine(Vector3 _origin, Vector3 _tile, Vector3 _scale)
    {
        float _path = -0.02f;
        float _time = 0;
        while(_time < _spawnDuration)
        {
            float _ratio = _time / _spawnDuration;
            transform.position = new Vector3(_origin.x * (1 - _ratio) +  _ratio * _tile.x, _origin.y * (1 - _ratio) + _curve.Evaluate(_ratio) + (_ratio) * _tile.y, _origin.z * (1 - _ratio) +  _ratio * _tile.z);
            transform.localScale = new Vector3(_scale.x * (1 - _ratio) + _ratio, _scale.y * (1 - _ratio) + _ratio, _scale.z * (1 - _ratio) + _ratio);
            _time += Time.deltaTime;
            if(_ratio < .5f)
                GetComponentInChildren<MaterialAnimator>()._actualState = EtatPersonnage.Jump;
            else
                GetComponentInChildren<MaterialAnimator>()._actualState = EtatPersonnage.Fall;
            yield return null;
        }

        transform.position = new Vector3(transform.position.x, _tile.y + _path, transform.position.z);
        StartCoroutine(NextStep());
        GetComponentInChildren<MaterialAnimator>()._actualState = EtatPersonnage.Run;
        //Debug.Log("position : " + transform.position.y + " t : " + _time);



    }

    [ContextMenu("Mort")]
    public void Dead()
    {
        GameObject.FindGameObjectWithTag("LevelManager").GetComponent<WavesManager>()._unitsAlive.Remove(gameObject);
    }
    [ContextMenu("Anim Mort")]
    public void DeadAnim()
    {
        GetComponentInChildren<MaterialAnimator>()._actualState = EtatPersonnage.Dead;
        _canMove = false;
        Instantiate(_deathParticles, _children);
        StartCoroutine(DeathColorisation());
        _children.GetComponent<MaterialAnimator>()._canRotation = false;
    }
    [ContextMenu("DistanceUpdate")]
    public void DistanceUpdate()
    {
        _distanceFromCrystal = 0;
        _distanceFromCrystal += Mathf.Sqrt(Mathf.Pow(transform.position.x - GameObject.FindGameObjectWithTag("LevelManager").GetComponent<PathsManager>()._pathsList[_pathID]._path[_actualTile].transform.position.x, 2) + Mathf.Pow(transform.position.z - GameObject.FindGameObjectWithTag("LevelManager").GetComponent<PathsManager>()._pathsList[_pathID]._path[_actualTile].transform.position.z, 2));
        //Debug.Log(_distanceFromCrystal);
        for (int _loop = 0; _loop < GameObject.FindGameObjectWithTag("LevelManager").GetComponent<PathsManager>()._pathsList[_pathID]._path.Count - _actualTile - 1; _loop++)
        {
            _distanceFromCrystal += Mathf.Sqrt(Mathf.Pow(GameObject.FindGameObjectWithTag("LevelManager").GetComponent<PathsManager>()._pathsList[_pathID]._path[_actualTile + 1].transform.position.x - GameObject.FindGameObjectWithTag("LevelManager").GetComponent<PathsManager>()._pathsList[_pathID]._path[_actualTile].transform.position.x, 2) + Mathf.Pow(GameObject.FindGameObjectWithTag("LevelManager").GetComponent<PathsManager>()._pathsList[_pathID]._path[_actualTile + 1].transform.position.z - GameObject.FindGameObjectWithTag("LevelManager").GetComponent<PathsManager>()._pathsList[_pathID]._path[_actualTile].transform.position.z, 2));
        }
        //Debug.Log(_distanceFromCrystal);
    }
    public void HitEffect()
    {
        StartCoroutine(HitFeedback());
    }
    IEnumerator HitFeedback()
    {
        float _timer = 0;
        while(_timer < _hitDuration)
        {
            _timer += Time.deltaTime;
            Color _color = _hitColor.Evaluate(_timer/_hitDuration);
            GetComponentInChildren<MeshRenderer>().material.color = _color;
            yield return null;
        }
    }

    IEnumerator DeathColorisation()
    {
        float _timer = 0;
        while (_timer < _deathDuration)
        {
            _timer += Time.deltaTime;
            Color _color = _deathColor.Evaluate(_timer / _deathDuration);
            GetComponentInChildren<MeshRenderer>().material.color = _color;
            yield return null;
        }
    }
}

