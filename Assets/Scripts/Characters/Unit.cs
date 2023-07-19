using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int _unitSpeed;
    float _spawnDuration = .5f;
    [SerializeField] AnimationCurve _curve;
    public Vector2 _startTilePosition;
    public Vector2 _endTilePosition;

    public int _pathID;
    public int _actualTile;
    void Start()
    {
        //StartCoroutine(NextStep());

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
        while (_actualStep < _maxStep)
        {
            float _ratio = (float)_actualStep / _maxStep;
            _actualStep++;
            //Debug.Log("ratio : " + _ratio + " actual step " +  _actualStep);
            transform.position = new Vector3(_endTilePosition.x * _ratio + _startTilePosition.x * (1 - _ratio), transform.position.y, _endTilePosition.y * _ratio + _startTilePosition.y * (1 - _ratio));
            yield return null;
        }

        StartCoroutine(NextStep());
    }

    public void Spawn(Vector3 _origin, Vector3 _scale)
    {
        StartCoroutine(SpawnCoroutine(_origin,_scale));
    }
    IEnumerator SpawnCoroutine(Vector3 _origin, Vector3 _scale)
    {
        float _time = 0;
        while(_time < _spawnDuration)
        {
            float _ratio = _time / _spawnDuration;
            transform.position = new Vector3(_origin.x * (1 - _ratio) + _endTilePosition.x * _ratio, _origin.y * (1 - _ratio) + _curve.Evaluate(_ratio) - .05f *(_ratio), _origin.z * (1 - _ratio) + _endTilePosition.y * _ratio);
            transform.localScale = new Vector3(_scale.x * (1 - _ratio) + _ratio, _scale.y * (1 - _ratio) + _ratio, _scale.z * (1 - _ratio) + _ratio);
            _time += Time.deltaTime;
            yield return null;
        }

        StartCoroutine(NextStep());

    }
}

