using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPointChanger : MonoBehaviour
{
    [SerializeField] float _duration;
    public Color _color;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Color _transitionColor;
    public Colors _colorName;

    void Start()
    {
        //_spriteRenderer = GetComponent<SpriteRenderer>();
        //Debug.Log(_color);
    }

    public void Colorisation(Color _newColor)
    {
        StartCoroutine(ColorChanger(_newColor));
        //_spriteRenderer.color = _newColor;
    }
    IEnumerator ColorChanger(Color _newColor)
    {
        float _timer = 0;
        while (_timer < _duration)
        {
            float _ratio = _timer / _duration;
            if (_ratio > 1)
                _ratio = 1;
            //Debug.Log("ratio : " + (int)(_ratio*100) + " color : " + _transitionColor + " color a atteindre : " + _newColor);
            _transitionColor = new Vector4(
                ((_newColor.r * _ratio) + (_color.r * (1 - _ratio)) ),
                ((_newColor.g * _ratio) + (_color.g * (1 - _ratio)) ),
                ((_newColor.b * _ratio) + (_color.b * (1 - _ratio)) ),
                1);
            _timer += Time.deltaTime;
            _spriteRenderer.color = _transitionColor;

            yield return null;
        }
    }
}
