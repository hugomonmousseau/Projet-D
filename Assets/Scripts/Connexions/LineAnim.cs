using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineAnim : MonoBehaviour
{
    [SerializeField] float _defaultWidth;
    [SerializeField] float _duration;
    [SerializeField] AnimationCurve _curve;
    LineRenderer _line;

    void Start()
    {
        _line = GetComponent<LineRenderer>();
        _defaultWidth = _line.startWidth;
    }

    public void LineAppear()
    {
        StartCoroutine(AnimEnter());
    }


    IEnumerator AnimEnter()
    {
        float _ratio = 0;
        while(_ratio < _duration)
        {
            _ratio += Time.deltaTime;
            _line.startWidth = _defaultWidth * _curve.Evaluate(_ratio / _duration);
            yield return null;
        }
    }

    public void LineDisappear()
    {
        StartCoroutine(AnimExit());
    }


    IEnumerator AnimExit()
    {
        float _ratio = 0;
        while (_ratio < _duration)
        {
            
            _ratio += Time.deltaTime;
            _line.startWidth = _defaultWidth * _curve.Evaluate(1 - _ratio / _duration);
            if ( _ratio>= _duration)
                _line.startWidth = 0;
            
            yield return null;
        }
    }

}