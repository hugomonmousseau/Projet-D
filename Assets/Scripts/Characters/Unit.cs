using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int _unitSpeed;
    public Vector2 _startTilePosition;
    public Vector2 _endTilePosition;

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
        int _maxStep = 5000 / _unitSpeed;
        while(_actualStep < _maxStep)
        {
            float _ratio = (float)_actualStep / _maxStep;
            _actualStep++;
            //Debug.Log("ratio : " + _ratio + " actual step " +  _actualStep);
            transform.position = new Vector3(_startTilePosition.x * _ratio + _endTilePosition.x * (1 - _ratio), transform.position.y, _startTilePosition.y * _ratio + _endTilePosition.y * (1 - _ratio));
            yield return null;
        }

        StartCoroutine(NextStep());
    }
}

