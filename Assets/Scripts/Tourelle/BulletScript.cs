using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Rigidbody _rb;
    public BulletType _type;

    public float _speed;
    public GameObject _focus;
    [SerializeField] GameObject _impact;
    [SerializeField] Material _smokeMat;
    [SerializeField] ParticleSystem _particleSystem;

    bool _canImpact = true;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if(_canImpact && _type == BulletType.Wind)
        {
            transform.LookAt(GetComponent<WindBullet>()._focus);
            _rb.velocity = transform.forward * _speed;
        }
        else if (_canImpact && _focus != null) 
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
            //hit effect
            _focus.GetComponent<Unit>().HitEffect();

            //on envoie l'anim de mort si besoin
            if(_focus.GetComponent<Unit>()._hp <= 0)
            {
                _focus.GetComponent<Unit>().DeadAnim();
            }

            //Debug.Log("frappons " + _other.name);
            Instantiate(_impact, new Vector3(_focus.transform.position.x, _focus.transform.position.y + _focus.GetComponent<Unit>()._size, _focus.transform.position.z), Quaternion.identity);
            if(_smokeMat != null)
                GetComponent<TrailRenderer>().material = _smokeMat;
            _canImpact = false;
            if (_type == BulletType.Camp)
            {
                _rb.velocity = Vector3.zero;
                var _main = _particleSystem.main;
                _main.loop = false;
                Destroy(gameObject, .85f);
            }
            else
            {
                Destroy(gameObject, .1f);
            }
        }
    }
}

public enum BulletType
{
    Turret,
    Wind,
    Camp,
    Cherry
}

