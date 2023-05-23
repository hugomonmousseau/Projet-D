using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialDependOnGradient : MonoBehaviour
{
    [SerializeField] Gradient _gradient;
    [SerializeField] float _duration;
    Material _mat;
    [SerializeField] bool _isChildConsern;
    void Start()
    {
        if (_isChildConsern)
            _mat = GetComponentInChildren<MeshRenderer>().material;
        else
            _mat = GetComponent<MeshRenderer>().material;
    }

    public IEnumerator ColorChanger()
    {
        float _timer = 0;
        while(_timer < _duration)
        {
            _timer += Time.deltaTime;
            _mat.color = _gradient.Evaluate(_timer / _duration);
            //Debug.Log(_timer / _duration);
            yield return null;
        }
    }


}
