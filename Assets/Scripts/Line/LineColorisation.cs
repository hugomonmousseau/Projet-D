using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineColorisation : MonoBehaviour
{
    LineRenderer _lineRenderer;
    public Color _startColor;
    public Color _endColor;
    [SerializeField] Gradient _gradient;
    [SerializeField] float _duration;
    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }
    [ContextMenu("LineColor")]
    public void LineNewColor(Color _newColor)
    {
        _endColor = _newColor;
        StartCoroutine(TransitionColor());
    }
    
    public IEnumerator TransitionColor()
    {
        float _timer = 0;
        while(_timer < _duration)
        {
            _timer += Time.deltaTime;
            float _ratio = _timer / _duration;
            Color _actualColor = new Vector4(
                _endColor.r * _ratio + _startColor.r * (1 - _ratio),
                _endColor.g * _ratio + _startColor.g * (1 - _ratio),
                _endColor.b * _ratio + _startColor.b * (1 - _ratio),
                _endColor.a * _ratio + _startColor.a * (1 - _ratio)
                );
            _gradient.SetKeys(
                new GradientColorKey[] { new GradientColorKey(_actualColor, 0f), new GradientColorKey(_actualColor, 1f) },
                new GradientAlphaKey[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, 1f) });
            _lineRenderer.colorGradient = _gradient;
            //Debug.Log(_timer / _duration);
            yield return null;
        }
        _startColor = _endColor;
    }
}
