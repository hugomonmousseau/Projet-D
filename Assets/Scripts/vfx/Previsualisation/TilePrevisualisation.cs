using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePrevisualisation : MonoBehaviour
{
    [SerializeField] float _range;
    [SerializeField] Vector3 _position;
    float _maxScale;
    private void Start()
    {
        _maxScale = transform.localScale.x;
    }
    void Update()
    {
        //entre 0 et 1
        //calculer la distance
        float _distance = Mathf.Sqrt(Mathf.Pow(transform.position.x - _position.x, 2)) + Mathf.Sqrt(Mathf.Pow(transform.position.y - _position.y, 2)) + Mathf.Sqrt(Mathf.Pow(transform.position.z - _position.z, 2));
        // si distance = 0 > localScale = 1
        float _scale = (1 - Mathf.Pow(_distance / _range,2));
        if (_scale < 0)
            _scale = 0;
        transform.localScale = new Vector3(_scale * _maxScale, _scale * _maxScale, _scale * _maxScale);

        _position = GameManager._instance._previsualisationPosition;
    }
}
