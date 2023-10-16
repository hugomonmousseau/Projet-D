using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSquares : MonoBehaviour
{
    [SerializeField] AnimationCurve _curveX;
    [SerializeField] AnimationCurve _curveY;
    [SerializeField] AnimationCurve _curveZ;
    [SerializeField] float _durationX;
    [SerializeField] float _durationZ;
    [SerializeField] float _maxAmountX;
    [SerializeField] float _maxAmountY;
    [SerializeField] float _maxAmountZ;
    float _ratioX;
    float _ratioZ;
    void Start()
    {
        StartCoroutine(AnimationCoroutineX());
        StartCoroutine(AnimationCoroutineZ());

    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(_curveX.Evaluate(_ratioX) * _maxAmountX, _curveY.Evaluate(_ratioX) * _maxAmountY, _curveZ.Evaluate(_ratioZ) * _maxAmountZ));
    }

    IEnumerator AnimationCoroutineX()
    {
        float _temp = 0;
        while(_temp < _durationX)
        {
            _ratioX = _temp / _durationX;
            _temp += Time.deltaTime;
            //Vector3 _vector = new Vector3(_curveX.Evaluate(_ratio) * _maxAmountX, _curveY.Evaluate(_ratio) * _maxAmountY, _curveZ.Evaluate(_ratio) * _maxAmountX);
            //transform.rotation = Quaternion.Euler(_vector);
            yield return null;
        }
        StartCoroutine(AnimationCoroutineX());
    }

    IEnumerator AnimationCoroutineZ()
    {
        float _temp = 0;
        while (_temp < _durationZ)
        {
            _ratioZ = _temp / _durationZ;
            _temp += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(AnimationCoroutineZ());
    }
}
