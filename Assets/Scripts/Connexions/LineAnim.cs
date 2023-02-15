using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineAnim : MonoBehaviour
{
    [SerializeField] float _defaultWidth;
    [SerializeField] float _duration;
    LineRenderer _line;

    public bool _appear;
    public bool _disappear;

    void Start()
    {
        _line = GetComponent<LineRenderer>();
        _defaultWidth = _line.startWidth;
    }

    private void Update()
    {
        if (_appear)
        {
            _appear = false;
            LineAppear();
        }

        if (_disappear)
        {
            _disappear = false;
            LineDisappear();
        }
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
            _line.startWidth = _defaultWidth * (_ratio / _duration);
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
            _line.startWidth = _defaultWidth * (1 -_ratio / _duration);
            yield return null;
        }
    }

}
