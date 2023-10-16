using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineHexManager : MonoBehaviour
{
    [SerializeField] List<Vector3> _pointsPositions;
    [SerializeField] List<float> _hexagonalDisposition;
    [SerializeField] float _localScale;
    LineRenderer _lineRenderer;

    public Vector3 _focus;
    float _maxScale;
    [SerializeField] float _range;
    [SerializeField] AnimationCurve _curve;
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();

        _maxScale = _lineRenderer.startWidth;

        for (int _loop = 0; _loop < _pointsPositions.Count; _loop++)
        {
            _lineRenderer.SetPosition(_loop, _pointsPositions[_loop]*_localScale + transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {

        _curve = new AnimationCurve();
        for(int _loop = 0; _loop < _pointsPositions.Count; _loop++)
        {
            //entre 0 et 1
            //calculer la distance
            float _distance = Mathf.Sqrt(Mathf.Pow((_pointsPositions[_loop].x * _localScale + transform.position.x) - _focus.x, 2)) + Mathf.Sqrt(Mathf.Pow((_pointsPositions[_loop].y * _localScale + transform.position.y) - _focus.y, 2)) + Mathf.Sqrt(Mathf.Pow((_pointsPositions[_loop].z* _localScale + transform.position.z) - _focus.z, 2));
            // si distance = 0 > localScale = 1
            float _scale = (1 - Mathf.Pow((_distance / _range),3));
            if (_scale < 0)
                _scale = 0;

            Keyframe _key = new Keyframe();
            _key.time = _hexagonalDisposition[_loop];
            _key.value = _scale * _maxScale * _scale;
            _curve.AddKey(_key);
        }
        _lineRenderer.widthCurve = _curve;
        //_focus = GameManager._instance._previsualisationPosition;
    }
}
