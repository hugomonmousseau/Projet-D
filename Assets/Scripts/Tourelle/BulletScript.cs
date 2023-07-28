using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Rigidbody _rb;
    public float _speed;
    public GameObject _focus;
    [SerializeField] GameObject _impact;
    [SerializeField] Material _smokeMat;

    bool _canImpact = true;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (_canImpact)
        {
            
            transform.LookAt(new Vector3(_focus.transform.position.x, _focus.transform.position.y + _focus.GetComponent<Unit>()._size, _focus.transform.position.z));
            _rb.velocity = transform.forward * _speed;
        }
        else
        {
            _rb.velocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider _other)
    {
        //Debug.Log(_other.gameObject);
        if (_other.gameObject == _focus && _canImpact)
        {
            //Debug.Log("frappons " + _other.name);
            Instantiate(_impact, new Vector3(_focus.transform.position.x, _focus.transform.position.y + _focus.GetComponent<Unit>()._size, _focus.transform.position.z), Quaternion.identity);
            GetComponent<TrailRenderer>().material = _smokeMat;
            _canImpact = false;
            Destroy(gameObject,.1f);
        }
    }
}

