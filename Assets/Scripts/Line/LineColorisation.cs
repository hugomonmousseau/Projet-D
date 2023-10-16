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
    public Colors _actualColorName;
    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }
    [ContextMenu("LineColor")]
    public void LineNewColor(Colors _color)
    {
        if(_actualColorName != _color)
        {
            Color _newColor = new Color();
            _actualColorName = _color;
            for (int _tier = 0; _tier < GameObject.FindGameObjectWithTag("ColorManager").GetComponent<ColorManager>()._tierList.Count; _tier++)
            {
                for (int _loop = 0; _loop < GameObject.FindGameObjectWithTag("ColorManager").GetComponent<ColorManager>()._tierList[_tier]._tier.Count; _loop++)
                {
                    if (_color == GameObject.FindGameObjectWithTag("ColorManager").GetComponent<ColorManager>()._tierList[_tier]._tier[_loop]._color)
                    {
                        _newColor = GameObject.FindGameObjectWithTag("ColorManager").GetComponent<ColorManager>()._tierList[_tier]._tier[_loop]._appearance;
                    }
                }
            }
             
            _endColor = _newColor;
            StartCoroutine(TransitionColor());

        }
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

    public void StartColor(Colors _color)
    {
        if (_color != Colors.None && _color != _actualColorName)
        {
            for (int _tier = 0; _tier < GameObject.FindGameObjectWithTag("ColorManager").GetComponent<ColorManager>()._tierList.Count; _tier++)
            {
                for(int _loop = 0; _loop < GameObject.FindGameObjectWithTag("ColorManager").GetComponent<ColorManager>()._tierList[_tier]._tier.Count; _loop ++)
                {
                    if(_color == GameObject.FindGameObjectWithTag("ColorManager").GetComponent<ColorManager>()._tierList[_tier]._tier[_loop]._color)
                    {
                        Color _actualColor = GameObject.FindGameObjectWithTag("ColorManager").GetComponent<ColorManager>()._tierList[_tier]._tier[_loop]._appearance;
                        _gradient.SetKeys(
                            new GradientColorKey[] { new GradientColorKey(_actualColor, 0f), new GradientColorKey(_actualColor, 1f) },
                            new GradientAlphaKey[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, 1f) });

                        _lineRenderer = GetComponent<LineRenderer>();
                        _lineRenderer.colorGradient = _gradient;


                        _actualColorName = _color;
                    }
                }
            }
        }
    }

}
